using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;
using AutoMapper;
using WebApplication2.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ITffFacultyRepository _repository;
        private readonly UserManager<User> _userManager;

        public AdminController(ITffFacultyRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Events()
        {
            return View();
        }
        //public IActionResult Events(string statusCode)
        //{
        //    var events = new List<Event>();
        //    var vm = new List<EventViewModel>();
        //    if(statusCode == null)
        //    {
        //        statusCode = "Active";
        //    }

        //    ViewBag.StatusCodes = _repository.GetStatusCodes();
        //    if(statusCode == "All")
        //    {
        //        events = _repository.GetAllEvents();                
        //        foreach(var e in events)
        //        {
        //            var temp = Mapper.Map<EventViewModel>(e);              
        //            vm.Add(temp);
        //        }
        //        return View(vm);
        //    }

        //    events = _repository.GetAllEvents(statusCode);
        //    foreach (var e in events)
        //    {
        //        var temp = Mapper.Map<EventViewModel>(e);                
        //        vm.Add(temp);
        //    }
        //    return View(vm);
        //}

        public IActionResult CreateEvent()
        {
            ViewBag.states = new SelectList(GetStates());
            return View(new EventViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(EventViewModel vm)
        {
            ViewBag.states = new SelectList(GetStates());

            if (!ModelState.IsValid) return View(vm);
            var newEvent = Mapper.Map<Event>(vm);

            var currentUser = this.User;
            newEvent.Manager = await _userManager.FindByNameAsync(currentUser.Identity.Name);

            if (_repository.AddEvent(newEvent))
            {
                if (_repository.SaveAll())
                {
                    return RedirectToAction("Events");
                }
                ModelState.AddModelError("", "There was an error while saving to database");
                return View(vm);
            }
            ModelState.AddModelError("", "There was an error adding the event.");
            return View(vm);
        }

        public IActionResult EventDetails(int id)
        {
            var vm = Mapper.Map<EventViewModel>(_repository.GetEvent(id));
            ViewBag.states = new SelectList(GetStates());
            return View(vm);
        }

        //FINISHED
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EventDetails(EventViewModel vm)
        {
            ViewBag.states = new SelectList(GetStates());

            if (!ModelState.IsValid) return View(vm);
            if (_repository.UpdateEvent(vm))
            {
                //var x = _repository.SaveAll();
                if (_repository.SaveAll())
                {
                    return RedirectToAction("Events");
                }
                ModelState.AddModelError("", "There was an error while saving updated event to database");
                return View(vm);
            }
            ModelState.AddModelError("", "There was an error while updating the event");
            return View(vm);
        }

        public IActionResult EventFaculty(int id)
        {
            ViewBag.EventId = id;
            ViewBag.EventName = _repository.GetEvent(id).EventName;
            var vm = _repository.GetEventAllFaculty(id);
            return View(vm);
        }

        public IActionResult AddEventFaculty(int id)
        {
            var vm = new AdminAddFactultyToEventViewModel()
            {
                Event = _repository.GetEvent(id),
                EventId = id
            };

            var users = _repository.GetAllUsersNotInEvent(id);

            if (users != null)
            {
                ViewBag.userSelect = CreateFacultySelectDropdown(users);
                return View(vm);
            }

            ModelState.AddModelError("", "There were no faculty to select for this event");
            return View(vm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEventFaculty(AdminAddFactultyToEventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.User = await _userManager.FindByIdAsync(vm.UserId);
                vm.Id = Guid.NewGuid().ToString();
                if (vm.User != null)
                {
                    if (await _repository.AddEventFaculty(Mapper.Map<EventFaculty>(vm)))
                    {
                        if (_repository.SaveAll())
                        {
                            return RedirectToAction("EventFaculty", new { id = vm.EventId });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Could not save to the database");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Could not add event faculty");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "There was a problem getting the user from the database");
                }
            }

            vm.Event = _repository.GetEvent(vm.EventId);
            var users = _repository.GetAllUsersNotInEvent(vm.EventId);
            if (users == null) return View(vm);
            ViewBag.userSelect = CreateFacultySelectDropdown(users);
            ModelState.AddModelError("", "There were no faculty to select for this event");

            return View(vm);
        }

        public IActionResult EventFacultyManage(string id)
        {
            var db = _repository.GetEventFaculty(id);

            var x = _repository.GetEventFacultyRoles(db.UserId, db.EventId);

            var selected = x.Select(role => role.FacultyRoleId).ToList();
            var allRoles = _repository.GetAllFacultyRoles();

            ViewBag.RolesSelect = new MultiSelectList(allRoles, "Id", "Name", selected); //  (_repository.GetAllFacultyRoles(); //_repository.GetEventFacultyRoles(UserId, int.Parse(EventId));
            ViewBag.RoleCount = allRoles.Count;

            ViewBag.times = new SelectList(GetTimes());

            return View(Mapper.Map<AdminEventFacultyViewModel>(db));
        }

        [HttpPost]
        public async Task<IActionResult> EventFacultyManage(AdminEventFacultyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (_repository.AddEventFacultyRoles(vm.UserId, vm.EventId, vm.EventFacultyRoles))
                {
                    if (_repository.SaveAll())
                    {
                        var db = _repository.GetEventFaculty(vm.Id);
                        db.BookHotel = vm.BookHotel;
                        db.CheckIn = vm.CheckIn;
                        db.CheckOut = vm.CheckOut;
                        db.TravelMethod = vm.TravelMethod;
                        db.DepartureDate = vm.DepartureDate;
                        db.ReturnDate = vm.ReturnDate;
                        db.DepartureLocation = vm.DepartureLocation;
                        db.DepartureTime = vm.DepartureTime;
                        db.ReturnLocation = vm.ReturnLocation;
                        db.ReturnTime = vm.ReturnTime;
                        db.FilledOut = true;
                        _repository.SaveAll();
                        return RedirectToAction("EventFaculty", new { id = vm.EventId });
                    }
                    ModelState.AddModelError("", "Could not save to the database");
                }
                ModelState.AddModelError("", "Could not change the faculty roles");
            }

            var allRoles = _repository.GetAllFacultyRoles();
            ViewBag.RolesSelect = new MultiSelectList(allRoles, "Id", "Name", _repository.GetEventFacultyRoles(vm.UserId, vm.EventId));
            ViewBag.RoleCount = allRoles.Count;

            vm.Event = _repository.GetEvent(vm.EventId);
            vm.User = await _repository.GetFaculty(vm.UserId);

            return View(vm);
        }

        //public IActionResult Faculty(string statusCode)
        //{
        //    ViewBag.StatusCodes = _repository.GetStatusCodes();
        //    var vm = new List<FacultyProfileViewModel>();
        //    var faculty = new List<User>();

        //    if(statusCode == null)
        //    {
        //        statusCode = "Active";
        //    }

        //    if (statusCode == "All")
        //    {
        //        faculty = _repository.GetAllFaculty();
        //        foreach(var f in faculty)
        //        {
        //            var temp = Mapper.Map<FacultyProfileViewModel>(f);                    
        //            vm.Add(temp);
        //        }
        //        return View(vm);
        //    }
        //    else
        //    {
        //        faculty = _repository.GetAllFaculty(statusCode);
        //        foreach (var f in faculty)
        //        {
        //            var temp = Mapper.Map<FacultyProfileViewModel>(f);                    
        //            vm.Add(temp);
        //        }
        //        return View(vm);
        //    }
        //}

        public async Task<IActionResult> Admins()
        {
            var admins = _repository.GetAllAdmins();
            var vm = new List<AdminProfileViewModel>();
            
            if (admins != null)
            {
                foreach (var a in admins)
                {
                    vm.Add(new AdminProfileViewModel() { Id = a.Id, FirstName = a.FirstName, LastName = a.LastName, SuperAdmin = await _userManager.IsInRoleAsync(a, "SuperAdmin") });
                }
                return View(vm);
            }
            ModelState.AddModelError("", "There was a problem retrieving the admins list");
            return View(new List<AdminProfileViewModel>());
        }

        public IActionResult FacultyRoles()
        {
            var facultyRoles = _repository.GetAllFacultyRoles();
            if (facultyRoles != null)
            {
                return View(facultyRoles);
            }
            ModelState.AddModelError("", "There was a problem when retrieving the faculty roles list");
            return View(new List<FacultyRole>());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddFacultyRole(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _repository.AddFacultyRole(name);
            }

            if (_repository.SaveAll())
            {
                return RedirectToAction("FacultyRoles");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Specialties()
        {
            var specialties = _repository.GetSpecialties();
            if (specialties != null)
            {
                return View(specialties);
            }
            ModelState.AddModelError("", "There was a problem when retrieving the specialties list");
            return View(new List<Specialty>());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddSpecialty(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _repository.AddSpecialty(name);
            }

            if (_repository.SaveAll())
            {
                return RedirectToAction("Specialties");
            }
            else
            {
                return View();
            }
        }

        public IActionResult CreateFaculty(string error)
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFaculty(CreateUserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                User user = Mapper.Map<User>(vm);
                user.Email = vm.UserName;
                //var x = await _repository.CreateUser(user);


                var result = _repository.CreateFaculty(user).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Faculty");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(AdminProfileViewModel vm)
        {
            if (!ModelState.IsValid) return View();
            

            var result = await _repository.CreateAdmin(vm);

            if (!result.Succeeded)
            {
                AddErrors(result);
                return View();
            }

            return RedirectToAction("Admins");                       
        }

        public async Task<IActionResult> EditFaculty(string id)
        {
            ViewBag.states = new SelectList(GetStates());

            ViewBag.countries = new SelectList(GetCountries());

            ViewBag.specialties = new SelectList(_repository.GetSpecialties().Select(m => m.SpecialtyTitle).OrderBy(m => m));

            var faculty = Mapper.Map<FacultyProfileViewModel>(await _repository.GetFaculty(id));

            return View(faculty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFaculty(FacultyProfileViewModel vm)
        {        
            

            if (ModelState.IsValid)
            {
                var dbFaculty = await _repository.GetFaculty(vm.Id);
                if (dbFaculty.UserName != vm.UserName)
                {
                    var result = await _userManager.SetEmailAsync(dbFaculty, vm.UserName);
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        return View(vm);
                    }

                    result = await _userManager.SetUserNameAsync(dbFaculty, vm.UserName);
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        return View(vm);
                    }
                }

                Mapper.Map(vm, dbFaculty);

                if (_repository.SaveAll())
                {
                    return RedirectToAction("Faculty");
                }
                return View(vm);
            }

            ViewBag.states = new SelectList(GetStates());

            ViewBag.countries = new SelectList(GetCountries());

            ViewBag.specialties = new SelectList(_repository.GetSpecialties().Select(m => m.SpecialtyTitle).OrderBy(m => m));

            return View(vm);
        }

        public async Task<IActionResult> EditAdmin(string Id)
        {
            var adminUser = await _userManager.FindByIdAsync(Id);
            var vm = Mapper.Map<AdminProfileViewModel>(adminUser);
            if(await _userManager.IsInRoleAsync(adminUser, "SuperAdmin"))
            {
                vm.SuperAdmin = true;
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(AdminProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (await _repository.EditAdmin(vm))
                {
                    return RedirectToAction("Admins");
                }                
            }
            return View(vm);
        }

        public async Task<IActionResult> DeleteAdmin(string Id)
        {
            var dbUser = await _userManager.FindByIdAsync(Id);
            var deleteUser = new AdminDeleteViewModel(){
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName };
            return View(deleteUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdmin(AdminDeleteViewModel deleteUser)
        {
            var dbUser = await _userManager.FindByIdAsync(deleteUser.Id);
            await _userManager.DeleteAsync(dbUser);
            _repository.SaveAll();
            return RedirectToAction("Admins");
        }

        public async Task<IActionResult> DeactivateUser(string Id)
        {
            await _repository.DeactivateUser(Id);
            return View();
        }

        public async Task<IActionResult> ActivateUser(string Id)
        {
            await _repository.ActivateUser(Id);
            return View();
        }

        public IActionResult DeactivateEvent(int Id)
        {
            _repository.DeactivateEvent(Id);
            return View();
        }

        public IActionResult ActivateEvent(int Id)
        {
            _repository.ActivateEvent(Id);
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> ResendEventFacultyEmail(string Id)
        {
            await _repository.ResendEventFacultyEmail(Id);
            ViewBag.Id = Id;
            return View();
        }

        public IActionResult Faculty()
        {
            return View();
        }        

        private static List<SelectListItem> CreateFacultySelectDropdown(IEnumerable<User> users)
        {
            var userSelect = new List<SelectListItem>() { new SelectListItem { Value = "", Text = "Select Faculty" } };
            userSelect.AddRange(users.Select(user => new SelectListItem
            {
                Value = user.Id, Text = user.LastName + ", " + user.FirstName
            }));
            return userSelect;
        }

        private List<string> GetCountries()
        {
            return new List<string> { "Afghanistan", "Albanien", "Algeriet", "Angola", "Antigua och Barbuda", "Argentina", "Australien", "Azerbajdzjan", "Österrike", "Östtimor", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belgien", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnien och Hercegovina", "Botswana", "Brasilien", "Brunei Darussalam", "Bulgarien", "Burkina Faso", "Burundi", "Centralafrikanska Republiken", "Chile", "Colombia", "Costa Rica", "Cypern", "Danmark", "Demokratiska Republiken Kongo", "Dominikanska Republiken", "Ecuador", "Egypten", "El Salvador", "Elfenbenskusten", "Estland", "Etiopien", "Färöarna", "Förenade Arabemiraten", "Filippinerna", "Finland", "Frankrike", "Gabon", "Georgien", "Ghana", "Gibraltar", "Grönland", "Grekland", "Grenada", "Guatemala", "Honduras", "Hong Kong", "Indien", "Indonesien", "Irak", "Iran", "Irland", "Island", "Israel", "Italien", "Jamaica", "Japan", "Jemen", "Jersey", "Jordanien", "Kambodja", "Kanada", "Kazakstan", "Kenya", "Kina", "Kiribati", "Kroatien", "Kuba", "Kuwait", "Laos", "Lettland", "Libanon", "Libyen", "Litauen", "Luxemburg", "Madagaskar", "Makedonien", "Malawi", "Malaysia", "Maldiverna", "Mali", "Malta", "Marocko", "Mauritius", "Mexiko", "Mocambique", "Monaco", "Mongoliet", "Myanmar", "Namibia", "Nederländerna", "Nederländska Antillerna", "Nepal", "Nicaragua", "Niger", "Nigeria", "Norge", "Nya Zeeland", "Oman", "Pakistan", "Panama", "Paraguay", "Peru", "Polen", "Portugal", "Puerto Rico", "Qatar", "Rumänien", "Rwanda", "Ryssland", "Saint Lucia", "Saint Vincent och Grenadinerna", "Samoa", "San Marino", "Sao Tome och Principe", "Saudiarabien", "Schweiz", "Senegal", "Serbien och Montenegro", "Seychellerna", "Sierra Leone", "Singapore", "Slovakien", "Slovenien", "Somalia", "Spanien", "Sri Lanka", "Sudan", "Surinam", "Sverige", "Swaziland", "Sydafrika", "Sydkorea", "Syrien", "Taiwan", "Tanzania", "Thailand", "Tjeckien", "Trinidad och Tobago", "Tunisien", "Turkiet", "Tyskland", "Uganda", "Ukraina", "Ungern", "Uruguay", "USA", "Uzbekistan", "Venezuela", "Vietnam", "Vitryssland", "Zambia", "Zimbabwe" };
        }

        private List<string> GetStates()
        {
            return new List<string> { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
        }

        private List<string> GetTimes()
        {
            return new List<string> { "12am", "1am", "2am", "3am", "4am", "5am", "6am", "7am", "8am", "9am", "10am", "11am", "12pm", "1pm", "2pm", "3pm", "4pm", "5pm", "6pm", "7pm", "8pm", "9pm", "10pm", "11pm" };

        }


    }
}
