using IAB_251_Assessment2.BusinessLogic.Entities;

namespace IAB_251_Assessment2.Application.Interfaces
{
    public interface IUserRegistrationAppService
    {
        List<User> GetAllUsers();
        User GetUser(int id);

        void AddUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int id);


    }
}
