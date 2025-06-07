using IAB_251_Assessment2.Application.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Entities;
using IAB_251_Assessment2.BusinessLogic.Interfaces;

namespace IAB_251_Assessment2.Application.Services
{
    public class UserRegistrationAppService : IUserRegistrationAppService
    {

        private readonly IUserRegistrationService _userService;

        public UserRegistrationAppService(IUserRegistrationService userService)
        {
            _userService = userService;
        }

        public List<User> GetAllUsers() => _userService.GetAll();

        public User GetUser(int id) => _userService.GetById(id);

        public void AddUser(User user) => _userService.Add(user);

        public void UpdateUser(User user) => _userService.Update(user);

        public void DeleteUser(int id) => _userService.Delete(id);
    }
}
