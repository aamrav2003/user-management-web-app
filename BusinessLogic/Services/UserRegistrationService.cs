using IAB_251_Assessment2.BusinessLogic.Entities;
using IAB_251_Assessment2.BusinessLogic.Interfaces;
using IAB_251_Assessment2.DataAccess.Interfaces;

namespace IAB_251_Assessment2.BusinessLogic.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAll() => _userRepository.GetAll();

        public User GetById(int id) => _userRepository.GetById(id);

        public void Add(User user) => _userRepository.Add(user);

        public void Update(User user) => _userRepository.Update(user);

        public void Delete(int id) => _userRepository.Delete(id);
    }
}
