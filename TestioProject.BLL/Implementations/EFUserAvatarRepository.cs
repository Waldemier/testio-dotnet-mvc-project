using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;

namespace TestioProject.BLL.Implementations
{
    public class EFUserAvatarRepository: IUserAvatarsRepository
    {
        private readonly TestioDbContext context;
        public EFUserAvatarRepository(TestioDbContext _context)
        {
            context = _context;
        }
    }
}
