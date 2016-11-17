using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebApplication2.ViewModels;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.Extensions.OptionsModel;
using Newtonsoft.Json;
using WebApplication2.Services;
using Microsoft.AspNet.Mvc;


namespace WebApplication2.Models
{
    public class TffFacultyRepository : ITffFacultyRepository
    {
        readonly TffFacultyContext _context;
        readonly ILogger<TffFacultyRepository> _logger;
        readonly UserManager<User> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly IEmailSender _emailSender;
        readonly IHttpContextAccessor _contextAccessor;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUrlHelper _urlHelper;
        readonly IHttpContextAccessor _httpContext;


        public TffFacultyRepository(TffFacultyContext context, 
            ILogger<TffFacultyRepository> logger, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IEmailSender emailSender, 
            IHttpContextAccessor contextAccessor,
            IOptions<AppSettings> appSettings,
            IUrlHelper urlHelper,
            IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            _appSettings = appSettings;
            _urlHelper = urlHelper;
            _httpContext = httpContext;
        }

        public bool AddEvent(Event newEvent)
        {
            try
            {
                newEvent.Status = "Active";
                _context.Add(newEvent);
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not add new event", ex);
                return false;
            }
            return true;
        }

        public async Task<bool> AddEventFaculty(EventFaculty eu)
        {
            try
            {
                _context.Add(eu);
                eu.Event = _context.Events
                    .Include(m => m.Manager)
                    .Single(m => m.Id == eu.EventId);

                
                if (!eu.User.EmailConfirmed)
                {
                    eu.User.EmailConfirmed = true;
                    var content = File.ReadAllText(@"EmailTemplates/NewFacultyMeetingEmail.html");
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(eu.User);
                    string callbackUrl = _urlHelper.Action("SetFirstPassword", "Account", new { userId = eu.User.Id, code = token }, _httpContext.HttpContext.Request.Scheme);

                    content = content
                        .Replace("[-eventName-]", eu.Event.EventName)
                        .Replace("[-eventManager-]", eu.Event.Manager.FirstName + " " + eu.Event.Manager.LastName)
                        .Replace("[-eventManagerEmail-]", eu.Event.Manager.Email)
                        .Replace("[-eventManagerPhone-]", eu.Event.Manager.PhoneNumber)
                        .Replace("[-validationLink-]", callbackUrl);

                    await _emailSender.SendEmailAsync(eu.User.UserName, eu.Event.Manager.Email, eu.Event.Manager.FirstName + " " + eu.Event.Manager.LastName, eu.Event.EventName + " - Please Complete Forms and Profile" , content);
                    eu.User.EmailConfirmed = false;
                } else
                {
                    var content = File.ReadAllText(@"EmailTemplates/CurrentFacultyMeetingEmail.html");
                    var siteURL = _urlHelper.Action("Index", "Home", new { }, _httpContext.HttpContext.Request.Scheme);

                    content = content
                        .Replace("[-eventName-]", eu.Event.EventName)
                        .Replace("[-eventManager-]", eu.Event.Manager.FirstName + " " + eu.Event.Manager.LastName)
                        .Replace("[-eventManagerEmail-]", eu.Event.Manager.Email)
                        .Replace("[-eventManagerPhone-]", eu.Event.Manager.PhoneNumber)
                        .Replace("[-userName-]", eu.User.UserName)
                        .Replace("[-siteURL-]", siteURL);

                    await _emailSender.SendEmailAsync(eu.User.UserName, eu.Event.Manager.Email, eu.Event.Manager.FirstName+" "+eu.Event.Manager.LastName, eu.Event.EventName + " - Please Complete Forms and Update Profile", content);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not add event faculty", ex);
                return false;
            }
            return true;
        }

        public async Task<bool> ResendEventFacultyEmail(string Id)
        {
            var ef = _context.EventFaculty.Include(m=>m.User).Include(m=>m.Event).Include(m=>m.Event.Manager).Single(m => m.Id == Id);

            if (!ef.User.EmailConfirmed)
            {
                ef.User.EmailConfirmed = true;
                var content = File.ReadAllText(@"EmailTemplates/NewFacultyMeetingEmail.html");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(ef.User);
                string callbackUrl = _urlHelper.Action("SetFirstPassword", "Account", new { userId = ef.User.Id, code = token }, _httpContext.HttpContext.Request.Scheme);

                content = content
                    .Replace("[-eventName-]", ef.Event.EventName)
                    .Replace("[-eventManager-]", ef.Event.Manager.FirstName + " " + ef.Event.Manager.LastName)
                    .Replace("[-eventManagerEmail-]", ef.Event.Manager.Email)
                    .Replace("[-eventManagerPhone-]", ef.Event.Manager.PhoneNumber)
                    .Replace("[-validationLink-]", callbackUrl);

                await _emailSender.SendEmailAsync(ef.User.UserName, ef.Event.Manager.Email, ef.Event.Manager.FirstName + " " + ef.Event.Manager.LastName, ef.Event.EventName + " - Please Complete Forms and Profile", content);
                ef.User.EmailConfirmed = false;
            }
            else
            {
                var content = File.ReadAllText(@"EmailTemplates/CurrentFacultyMeetingEmail.html");
                var siteURL = _urlHelper.Action("Index", "Home", new { }, _httpContext.HttpContext.Request.Scheme);

                content = content
                    .Replace("[-eventName-]", ef.Event.EventName)
                    .Replace("[-eventManager-]", ef.Event.Manager.FirstName + " " + ef.Event.Manager.LastName)
                    .Replace("[-eventManagerEmail-]", ef.Event.Manager.Email)
                    .Replace("[-eventManagerPhone-]", ef.Event.Manager.PhoneNumber)
                    .Replace("[-userName-]", ef.User.UserName)
                    .Replace("[-siteURL-]", siteURL);

                await _emailSender.SendEmailAsync(ef.User.UserName, ef.Event.Manager.Email, ef.Event.Manager.FirstName + " " + ef.Event.Manager.LastName, ef.Event.EventName + " - Please Complete Forms and Update Profile", content);
            }
            return true;
        }

        public void AddFacultyRole(string name)
        {
            _context.FacultyRoles.Add(new FacultyRole() { Name = name });
        }

        public bool CreateDisclosure(Disclosure newDisclosure)
        {
            try
            {
                newDisclosure.CreateDate = DateTime.Now;
                newDisclosure.EditDate = DateTime.Now;
                _context.Add(newDisclosure);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not add Disclosure", ex);
                return false;
            }

            return true;
        }

        public List<Disclosure> GetUserDisclosures(User user)
        {
            try
            {
                return _context.Disclosures.Where(m => m.User.UserName == user.UserName).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not get disclosures for a user", ex);
                return null;
            }
            
        }

        public async Task<IdentityResult> CreateUser(User user)
        {
            user.Status = "Active";
            return await _userManager.CreateAsync(user, _appSettings.Value.DefaultPassword);
        }

        public List<Event> GetAllEvents()
        {
            try
            {
                return _context.Events.Include(m=>m.Manager).OrderByDescending(m => m.EventStartDate).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get events.", ex);
                return null;
            }
        }

        public List<Event> GetAllEvents(string adminId)
        {
            try
            {
                return _context.Events.Include(m=>m.Manager).Where(m => m.Manager.Id == adminId).OrderByDescending(m => m.EventStartDate).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get events.", ex);
                return null;
            }
        }

        public List<FacultyRole> GetAllFacultyRoles()
        {
            try
            {
                return _context.FacultyRoles.OrderBy(m => m.Name).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not get faculty roles from database", ex);
                return null;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return _context.Users.OrderBy(t => t.LastName).Include(m=>m.Disclosures).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Users from database", ex);
                return null;
            }
        }

        public Event GetEvent(int id)
        {
            try
            {
                
                return _context.Events.SingleOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Count not get Event ID {id} from database", id, ex);
                return null;
            }
        }

        public List<EventFaculty> GetEventAllFaculty(int id)
        {
            try
            {
                return _context.EventFaculty.Where(m => m.EventId == id).Include(m => m.User).Include(m=>m.Event)
                    .Include(m=>m.Event.EventFacultyRoles).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Event ID {id} Faculty from database", id, ex);
                return null;
            }
        }

        public EventFaculty GetOneEventFaculty(short v, string userId)
        {
            return _context.EventFaculty.Where(m => m.EventId == v).Where(m => m.UserId == userId).Include(m => m.User).Include(m => m.Event).SingleOrDefault();
        }

        public FacultyRole GetOneRole(int v)
        {
            return _context.FacultyRoles.SingleOrDefault(m => m.Id == v);
        }

        public bool SaveAll()
        {
            try
            {
                return _context.SaveChanges() >= 0;
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot save to database", ex);
                return false;
            }
        }

        public bool UpdateEvent(EventViewModel updatedEvent)
        {
            try
            {
                var db = _context.Events.Single(m => m.Id == updatedEvent.Id);
                Mapper.Map(updatedEvent, db);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not update the event: {updatedEvent}", JsonConvert.SerializeObject(updatedEvent), ex);
                return false;
            }
        }

        public List<EventFaculty> GetUserEvents(User x)
        {
            return _context.EventFaculty.Where(m => m.UserId == x.Id).Include(m=>m.Event).ToList();
        }

        public List<EventFacultyRole> GetEventFacultyRoles(string userId, int eventId)
        {
            return _context.EventFacultyRoles.Where(m => m.UserId == userId).Where(m => m.EventId == eventId).ToList();
        }

        public EventFaculty GetEventFaculty(string id)
        {
            try
            {
                var x = _context.EventFaculty.Where(m => m.Id == id).Include(m => m.User).Include(m => m.Event).SingleOrDefault();
                return x;
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not get Event ID {id} faculty", id, ex);
                return null;
            }
        }

        public bool RemoveEventFacultyRoles(string userId, int eventId)
        {
            try
            {
                var roles = _context.EventFacultyRoles.Where(m => m.UserId == userId).Where(m => m.EventId == eventId).ToList();
                //_context.EventFacultyRoles.Remove(roles);
                foreach (var role in roles)
                {
                    _context.EventFacultyRoles.Remove(role);
                    _context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not get event faculty roles for User ID {userId}, Event Id {eventId}", userId, eventId, ex);
                return false;
            }
            
            return true;
        }

        public void AddEventFacultyRole(string userId, int eventId, int roleId)
        {
            _context.EventFacultyRoles.Add(new EventFacultyRole() { UserId = userId, EventId = eventId, FacultyRoleId = roleId });
        }

        public async Task<User> GetFaculty(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<User> GetFacultyByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public bool AddEventFacultyRoles(string userId, int eventId, List<string> eventFacultyRoles)
        {
            if (!RemoveEventFacultyRoles(userId, eventId)) return false;
            foreach (var role in eventFacultyRoles)
            {
                AddEventFacultyRole(userId, eventId, int.Parse(role));
            }
            return true;
        }

        public List<User> GetAllUsersNotInEvent(int id)
        {
            var users = GetAllFaculty();

            var query = from user in users
                        where   user.EventFaculty.Count == 0 ||
                                user.EventFaculty.Any(m => m.EventId != id)
                        select user;

            var result = query.ToList();

            try
            {
                return result.Count > 0 ? result : null;
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not get faculty not associated with Event Id {id}", id, ex);
                return null;
            } 
        }

        public Disclosure GetUserCurrentDisclosure(string name)
        {
            var user = _context.Users.Single(m => m.UserName == name);

            return _context.Disclosures.SingleOrDefault(m => m.User.UserName == user.UserName && m.IsBackup == false);
        }

        public Disclosure GetDisclosureById(int id)
        {
            return _context.Disclosures.Single(m => m.Id == id);
        }

        public void UpdateFacultyDisclosure(Disclosure disclosure)
        {
            var db = GetDisclosureById(disclosure.Id);
            db.IsBackup = true;
            disclosure.Id = 0;
            disclosure.EditDate = DateTime.Now;
            disclosure.SignatureDate = DateTime.Now;
            _context.Disclosures.Add(disclosure);
            SaveAll();
        }

        public List<User> GetAllFaculty()
        {
            try
            {
                var role = _roleManager.FindByNameAsync("Faculty").Result;
                return _context.Users
                    .Include(m => m.EventFaculty)
                    .Include(m => m.Disclosures)
                    .Include(m => m.Roles)
                    .ToList()
                    .Where(m => m.Roles.Select(r => r.RoleId).ToList().Contains(role.Id))
                    .ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not get all faculty", ex);
                return null;
            }
        }

        public List<User> GetAllFaculty(string statusCode)
        {
            try
            {
                var role = _roleManager.FindByNameAsync("Faculty").Result;
                return _context.Users
                    .Include(m => m.EventFaculty)
                    .Include(m => m.Disclosures)
                    .Include(m => m.Roles)
                    .ToList()
                    .Where(m => m.Status == statusCode).Where(m => m.Roles.Select(r => r.RoleId).ToList().Contains(role.Id))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get all faculty", ex);
                return null;
            }
        }


        public async Task<IdentityResult> CreateFaculty(User user)
        {
            //user.EmailConfirmed = true;
            user.Status = "Active";

            var x = await _userManager.CreateAsync(user);

            if (x.Succeeded)
            {
                var u = await _userManager.FindByNameAsync(user.UserName);                
                await _userManager.AddToRoleAsync(u, "Faculty");
                return x;
            }
            return x;
        }

        private async Task<string> GenerateNewFacultyEmailContent(User newUser, string token)
        {
            var url = _urlHelper.Action("SetFirstPassword", "Account", new { userId = newUser.Id, code = token }, protocol: _httpContext.HttpContext.Request.Scheme);

            var content = File.ReadAllText(@"EmailTemplates/NewFacultyEmail.html");

            var code = await _userManager.GeneratePasswordResetTokenAsync(newUser);

            var projectManagerUserName = _contextAccessor.HttpContext.User.Identity.Name;
            var projectManager = await _userManager.FindByNameAsync(projectManagerUserName);

            content = content
                .Replace("[lastName]", newUser.LastName)
                .Replace("[emailAddress]", newUser.UserName)
                .Replace("[projectManagerFirstName]", projectManager.FirstName)
                .Replace("[projectManagerLastName]", projectManager.LastName)
                .Replace("[projectManagerEmail]", projectManager.UserName)
                .Replace("[projectManagerPhone]", projectManager.PhoneNumber)
                .Replace("[setPasswordLink]", url);
            //.Replace("[siteAddress]", _appSettings.Value.WebsiteAddress);

            return content;
        }

        public List<User> GetAllAdmins()
        {
            try
            {
                var role = _roleManager.FindByNameAsync("Admin").Result;
                return _context.Users
                    .Include(m => m.Roles)
                    .ToList()
                    .Where(m => m.Roles.Select(r => r.RoleId).ToList().Contains(role.Id))
                    .ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not get all admins", ex);
                return null;
            }
        }

        public async Task<IdentityResult> CreateAdmin(AdminProfileViewModel user)
        {
            var newUser = new User()
            {
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.UserName,
                Status = "Active"
            };
            var result = await _userManager.CreateAsync(newUser, _appSettings.Value.DefaultPassword);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Admin");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                await _userManager.ConfirmEmailAsync(newUser, code);
                if (user.SuperAdmin) await _userManager.AddToRoleAsync(newUser, "SuperAdmin");
            }                        

            return result;
        }

        public List<Specialty> GetSpecialties()
        {
            return _context.Specialties.ToList();
        }

        public void AddSpecialty(string name)
        {
            _context.Specialties.Add(new Specialty() { SpecialtyTitle = name });
        }

        public async Task DeactivateUser(string Id)
        {
            var dbUser = await _userManager.FindByIdAsync(Id);
            dbUser.Status = "Inactive";
            _context.SaveChanges();        
        }

        public async Task ActivateUser(string Id)
        {
            var dbUser = await _userManager.FindByIdAsync(Id);
            dbUser.Status = "Active";
            _context.SaveChanges();
        }

        public void DeactivateEvent(int Id)
        {
            var dbEvent = _context.Events.Single(m => m.Id == Id);
            dbEvent.Status = "Inactive";
            _context.SaveChanges();
        }

        public void ActivateEvent(int Id)
        {
            var dbEvent = _context.Events.Single(m => m.Id == Id);
            dbEvent.Status = "Active";
            _context.SaveChanges();
        }

        public async Task<bool> EditAdmin(AdminProfileViewModel vm)
        {

            try
            {
                var dbUser = await _userManager.FindByIdAsync(vm.Id);

                if (vm.SuperAdmin == false)
                {
                    await _userManager.RemoveFromRoleAsync(dbUser, "SuperAdmin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(dbUser, "SuperAdmin");
                }

                Mapper.Map(vm, dbUser);
                await _userManager.SetEmailAsync(dbUser, vm.UserName);
                _context.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not get all faculty", ex);
                return false;
            }
            
        }

        public List<StatusCode> GetStatusCodes()
        {
            return _context.StatusCodes.ToList();
        }

        //public string GetEventStatusCodeName(int Id)
        //{
        //    return _context.StatusCodes.Single(m => m.Id == _context.Events.Single(i => i.Id == Id).StatusCodeId).Name;
        //}

        //public string GetFacultyStatusCodeName(string Id)
        //{
        //    return _context.StatusCodes.Single(m => m.Id == _context.Users.Single(i => i.Id == Id).StatusCodeId).Name;
        //}
    }
}
