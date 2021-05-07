using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.DAL.Models;

namespace TestioProject.BLL.Interfaces
{

    public interface IUsersRepository
    {
        ApplicationUser GetFirstFromDb();
        string GetIdByEmail(string email);
        ApplicationUser GetUserById(string userId);
        void DeleteUserById(string userId);
        void Update(ApplicationUser user);
    }
}
