﻿using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;

namespace TestioProject.BLL.Implementations
{
    public class EFRestrictedRepository: IRestrictedRepository
    {
        private readonly TestioDbContext context;
        public EFRestrictedRepository(TestioDbContext _context)
        {
            context = _context;
        }
    }
}