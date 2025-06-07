using IAB_251_Assessment2.BusinessLogic.Entities;
using IAB_251_Assessment2.DataAccess.Interfaces;

namespace IAB_251_Assessment2.DataAccess.Repositories
{
    public class InMemoryUserDB : IUserRepository
    {
        private static readonly List<User> _users = new();
        private static int _nextId = 1;

        public List<User> GetAll() => _users;

        public User GetById(int id) => _users.FirstOrDefault(v => v.Id == id);

        public void Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public void Update(User user)
        {
            var existing = GetById(user.Id);
            if (existing != null)
            {
                existing.FirstName = user.FirstName;
                existing.FamilyName = user.FamilyName;
                existing.EmailAddress = user.EmailAddress;
                existing.PhoneNumber = user.PhoneNumber;
                existing.CompanyName = user.CompanyName;
                existing.Address = user.Address;
                existing.Password = user.Password;    
            }
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }
    }
}
