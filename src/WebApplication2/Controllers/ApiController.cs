using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using WebApplication2.Models;
using WebApplication2.ViewModels;
using AutoMapper;
using System;
using Microsoft.Extensions.Logging;

namespace WebApplication2.Controllers
{
    [Produces("application/json")]
    public class ApiController : Controller
    {
        private TffFacultyContext _context;
        private ITffFacultyRepository _repository;
        private ILogger<TffFacultyRepository> _logger;

        public ApiController(TffFacultyContext context, ITffFacultyRepository repository, ILogger<TffFacultyRepository> logger)
        {
            _context = context;
            _repository = repository;
            _logger = logger;
        }

        [Produces("application/json")]
        //[HttpGet("Api/GetFaculty")]
        //[Route("api/GetFaculty")]
        public IActionResult GetFaculty()
        {
            var db = _repository.GetAllFaculty();
            var vm = new List<FacultyProfileApiModel>();

            foreach (var x in db)
            {
                var viewModel = Mapper.Map<FacultyProfileApiModel>(x);
                viewModel.DisclosureComplete = DisclosureComplete(x);
                viewModel.ProfileComplete = ProfileComplete(x);
                vm.Add(viewModel);
            }

            return Ok(vm);
        }

        [Produces("application/json")]
        public async Task<IActionResult> GetFacultyOne(string Id)
        {
            var db = await _repository.GetFaculty(Id);
            var vm = Mapper.Map<HomeIndexFacultyViewModel>(db);

            return Ok(vm);
        }

        public async Task<IActionResult> UpdateFacultyOne([FromBody]HomeIndexFacultyViewModel data)
        {
            try
            {
                var db = await _repository.GetFacultyByUserName(data.UserName);
                db.FirstName = data.FirstName;
                db.LastName = data.LastName;
                db.Degree = data.Degree;
                db.Affiliation = data.Affiliation;
                db.StreetAddress = data.StreetAddress;
                db.City = data.City;
                db.State = data.State;
                db.Zip = data.Zip;
                db.Country = data.Country;
                db.BusinessPhone = data.BusinessPhone;
                _repository.SaveAll();
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not update faculty user", ex);
                return new HttpStatusCodeResult(500);
            }

            return Ok();
        }

        [Produces("application/json")]
        //[HttpGet("Api/GetEvents")]
        //[Route("api/GetEvents")]
        public IActionResult GetEvents()
        {
            var db = _repository.GetAllEvents();
            var vm = Mapper.Map<List<EventViewModel>>(db);

            return Ok(vm);
        }

        public IActionResult GetEventsForAdmin(string Id)
        {
            var db = _repository.GetAllEvents(Id);
            var vm = Mapper.Map<List<EventViewModel>>(db);

            return Ok(vm);
        }

        private bool DisclosureComplete(User user)
        {
            if (user.Disclosures == null)
            {
                return false;
            }

            if (user.Disclosures.ToList().Count < 1)
            {
                return false;
            }

            var disclosure = user.Disclosures.Where(m => m.IsBackup == false).Single();

            if (disclosure.CreateDate.AddMonths(12) <= DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ProfileComplete(User user)
        {
            if (user.BirthDate == null ||
                user.FirstName == null ||
                user.LastName == null ||
                user.OfficialName == null)
            {
                return false;
            }
            return true;
        }
    }
}