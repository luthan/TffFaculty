using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.ViewModels;

namespace WebApplication2.Models
{
    public interface ITffFacultyRepository
    {
        List<Event> GetAllEvents();
        List<Event> GetAllEvents(string adminId);
        Event GetEvent(int id);
        bool AddEvent(Event newEvent);
        bool UpdateEvent(EventViewModel updatedEvent);

        IEnumerable<User> GetAllUsers();
        //void AddUser(User newUser);

        bool SaveAll();

        List<EventFaculty> GetEventAllFaculty(int id);

        Task<bool> AddEventFaculty(EventFaculty eu);

        List<FacultyRole> GetAllFacultyRoles();
        void AddFacultyRole(string name);
        Task<IdentityResult> CreateUser(User user);
        EventFaculty GetOneEventFaculty(short v, string userId);
        bool CreateDisclosure(Disclosure newDisclosure);
        FacultyRole GetOneRole(int v);
        //void AddDisclosureFacultyRole(EventFacultyRole disclosureFacultyRole);
        List<Disclosure> GetUserDisclosures(User user);
        List<EventFaculty> GetUserEvents(User x);
        List<EventFacultyRole> GetEventFacultyRoles(string userId, int eventId);
        EventFaculty GetEventFaculty(string id);
        bool RemoveEventFacultyRoles(string userId, int eventId);
        void AddEventFacultyRole(string userId, int eventId, int roleId);
        Task<User> GetFaculty(string userId);
        Task<User> GetFacultyByUserName(string userName);
        bool AddEventFacultyRoles(string userId, int eventId, List<string> eventFacultyRoles);
        List<User> GetAllUsersNotInEvent(int id);
        Disclosure GetUserCurrentDisclosure(string name);
        Disclosure GetDisclosureById(int id);
        void UpdateFacultyDisclosure(Disclosure disclosure);
        List<User> GetAllFaculty();
        List<User> GetAllFaculty(string statusCode);
        Task<IdentityResult> CreateFaculty(User user);
        List<User> GetAllAdmins();
        Task<IdentityResult> CreateAdmin(AdminProfileViewModel user);
        List<Specialty> GetSpecialties();
        void AddSpecialty(string name);
        Task DeactivateUser(string Id);
        Task ActivateUser(string Id);
        void DeactivateEvent(int Id);
        void ActivateEvent(int Id);
        Task<bool> EditAdmin(AdminProfileViewModel vm);
        List<StatusCode> GetStatusCodes();
        //string GetEventStatusCodeName(int Id);
        //string GetFacultyStatusCodeName(string Id);
        Task<bool> ResendEventFacultyEmail(string Id);
    }
}