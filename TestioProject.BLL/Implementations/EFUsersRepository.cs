using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;
using TestioProject.DAL.Models;

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
    }
}
