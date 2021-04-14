using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestioProject.Controllers
{
    /*
         * Profile
         * Check own statistic (History)
         * Check statistic own test - if role teacher (Tests statistics) + delete test
         * Edit
         * Delete account
    */
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
