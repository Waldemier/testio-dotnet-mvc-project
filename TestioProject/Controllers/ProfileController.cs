using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using TestioProject.PL;

namespace TestioProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using TestioProject.PL.Models;

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
        private readonly ILogger<ProfileController> _logger;
        private readonly DataManager dataManager;
        private readonly ServicesManager servicesManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public ProfileController(ILogger<ProfileController> logger, DataManager dataManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._logger = logger;
            this.servicesManager = new ServicesManager(dataManager);
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        public IActionResult Index()
        {
            var x = User.Identity.Name;
            string Id = userManager.FindByEmailAsync(User.Identity.Name).Result.Id;
            UserViewModel model = servicesManager.Users.GetUserModelFromDbToViewById(Id);
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                servicesManager.Users.UpdateModel(model);
                await signInManager.SignOutAsync();
                return Redirect("/");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteAccountModalWindow()
        {
            ViewBag.UserId = userManager.FindByEmailAsync(User.Identity.Name).Result.Id;
            return PartialView("_DeleteAccountPartial");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            servicesManager.Users.DeleteUserById(userId);
            await signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
