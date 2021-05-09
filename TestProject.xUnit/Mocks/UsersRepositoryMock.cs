using System.Collections.Generic;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;

namespace TestProject.xUnit.Mocks
{
    public class UsersRepositoryMock: IUsersRepository
    {
        public ApplicationUser GetFirstFromDb()
        {
            throw new System.NotImplementedException();
        }

        public string GetIdByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public ApplicationUser GetUserById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUserById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public List<ApplicationUser> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void BanByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void UnbanByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}