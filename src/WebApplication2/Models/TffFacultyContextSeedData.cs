using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class TffFacultyContextSeedData
    {
        private readonly TffFacultyContext _context;
        private readonly UserManager<User> _usermanager;
        private IOptions<AppSettings> _appSettings;

        public TffFacultyContextSeedData(TffFacultyContext context, UserManager<User> userManager, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _usermanager = userManager;
            _appSettings = appSettings;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (!_context.StatusCodes.Any())
            {
                _context.StatusCodes.Add(new StatusCode() { Name = "Active" });
                _context.SaveChanges();
                _context.StatusCodes.Add(new StatusCode() { Name = "Inactive" });
                _context.SaveChanges();
            }

            if (!_context.Roles.Any(m => m.Name == "Admin"))
            {
                _context.Roles.Add(new IdentityRole() { Name = "Admin", NormalizedName = "Admin" });
                _context.Roles.Add(new IdentityRole() { Name = "SuperAdmin", NormalizedName = "SuperAdmin" });
                _context.Roles.Add(new IdentityRole() { Name = "Faculty",NormalizedName = "Faculty" });
                _context.SaveChanges();
            }

            if (!_context.FacultyRoles.Any())
            {
                List<string> facultyRoles = _appSettings.Value.FacultyRoles.Split(',').ToList();
                foreach(var role in facultyRoles)
                {
                    if(!_context.Roles.Any(m=>m.Name == role))
                    {
                        _context.FacultyRoles.Add(new FacultyRole() { Name = role });
                    }
                    _context.SaveChanges();
                }
            }

            if (!_context.Specialties.Any())
            {
                List<string> specialties = _appSettings.Value.Specialties.Split(',').ToList();
                foreach (var s in specialties)
                {
                    if (!_context.Specialties.Any(m => m.SpecialtyTitle == s))
                    {
                        _context.Specialties.Add(new Specialty { SpecialtyTitle = s });
                    }
                    _context.SaveChanges();
                }
            }

            if (await _usermanager.FindByEmailAsync("facultyadministrator@francefoundation.com") == null)
            {
                var newAdminUser = new User()
                {
                    UserName = "facultyadministrator@francefoundation.com",
                    Email = "facultyadministrator@francefoundation.com",
                    FirstName = "Paul",
                    LastName = "Benetis",
                    EmailConfirmed = true,
                    Status = "Active"
                };
                var x = await _usermanager.CreateAsync(newAdminUser, _appSettings.Value.DefaultPassword);
                if (x.Succeeded)
                {
                    await _usermanager.AddToRoleAsync(newAdminUser, "Admin");
                    await _usermanager.AddToRoleAsync(newAdminUser, "SuperAdmin");
                }
            }
        }
    }
}
