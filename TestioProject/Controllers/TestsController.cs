using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestioProject.BLL;
using TestioProject.PL;
using TestioProject.PL.Models;

namespace TestioProject.Controllers
{
    public class TestsController : Controller
    {
        private DataManager dataManager;
        private ServicesManager servicesManager;

        public TestsController(DataManager _dataManager)
        {
            dataManager = _dataManager;
            servicesManager = new ServicesManager(dataManager);
        }
        public IActionResult Index()
        {
            List<TestViewModel> _models = servicesManager.Tests.GetTestsList();
            return View(_models);
        }

        [HttpGet]
        public IActionResult CreateOrEditTest(int testId = 0, string isPrivate = "false")
        {
            TestEditModel _model;
            if (testId != 0)
                _model = servicesManager.Tests.GetTestEditModel(testId);
            else
                _model = servicesManager.Tests.GetTestEditModel();

            ViewBag.testId = testId;
            ViewBag.isPrivate = bool.Parse(isPrivate);
            return View(_model);
        }
        
        [HttpPost]
        public RedirectToActionResult CreateOrEditTest(TestEditModel _model)
        {
            string owner = User.Identity.Name;
            int testId = servicesManager.Tests.SaveTestFromViewIntoDb(_model, owner);
            return RedirectToAction("CreateOrEditQuestion", new { testId });
        }

        //[HttpPost]
        public IActionResult CreateOrEditQuestion(int testId, IFormCollection collection)
        {
            ViewBag.testId = testId;
            ViewBag.AnswersAmount = collection["answer"].Count + 1;
            return View("CreateOrEditQuestion");
        }

        [HttpGet]
        public IActionResult PrivateChooser()
        {
            return View("PrivateChooser");
        }
    }
}
