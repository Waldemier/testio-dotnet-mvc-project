namespace TestioProject.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using TestioProject.BLL;
    using TestioProject.DAL.Models;
    using TestioProject.PL;
    using TestioProject.PL.Models;
    using static TestioProject.PL.Enums.Common;

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

        private readonly ILogger<AdminController> _logger;
        private readonly DataManager dataManager;
        private readonly ServicesManager servicesManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(ILogger<AdminController> logger, DataManager _dataManager, UserManager<ApplicationUser> _userManager)
        {
            _logger = logger;
            userManager = _userManager;
            dataManager = _dataManager;
            servicesManager = new ServicesManager(dataManager);
        }

        public IActionResult Index(bool searched = false, List<string> Ids = null)
        {
            _logger.LogInformation("Admin panel page");
            List<UserViewModel> models = new List<UserViewModel>();
            if (searched)
            {
                var temp = servicesManager.Users.GetAllUsersViewModelsFromDb();
                foreach (var Id in Ids)
                {
                    foreach (var user in temp)
                    {
                        if (user.Id == Id)
                        {
                            models.Add(user);
                        }
                    }
                }
            }
            else
            {
                models = servicesManager.Users.GetAllUsersViewModelsFromDb().ToList();
            }
            return View(models);
        }

        public IActionResult MoreUserDetail(string userId)
        {
            _logger.LogInformation("More User Detail page");
            return View();
        }

        [HttpPost]
        public IActionResult Ban(string userId)
        {
            servicesManager.Users.BanByUserId(userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Unban(string userId)
        {
            servicesManager.Users.UnbanByUserId(userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Search(string querySearch)
        {
            if (querySearch != null)
            {
                string[] search = querySearch.Split(" ");
                List<string> users = new List<string>();
                if (search.Length > 1)
                {
                    users = servicesManager.Users.GetAllUsersViewModelsFromDb().Where(x =>
                        x.FirstName == search[0] || x.FirstName == search[1] || x.LastName == search[0] ||
                        x.LastName == search[1]).Select(x => x.Id).ToList();
                }
                else
                {
                    users = servicesManager.Users.GetAllUsersViewModelsFromDb().Where(x =>
                        x.FirstName == search[0] || x.LastName == search[0]).Select(x => x.Id).ToList();
                }
                
                return RedirectToAction(nameof(Index), new { searched = true, Ids = users });
            }

            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult Requests()
        {
            _logger.LogInformation("Requests action");

            List<WrittenLetterModel> _list = servicesManager.WrittenLetter.GetAll();
            ViewData["AdminEmail"] = User.Identity.Name;
            return View(_list);
        }

        [HttpPost]
        public IActionResult UserInfo(string userId, string exp, string reason)
        {
            _logger.LogInformation("UserInfo action");

            var _user = dataManager.Users.GetUserById(userId);
            WrittenLetterModel _model = new WrittenLetterModel() {User = _user, Experience = exp, Reason = reason};
            return View(_model);
        }

        [HttpPost]
        public IActionResult TeacherRoleConfirm(string userId, ActionType actionType)
        {
            _logger.LogInformation("TeacherRoleConfirm(post) action");

            switch (actionType)
            {
                case ActionType.Confirm:
                    _logger.LogInformation("TeacherRoleConfirm(post) action: Switch: Confirm");

                    ApplicationUser user = dataManager.Users.GetUserById(userId);
                    userManager.RemoveFromRoleAsync(user, "Learner").Wait();
                    userManager.AddToRoleAsync(user, "Teacher").Wait();
                    dataManager.WrittenLetters.DeleteWriteLetterFromDb(userId);
                    break;
                case ActionType.Reject:
                    _logger.LogInformation("TeacherRoleConfirm(post) action: Switch: Reject");

                    dataManager.WrittenLetters.DeleteWriteLetterFromDb(userId);
                    break;
                default:
                    _logger.LogInformation("TeacherRoleConfirm(post) action: Switch: Default");
                    break;
            }

            ModelState.Clear();
            return RedirectToRoute(new {action = "Requests"});
        }

    }
}
