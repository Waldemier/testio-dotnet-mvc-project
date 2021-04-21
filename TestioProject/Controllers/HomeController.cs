using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestioProject.BLL;
using TestioProject.DAL.Data;
using TestioProject.DAL.Models;
using TestioProject.Models;
using TestioProject.PL;
using TestioProject.PL.Models;

namespace TestioProject.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DataManager dataManager;
        private readonly ServicesManager servicesManager;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> _userManager, DataManager _dataManager)
        {
            _logger = logger;
            userManager = _userManager;
            dataManager = _dataManager;
            servicesManager = new ServicesManager(dataManager);
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            _logger.LogInformation("You are on the main page");
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Learner")]
        public async Task<IActionResult> BecomeATeacher()
        {
            _logger.LogInformation("BecomeATeacher action");

            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            ViewBag.userId = user.Id;
            return View("BecomeATeacher");
        }

        [HttpPost]
        [Authorize(Roles = "Learner")]
        public RedirectResult RequestToBecomeATeacher(WrittenLetterModel _model)
        {
            _logger.LogInformation("RequestToBecomeATeacher(post) action");

            servicesManager.WrittenLetter.SaveWrittenLetterModelIntoDb(_model);
            return Redirect("/");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
