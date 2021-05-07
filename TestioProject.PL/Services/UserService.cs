using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using TestioProject.PL.Models;

namespace TestioProject.PL.Services
{
    public class UserService
    {
        private readonly DataManager dataManager;

        public UserService(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public UserViewModel GetUserModelFromDbToViewById(string Id)
        {
            ApplicationUser user = dataManager.Users.GetUserById(Id);
            UserViewModel model = new UserViewModel() { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, AvatarUri = user.AvatarUri, Email = user.Email };
            return model;
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
