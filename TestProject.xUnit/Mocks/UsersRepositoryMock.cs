using System.Collections.Generic;
using System.Linq;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;
using Xunit;

namespace TestProject.xUnit.Mocks
{
    public class UsersRepositoryMock: IUsersRepository
    {
        public static bool IsUserBanned { get; set; }
        public static bool IsUserUnbanned { get; set; }
        
        public List<ApplicationUser> GetAll()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser() { Id = "1", Email = "user1@gmail.com", FirstName = "firstname1", LastName = "lastname1", Baned = false, AvatarUri = "/" },
                new ApplicationUser() { Id = "2", Email = "user2@gmail.com", FirstName = "firstname2", LastName = "lastname2", Baned = true, AvatarUri = "/" }
            };
        }
        
        public ApplicationUser GetUserById(string userId)
        {
            return this.GetAll().FirstOrDefault(x => x.Id == userId);
        }
        
        public void BanByUserId(string userId)
        {
            var user = this.GetAll().FirstOrDefault(x => x.Id == userId);
            if (user != null) user.Baned = true;
            if (user.Baned) IsUserBanned = true;
        }
        
        public void UnbanByUserId(string userId)
        {
            var user = this.GetAll().FirstOrDefault(x => x.Id == userId);
            if (user != null) user.Baned = false;
            if (!user.Baned) IsUserUnbanned = true;
        }
        
        public ApplicationUser GetFirstFromDb()
        {
            throw new System.NotImplementedException();
        }

        public string GetIdByEmail(string email)
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

        
    }
}