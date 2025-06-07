using IAB_251_Assessment2.BusinessLogic.Entities;

namespace IAB_251_Assessment2.BusinessLogic.Interfaces
{
    public interface IUserRegistrationService
    {

        List<User> GetAll();
        User GetById(int id);

        void Add(User user);

        void Update(User user);

        void Delete(int id);
    }
}
