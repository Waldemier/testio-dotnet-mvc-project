namespace TestioProject.PL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TestioProject.BLL;
    using TestioProject.DAL.Models;
    using TestioProject.PL.Models;

    public class UserService
    {
        private readonly IDataManager dataManager;

        public UserService(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public UserViewModel GetUserModelFromDbToViewById(string Id)
        {
            ApplicationUser user = dataManager.Users.GetUserById(Id);
            UserViewModel model = new UserViewModel() { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, AvatarUri = user.AvatarUri, Email = user.Email, Baned = user.Baned };
            return model;
        }

        public IEnumerable<UserViewModel> GetAllUsersViewModelsFromDb()
        {
            var users = dataManager.Users.GetAll().Select(user => new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AvatarUri = user.AvatarUri,
                Baned = user.Baned
            }).ToList();
            
            return users;
        }

        public void BanByUserId(string userId)
        {
            dataManager.Users.BanByUserId(userId);
        }

        public void UnbanByUserId(string userId)
        {
            dataManager.Users.UnbanByUserId(userId);
        }
        
        public void DeleteUserById(string userId)
        {
            dataManager.Users.DeleteUserById(userId);
        }

        public void UpdateModel(UserViewModel model)
        {
            var user = dataManager.Users.GetUserById(model.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.NormalizedUserName = model.Email.ToUpper();
            user.NormalizedEmail = model.Email.ToUpper();
            user.AvatarUri = model.AvatarUri;
            
            dataManager.Users.Update(user);
        }
    }
}
