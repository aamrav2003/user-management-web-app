using IAB_251_Assessment2.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using IAB_251_Assessment2.DataAccess.Data;
using IAB_251_Assessment2.DataAccess.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Entities;
using System.Collections.Generic;

namespace IAB_251_Assessment2.DataAccess.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly UserManagementDBContext myContext;

        public EFUserRepository(UserManagementDBContext context)
        {
            myContext = context;
        }


        public List<User> GetAll() => myContext.Users.ToList();

        public User GetById(int id) => myContext.Users.Find(id);

        public void Add(User user)
        {
            myContext.Users.Add(user);
            myContext.SaveChanges();
        }

        public void Update(User user)
        {
            myContext.Entry(user).State = EntityState.Modified;
            myContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = myContext.Users.Find(id);
            if (user != null)
            {
                myContext.Users.Remove(user);
                myContext.SaveChanges();
            }
        }
    }
}
