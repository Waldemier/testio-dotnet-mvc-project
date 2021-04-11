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

        //TODO : 10.04 customize pages (+congrats buttons), test action, finalize (fix amout, add more/next)
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
                    _questionModel.Answers.Add(new AnswerEditModel() { });
                    break;
                case QuestionsActionType.NextQuestion:
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
