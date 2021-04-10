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

namespace TestioProject.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        //private readonly TestioDbContext context;
        private readonly DataManager dataManager;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> _userManager, DataManager _dataManager)
        {
            _logger = logger;
            userManager = _userManager;
            dataManager = _dataManager;
        }

        public IActionResult Index()
        {

            //string userId = dataManager.Users.GetFirstFromDb().Id;
            //dataManager.Tests.SaveTest(new Test() { Name = "Test1", Description = "Some desc1", UserId = userId, CodeLock = "12345" });

            //dataManager.Questions.SaveQuestion(new Question() { Name = "What color is sky ?", TestId = 6 });
            //dataManager.Questions.SaveQuestion(new Question() { Name = "In what year was Lviv founded ?", TestId = 6 });

            //List<Question> questions = dataManager.Questions.GetAllQuestions().ToList();

            //dataManager.Answers.SaveAnswer(new Answer() { Text = "Blue", isTruth = true, QuestionId = questions[0].Id });
            //dataManager.Answers.SaveAnswer(new Answer() { Text = "Dark", isTruth = false, QuestionId = questions[0].Id });

            //dataManager.Answers.SaveAnswer(new Answer() { Text = "1256", isTruth = true, QuestionId = questions[1].Id });
            //dataManager.Answers.SaveAnswer(new Answer() { Text = "1154", isTruth = false, QuestionId = questions[1].Id });


            //Test test = context.Tests.Include(x => x.Questions).First();
            //Question question = context.Questions.Include(x => x.Answers).Where(x => x.TestId == test.Id).FirstOrDefault();
            //Test test = dataManager.Tests.GetTestById(6);
            //List<Question> questions = dataManager.Questions.GetQuestionsByTestId(test.Id).ToList();
            //var x = "";
            return View();
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
