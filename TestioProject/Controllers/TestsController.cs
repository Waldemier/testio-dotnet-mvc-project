using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestioProject.BLL;
using TestioProject.PL;
using TestioProject.PL.Models;
using static TestioProject.PL.Enums.Common;
using static TestioProject.PL.Models.AnswerModel;

namespace TestioProject.Controllers
{
    public class TestsController : Controller
    {
        private readonly DataManager dataManager;
        private readonly ServicesManager servicesManager;

        public TestsController(DataManager _dataManager)
        {
            dataManager = _dataManager;
            servicesManager = new ServicesManager(dataManager);
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<TestViewModel> _models = servicesManager.Tests.GetTestsList();
            return View(_models);
        }

        //TODO: edit form (response test data into view)
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

        //TODO : 10.04 Create helper functions, add remove answer options, customize pages (+congrats buttons)
        public IActionResult CreateOrEditQuestion(int testId, QuestionEditModel _questionModel, QuestionsActionType actionType)
        {
            ViewBag.testId = testId;
            switch (actionType)
            {
                case QuestionsActionType.AddAnswer:
                    ViewBag.AnswersAmount = _questionModel.Answers.Count + 1;
                    _questionModel.Answers.Add(new AnswerEditModel() { });
                    break;
                case QuestionsActionType.NextQuestion:
                    servicesManager.Questions.SaveQuestionFromViewIntoDb(_questionModel);
                    _questionModel.Name = null;
                    _questionModel.Answers = null;
                    ModelState.Clear(); //For clear razor state
                    break;
                case QuestionsActionType.RemoveAnswer:
                    break;
                case QuestionsActionType.RemoveQuestion:
                    break;
                case QuestionsActionType.Finish:
                    servicesManager.Questions.SaveQuestionFromViewIntoDb(_questionModel);
                    return RedirectToAction("FinishTestCreating", new { testId });
                default:
                    break;
            }       
            return View(_questionModel);
        }

        [HttpGet]
        public IActionResult FinishTestCreating(int testId)
        {

            ViewData["testId"] = testId;
            return View("FinishTestCreating");
        }

        [HttpGet]
        public IActionResult PrivateChooser()
        {
            return View("PrivateChooser");
        }
    }
}
