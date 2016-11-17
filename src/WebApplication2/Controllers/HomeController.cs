using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication2.Models;
using WebApplication2.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc.Rendering;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ITffFacultyRepository _repository;
        //private readonly UserManager<System.Security.Claims.ClaimsPrincipal> _otherManager;

        public HomeController(UserManager<User> usermanager, ITffFacultyRepository repository
            //, UserManager<System.Security.Claims.ClaimsPrincipal> othermanager
            )
        {
            _userManager = usermanager;
            _repository = repository;
            //_otherManager = othermanager;
        }

        //Task<System.Security.Claims.ClaimsPrincipal> GetCurrentUserAsync() => _otherManager.GetUserIdAsync(HttpContext.User);

        public async Task<IActionResult> Index(string message)
        {
            ViewBag.Message = message;
            //var z = _otherManager.GetUserIdAsync(HttpContext.User);
            var x = await _userManager.FindByNameAsync(User.Identity.Name);
            var vm = Mapper.Map<FacultyProfileViewModel>(x);
            vm.Disclosures = _repository.GetUserDisclosures(x);
            vm.EventFaculty = _repository.GetUserEvents(x);
            return View(vm);
        }

        public async Task<IActionResult> Profile()
        {
            var states = new List<string> { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
            ViewBag.states = new SelectList(states);

            var countries = new List<string> { "Afghanistan", "Albanien", "Algeriet", "Angola", "Antigua och Barbuda", "Argentina", "Australien", "Azerbajdzjan", "Österrike", "Östtimor", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belgien", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnien och Hercegovina", "Botswana", "Brasilien", "Brunei Darussalam", "Bulgarien", "Burkina Faso", "Burundi", "Centralafrikanska Republiken", "Chile", "Colombia", "Costa Rica", "Cypern", "Danmark", "Demokratiska Republiken Kongo", "Dominikanska Republiken", "Ecuador", "Egypten", "El Salvador", "Elfenbenskusten", "Estland", "Etiopien", "Färöarna", "Förenade Arabemiraten", "Filippinerna", "Finland", "Frankrike", "Gabon", "Georgien", "Ghana", "Gibraltar", "Grönland", "Grekland", "Grenada", "Guatemala", "Honduras", "Hong Kong", "Indien", "Indonesien", "Irak", "Iran", "Irland", "Island", "Israel", "Italien", "Jamaica", "Japan", "Jemen", "Jersey", "Jordanien", "Kambodja", "Kanada", "Kazakstan", "Kenya", "Kina", "Kiribati", "Kroatien", "Kuba", "Kuwait", "Laos", "Lettland", "Libanon", "Libyen", "Litauen", "Luxemburg", "Madagaskar", "Makedonien", "Malawi", "Malaysia", "Maldiverna", "Mali", "Malta", "Marocko", "Mauritius", "Mexiko", "Mocambique", "Monaco", "Mongoliet", "Myanmar", "Namibia", "Nederländerna", "Nederländska Antillerna", "Nepal", "Nicaragua", "Niger", "Nigeria", "Norge", "Nya Zeeland", "Oman", "Pakistan", "Panama", "Paraguay", "Peru", "Polen", "Portugal", "Puerto Rico", "Qatar", "Rumänien", "Rwanda", "Ryssland", "Saint Lucia", "Saint Vincent och Grenadinerna", "Samoa", "San Marino", "Sao Tome och Principe", "Saudiarabien", "Schweiz", "Senegal", "Serbien och Montenegro", "Seychellerna", "Sierra Leone", "Singapore", "Slovakien", "Slovenien", "Somalia", "Spanien", "Sri Lanka", "Sudan", "Surinam", "Sverige", "Swaziland", "Sydafrika", "Sydkorea", "Syrien", "Taiwan", "Tanzania", "Thailand", "Tjeckien", "Trinidad och Tobago", "Tunisien", "Turkiet", "Tyskland", "Uganda", "Ukraina", "Ungern", "Uruguay", "USA", "Uzbekistan", "Venezuela", "Vietnam", "Vitryssland", "Zambia", "Zimbabwe" };
            ViewBag.countries = new SelectList(countries);

            var specialities = _repository.GetSpecialties().Select(m => m.SpecialtyTitle).OrderBy(m => m);
            ViewBag.specialties = new SelectList(specialities);

            var vm = Mapper.Map<FacultyProfileViewModel>(await _userManager.FindByNameAsync(HttpContext.User.Identity.Name));
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(FacultyProfileViewModel vm)
        {
            var states = new List<string> { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
            ViewBag.states = new SelectList(states);

            var countries = new List<string> { "Afghanistan", "Albanien", "Algeriet", "Angola", "Antigua och Barbuda", "Argentina", "Australien", "Azerbajdzjan", "Österrike", "Östtimor", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belgien", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnien och Hercegovina", "Botswana", "Brasilien", "Brunei Darussalam", "Bulgarien", "Burkina Faso", "Burundi", "Centralafrikanska Republiken", "Chile", "Colombia", "Costa Rica", "Cypern", "Danmark", "Demokratiska Republiken Kongo", "Dominikanska Republiken", "Ecuador", "Egypten", "El Salvador", "Elfenbenskusten", "Estland", "Etiopien", "Färöarna", "Förenade Arabemiraten", "Filippinerna", "Finland", "Frankrike", "Gabon", "Georgien", "Ghana", "Gibraltar", "Grönland", "Grekland", "Grenada", "Guatemala", "Honduras", "Hong Kong", "Indien", "Indonesien", "Irak", "Iran", "Irland", "Island", "Israel", "Italien", "Jamaica", "Japan", "Jemen", "Jersey", "Jordanien", "Kambodja", "Kanada", "Kazakstan", "Kenya", "Kina", "Kiribati", "Kroatien", "Kuba", "Kuwait", "Laos", "Lettland", "Libanon", "Libyen", "Litauen", "Luxemburg", "Madagaskar", "Makedonien", "Malawi", "Malaysia", "Maldiverna", "Mali", "Malta", "Marocko", "Mauritius", "Mexiko", "Mocambique", "Monaco", "Mongoliet", "Myanmar", "Namibia", "Nederländerna", "Nederländska Antillerna", "Nepal", "Nicaragua", "Niger", "Nigeria", "Norge", "Nya Zeeland", "Oman", "Pakistan", "Panama", "Paraguay", "Peru", "Polen", "Portugal", "Puerto Rico", "Qatar", "Rumänien", "Rwanda", "Ryssland", "Saint Lucia", "Saint Vincent och Grenadinerna", "Samoa", "San Marino", "Sao Tome och Principe", "Saudiarabien", "Schweiz", "Senegal", "Serbien och Montenegro", "Seychellerna", "Sierra Leone", "Singapore", "Slovakien", "Slovenien", "Somalia", "Spanien", "Sri Lanka", "Sudan", "Surinam", "Sverige", "Swaziland", "Sydafrika", "Sydkorea", "Syrien", "Taiwan", "Tanzania", "Thailand", "Tjeckien", "Trinidad och Tobago", "Tunisien", "Turkiet", "Tyskland", "Uganda", "Ukraina", "Ungern", "Uruguay", "USA", "Uzbekistan", "Venezuela", "Vietnam", "Vitryssland", "Zambia", "Zimbabwe" };
            ViewBag.countries = new SelectList(countries);

            var specialities = _repository.GetSpecialties().Select(m => m.SpecialtyTitle).OrderBy(m => m);
            ViewBag.specialties = new SelectList(specialities);

            if (!ModelState.IsValid) return View(vm);
            var dbFaculty = await _userManager.FindByIdAsync(vm.Id);

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
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public IActionResult CreateDisclosure()
        {
            return View(new DisclosureViewModel() { SignatureDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDisclosure(DisclosureViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var disclosure = Mapper.Map<Disclosure>(vm);
            disclosure.User = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!_repository.CreateDisclosure(disclosure)) return View(vm);
            _repository.SaveAll();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateDisclosure()
        {
            var disclosure = _repository.GetUserCurrentDisclosure(User.Identity.Name);
            var vm = Mapper.Map<DisclosureViewModel>(disclosure);
            vm.SignatureDate = DateTime.Now;
            vm.SignatureText = "";
            vm.SignatureConfirm = false;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDisclosure(DisclosureViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            _repository.UpdateFacultyDisclosure(Mapper.Map<Disclosure>(vm));
            _repository.SaveAll();
            return RedirectToAction("Index");
        }

        public IActionResult EventFacultyManage(string id)
        {
            var db = _repository.GetEventFaculty(id);
            if (db.User.UserName != User.Identity.Name)
                return RedirectToAction("Index",
                    new
                    {
                        Message =
                            "You are not authorized to view the event details for this event: " + db.Event.EventName
                    });
            //var x = _repository.GetEventFacultyRoles(db.UserId, db.EventId);
            //var selected = x.Select(role => (role.FacultyRoleId).ToString()).ToList();
            //var allRoles = _repository.GetAllFacultyRoles();
            //ViewBag.RolesSelect = new MultiSelectList(allRoles, "Id", "Name", selected);
            //ViewBag.RoleCount = allRoles.Count;

            var times = new List<string> { "12am", "1am", "2am", "3am", "4am", "5am", "6am", "7am", "8am", "9am", "10am", "11am", "12pm", "1pm", "2pm", "3pm", "4pm", "5pm", "6pm", "7pm", "8pm", "9pm", "10pm", "11pm"};
            ViewBag.times = new SelectList(times);


            return View(Mapper.Map<HomeEventFacultyViewModel>(db));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EventFacultyManage(HomeEventFacultyViewModel vm)
        {
            var times = new List<string> { "12am", "1am", "2am", "3am", "4am", "5am", "6am", "7am", "8am", "9am", "10am", "11am", "12pm", "1pm", "2pm", "3pm", "4pm", "5pm", "6pm", "7pm", "8pm", "9pm", "10pm", "11pm" };
            ViewBag.times = new SelectList(times);

            if (!ModelState.IsValid) return View(vm);

            //remove all roles and add the selected ones
            //_repository.RemoveEventFacultyRoles(vm.UserId, vm.EventId);
            //_repository.AddEventFacultyRoles(vm.UserId, vm.EventId, vm.EventFacultyRoles);

            var db = _repository.GetEventFaculty(vm.Id);

            db.BookHotel = vm.BookHotel;

            db.CheckIn = vm.BookHotel == true ? vm.CheckIn : null;
            db.CheckOut = vm.BookHotel == true ? vm.CheckOut : null;

            db.TravelMethod = vm.TravelMethod;

            if(vm.TravelMethod == "Please book my air travel" || vm.TravelMethod == "Please book my train travel")
            {
                db.DepartureDate = vm.DepartureDate;
                db.DepartureTime = vm.DepartureTime;
                db.DepartureLocation = vm.DepartureLocation;
                db.ReturnDate = vm.ReturnDate;
                db.ReturnTime = vm.ReturnTime;
                db.ReturnLocation = vm.ReturnLocation;
            } else
            {
                db.DepartureDate = null;
                db.DepartureTime = null;
                db.DepartureLocation = null;
                db.ReturnDate = null;
                db.ReturnTime = null;
                db.ReturnLocation = null;
            }

            db.FilledOut = true;
            _repository.SaveAll();

            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
