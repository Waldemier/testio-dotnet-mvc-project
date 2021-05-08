using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;
using TestioProject.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace TestioProject.BLL.Implementations
{
    public class EFUsersRepository: IUsersRepository
    {
        private readonly TestioDbContext context;
        public EFUsersRepository(TestioDbContext _context)
        {
            context = _context;
        }
        public ApplicationUser GetFirstFromDb()
        {
            return context.Users.First();
        }

        public List<ApplicationUser> GetAll()
        {
            return context.Users.ToList();
        }
        
        public ApplicationUser GetUserById(string userId)
        {
            ApplicationUser _user = context.Users.Where(x => x.Id == userId).FirstOrDefault();
            return _user;
        }

        public void BanByUserId(string userId)
        {
            context.Users.FirstOrDefault(x => x.Id == userId).Baned = true;
            context.SaveChanges();
        }
        
        public void UnbanByUserId(string userId)
        {
            context.Users.FirstOrDefault(x => x.Id == userId).Baned = false;
            context.SaveChanges();
        }
        
        public void DeleteUserById(string userId)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            context.Users.Remove(user);
            context.SaveChanges();
        }    
        
        public void Update(ApplicationUser user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public string GetIdByEmail(string email)
        {
            return context.Users.Where(x => x.UserName == email).FirstOrDefault().Id.ToString();
        }

    }
}
