using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using TestioProject.PL;
using TestioProject.PL.Models;
using static TestioProject.PL.Enums.Common;

namespace TestioProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /*
         * Admin panel
         * 
         * TODO:
         * 
         * Get all users
         * Get all tests
         * Get all teachers
         * Get all reports
         * Temporary limited specific user
         * Delete specific test
        */
        private readonly DataManager dataManager;
        private readonly ServicesManager servicesManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(DataManager _dataManager, UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
            dataManager = _dataManager;
            servicesManager = new ServicesManager(dataManager);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Requests()
        {
            List<WrittenLetterModel> _list = servicesManager.WrittenLetter.GetAll();
            ViewData["AdminEmail"] = User.Identity.Name;
            return View(_list);
        }

        [HttpPost]
        public IActionResult UserInfo(string userId, string exp, string reason)
        {
            var _user = dataManager.Users.GetUserById(userId);
            WrittenLetterModel _model = new WrittenLetterModel() {User = _user, Experience = exp, Reason = reason};
            return View(_model);
        }

        [HttpPost]
        public IActionResult TeacherRoleConfirm(string userId, ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.Confirm:
                    ApplicationUser user = dataManager.Users.GetUserById(userId);
                    userManager.RemoveFromRoleAsync(user, "Learner").Wait();
                    userManager.AddToRoleAsync(user, "Teacher").Wait();
                    dataManager.WrittenLetters.DeleteWriteLetterFromDb(userId);
                    break;
                case ActionType.Reject:
                    dataManager.WrittenLetters.DeleteWriteLetterFromDb(userId);
                    break;
                default:
                    break;
            }

            ModelState.Clear();
            return RedirectToRoute(new {action = "Requests"});
        }

        // [HttpGet]
        // [Route("/Admin/AdminPanel")]
        // public IActionResult AdminControllPanel()
        // {
        //     return View();
        // }
    }
}
