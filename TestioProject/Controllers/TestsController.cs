using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using TestioProject.PL;
using TestioProject.PL.Models;
using static TestioProject.PL.Enums.Common;
using static TestioProject.PL.Models.AnswerModel;

namespace TestioProject.Controllers
{
    [Authorize]
    public class TestsController : Controller
    {
        private readonly ILogger<TestsController> _logger;
        private readonly DataManager dataManager;
        private readonly ServicesManager servicesManager;

        public TestsController(ILogger<TestsController> logger, DataManager _dataManager)
        {
            _logger = logger;
            dataManager = _dataManager;
            servicesManager = new ServicesManager(dataManager);
        }

        [HttpGet]
        //[AllowAnonymous]
        public IActionResult Index()
        {
            _logger.LogInformation("Index tests controller action");
            List<TestViewModel> _models = servicesManager.Tests.GetTestsList();
            return View(_models);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult PrivateChooser()
        {
            _logger.LogInformation("PrivateChooser action");
            return View("PrivateChooser");
        }

        [HttpGet]
        public IActionResult ViewTest(int testId)
        {
            _logger.LogInformation("ViewTest action");
            TestViewModel _model = servicesManager.Tests.TestFromDbToViewModelById(testId);
            return View(_model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult TestPassing(int testId, int currentQuestionIndex, int previousesQuestionCorrectAnswersAmount, QuestionEditModel _model, ActionType actionType)
        {
            _logger.LogInformation("TestPassing(post) action");

            List<QuestionViewModel> _listQuestionModels = servicesManager.Questions.GetAllViewQuestionsByTestId(_model.testId);
            ViewData["TestTitle"] = servicesManager.Tests.TestFromDbToViewModelById(_model.testId).Title;
            ViewBag.testId = testId != 0 ? testId : _model.testId;
            
            ViewBag.Questions = _listQuestionModels;

            switch (actionType)
            {
                case ActionType.Start:
                    _logger.LogInformation("TestPassing action: Switch Start");

                    ViewBag.QuestionIndex = 0;
                    return View();

                case ActionType.Next:
                    _logger.LogInformation("TestPassing action: Switch Next");

                    int currentQuestionCorrectAnswersCounter = 0;
                    for(int i = 0; i < _listQuestionModels[currentQuestionIndex].Answers.Count; i++)
                    {
                        var currentAnswerChecked = _model.Answers[i].isTruth;
                        if (_listQuestionModels[currentQuestionIndex].Answers[i].isTruth && currentAnswerChecked)
                        {
                            currentQuestionCorrectAnswersCounter++;
                        }
                    }
                    int totalCorrectAnswersAmount = currentQuestionCorrectAnswersCounter + previousesQuestionCorrectAnswersAmount;
                    ViewBag.TotalCorrectAnswers = totalCorrectAnswersAmount;

                    int nextQuestionIndex = currentQuestionIndex + 1;
                    if(nextQuestionIndex > _listQuestionModels.Count - 1)
                    {
                        ViewBag.Finish = true;
                        
                        ModelState.Clear();
                        return View();
                    }
                    ViewBag.QuestionIndex = nextQuestionIndex;
                    ModelState.Clear();
                    return View();

                case ActionType.Cancel:
                    _logger.LogInformation("TestPassing action: Switch Cancel");

                    return Redirect("/Tests");

                default:
                    return Redirect("~/");
            }
            
        }

        [HttpPost]
        [Authorize]
        public IActionResult TestPassFinished(int testId, int previousesQuestionCorrectAnswersAmount)
        {
            _logger.LogInformation("TestPassFinished(post) action");

            string userName = User.Identity.Name;
            string userId = dataManager.Users.GetIdByEmail(userName);
            StatiscticViewModel _statiscticViewModel = new StatiscticViewModel() { testId = testId, Result = previousesQuestionCorrectAnswersAmount };
            servicesManager.Statistics.SaveStatisticEditModelIntoDb(new StatisticEditModel() { userId = userId, testId = testId, Result = previousesQuestionCorrectAnswersAmount });

            var testTitle = servicesManager.Tests.TestFromDbToViewModelById(testId);
            var questionsAmount = servicesManager.Questions.GetAllViewQuestionsByTestId(testId).Count;

            double devide = ((double)_statiscticViewModel.Result / (double)questionsAmount);
            var percentage = devide * 100;

            ViewBag.TestTitle = testTitle.Title;
            ViewBag.QuestionsAmount = questionsAmount;
            ViewBag.PercentEquivalent = percentage;

            return View(_statiscticViewModel);
        }

        //TODO: edit form (response test data into view)
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult CreateOrEditTest(int testId = 0, string isPrivate = "false")
        {
            _logger.LogInformation("CreateOrEditTest action");

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
        [Authorize(Roles = "Teacher")]
        public RedirectToActionResult CreateOrEditTest(TestEditModel _model)
        {
            _logger.LogInformation("CreateOrEditTest(post) action");

            string owner = User.Identity.Name;
            int testId = servicesManager.Tests.SaveTestFromViewIntoDb(_model, owner);
            return RedirectToAction("CreateOrEditQuestion", new { testId });
        }

        //TODO : 10.04 customize pages (+congrats buttons), test action, finalize (fix amout, add more/next)
        [Authorize(Roles = "Teacher")]
        public IActionResult CreateOrEditQuestion(int testId, QuestionEditModel _questionModel, QuestionsActionType actionType, int answerIdToDelete)
        {

            /* 
                Method for create/edit test.
                Functional : 
                    - add more answer, 
                    - delete selected answer, 
                    - create more question (NextQuestion case),
                    - take a step to previous question created,
                    - take a step to forward question created (NextQuestion case),
                    - remove selected answer,
                    - remove selected question 
                Used: 
                    - Enums constant for option identification (path to file PL/Enums/Common)
                    - Tests and Questions services
            */

            _logger.LogInformation("CreateOrEditQuestion action");

            ViewBag.testId = testId;
            bool detected = false;
            /*the value may be null when we have just created a test and gone to the questions page*/
            if (_questionModel.Answers != null)
            {
                foreach (var item in _questionModel.Answers)
                {
                    if (item.Name == null)
                    {
                        detected = true;
                    }
                }
            }
            
            switch (actionType)
            {
                case QuestionsActionType.AddAnswer:
                    _logger.LogInformation("CreateOrEditQuestion action: Switch AddAnswer");

                    _questionModel.Answers.Add(new AnswerEditModel() { });
                    break;
                case QuestionsActionType.NextQuestion:
                    _logger.LogInformation("CreateOrEditQuestion action: Switch NextQuestion");
                    //if any answer is empty - detected and throw error on frontend

                    QuestionEditModel _lastModelInList = servicesManager.Questions.GetLastQuestionFromTestQuestionsList(testId);
                    if (_questionModel.Name != null && _questionModel.questionId != 0 && /*not last in list*/ _lastModelInList.questionId != _questionModel.questionId)
                    {
                        if (detected)
                        {
                            ViewBag.Error = "Answer cannot be empty. Input or delete it.";
                        }
                        else
                        {
                            QuestionEditModel _AheadQuestion = servicesManager.Questions.GetQuestionThatIsOneStepAheadFromTestQuestionsList(_questionModel, testId);
                            servicesManager.Questions.SaveQuestionFromViewIntoDb(_questionModel);
                            _questionModel = _AheadQuestion;
                        }
                    }
                    else if(_questionModel.Name == null)
                    {
                        ViewBag.Error = "Question cannot be empty";
                    }
                    else
                    {
                        if (detected)
                        {
                            ViewBag.Error = "Answer cannot be empty. Input or delete it.";
                        }
                        else
                        {
                            servicesManager.Questions.SaveQuestionFromViewIntoDb(_questionModel);
                            _questionModel.Name = null;
                            _questionModel.questionId = 0;
                            _questionModel.Answers = null;
                        }
                    }
                    ModelState.Clear(); //For clear razor state
                    break;
                case QuestionsActionType.RemoveAnswer:
                    _logger.LogInformation("CreateOrEditQuestion action: Switch RemoveAnswer");

                    AnswerEditModel _answerToDelete = _questionModel.Answers[answerIdToDelete];
                    if(_questionModel.Answers.Count > 1) //if this input not last
                    {
                        if (_questionModel.questionId != 0)
                        {
                            servicesManager.Questions.DeleteAnswer(_answerToDelete);
                        }
                        _questionModel.Answers.Remove(_answerToDelete);
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Error = "You cannot delete the last answer";
                    }
                    break;
                case QuestionsActionType.PreviousQuestion:
                    _logger.LogInformation("CreateOrEditQuestion action: Switch PreviousQuestion");

                    if (_questionModel.questionId == 0 && _questionModel.Name != null)
                    {
                        /*
                         * if the current question have non-empty inputs
                        */
                        QuestionEditModel _lastQuestion = servicesManager.Questions.GetLastQuestionFromTestQuestionsList(testId);
                        servicesManager.Questions.SaveQuestionFromViewIntoDb(_questionModel);
                        _questionModel = _lastQuestion;
                    }
                    else if(_questionModel.questionId != 0) //If question has been already exist (in db)
                    {
                        /*
                         * if the current question has been exist in db (for edit from or if we are already there in previouses questions in test creating form)
                         * logic : indexOf currentQeustuoion in questions by testId and take a step back
                        */

                        List<QuestionEditModel> _CurrentTestQuestions = servicesManager.Questions.GetAllEditQuestionsByTestId(testId);
                        int _CurrentQuestionId = servicesManager.Questions.GetIndexOfSpecifierQuestionById(testId, _questionModel.questionId);
                        QuestionEditModel _previousQuestion = _CurrentTestQuestions[_CurrentQuestionId - 1];
                        _questionModel = _previousQuestion;
                    }
                    else
                    {
                        /*
                         * if current question creating inputs are empty and we need to take step to back
                        */
                        QuestionEditModel _lastQuestion = servicesManager.Questions.GetLastQuestionFromTestQuestionsList(testId);
                        _questionModel = _lastQuestion;
                    }
                    ModelState.Clear();
                    break;
                case QuestionsActionType.RemoveQuestion:
                    _logger.LogInformation("CreateOrEditQuestion action: Switch RemoveQuestion");

                    servicesManager.Questions.RemoveEditQuestionFromDb(_questionModel);
                    QuestionEditModel lastQuestionModel = servicesManager.Questions.GetLastQuestionFromTestQuestionsList(testId);
                    _questionModel = lastQuestionModel != null ? lastQuestionModel : new QuestionEditModel() { testId = testId };
                    ModelState.Clear();
                    break;
                case QuestionsActionType.Finish:
                    if(_questionModel.Name != null)
                    {
                        if (detected)
                        {
                            ViewBag.Error = "Answer cannot be empty. Input or delete it.";
                        }
                        else
                        {
                            servicesManager.Questions.SaveQuestionFromViewIntoDb(_questionModel);
                            return RedirectToAction("FinishTestCreating", new { testId });
                        }
                    }
                    ViewBag.Error = "Fields must be filled";
                    break;
                default:
                    break;
            }
            int questionsAmount = servicesManager.Questions.GetAllEditQuestionsByTestId(testId).Count;
            ViewBag.QuestionsAmount = questionsAmount;
            int currentQuestionIndexInList = servicesManager.Questions.GetIndexOfSpecifierQuestionById(testId, _questionModel.questionId);
            ViewBag.CurrentQuestionIndex = currentQuestionIndexInList;
            return View(_questionModel);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult FinishTestCreating(int testId)
        {
            _logger.LogInformation("FinishTestCreating action");

            ViewData["testId"] = testId;
            return View("FinishTestCreating");
        }
    }
}
