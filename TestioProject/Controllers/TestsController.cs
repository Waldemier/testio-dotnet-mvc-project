namespace TestioProject.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using TestioProject.BLL;
    using TestioProject.PL;
    using TestioProject.PL.Models;
    using static TestioProject.PL.Enums.Common;
    using static TestioProject.PL.Models.AnswerModel;

    [Authorize]
    public class TestsController : Controller
    {
        private readonly ILogger<TestsController> logger;
        private readonly DataManager dataManager;
        private readonly ServicesManager servicesManager;

        public TestsController(ILogger<TestsController> logger, DataManager dataManager)
        {
            this.logger = logger;
            this.dataManager = dataManager;
            this.servicesManager = new ServicesManager(this.dataManager);
        }

        [HttpGet]
        // [AllowAnonymous]
        public IActionResult Index()
        {
            this.logger.LogInformation("Index tests controller action");
            List<TestViewModel> models = this.servicesManager.Tests.GetTestsList();
            return this.View(models);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult PrivateChooser()
        {
            this.logger.LogInformation("PrivateChooser action");
            return this.View("PrivateChooser");
        }

        [HttpGet]
        public IActionResult ViewTest(int testId)
        {
            this.logger.LogInformation("ViewTest action");
            TestViewModel model = this.servicesManager.Tests.TestFromDbToViewModelById(testId);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult TestPassing(int testId, int currentQuestionIndex, int previousesQuestionCorrectAnswersAmount, QuestionEditModel model, ActionType actionType)
        {
            this.logger.LogInformation("TestPassing(post) action");

            List<QuestionViewModel> listQuestionModels = this.servicesManager.Questions.GetAllViewQuestionsByTestId(model.testId);
            this.ViewData["TestTitle"] = this.servicesManager.Tests.TestFromDbToViewModelById(model.testId).Title;
            this.ViewBag.testId = testId != 0 ? testId : model.testId;
            this.ViewBag.Questions = listQuestionModels;

            switch (actionType)
            {
                case ActionType.Start:
                    this.logger.LogInformation("TestPassing action: Switch Start");

                    this.ViewBag.QuestionIndex = 0;
                    return this.View();

                case ActionType.Next:
                    this.logger.LogInformation("TestPassing action: Switch Next");

                    int currentQuestionCorrectAnswersCounter = 0;
                    for (int i = 0; i < listQuestionModels[currentQuestionIndex].Answers.Count; i++)
                    {
                        var currentAnswerChecked = model.Answers[i].isTruth;
                        if (listQuestionModels[currentQuestionIndex].Answers[i].isTruth && currentAnswerChecked)
                        {
                            currentQuestionCorrectAnswersCounter++;
                        }
                    }

                    int totalCorrectAnswersAmount = currentQuestionCorrectAnswersCounter + previousesQuestionCorrectAnswersAmount;
                    this.ViewBag.TotalCorrectAnswers = totalCorrectAnswersAmount;

                    int nextQuestionIndex = currentQuestionIndex + 1;
                    if (nextQuestionIndex > listQuestionModels.Count - 1)
                    {
                        this.ViewBag.Finish = true;
                        this.ModelState.Clear();
                        return this.View();
                    }

                    this.ViewBag.QuestionIndex = nextQuestionIndex;
                    this.ModelState.Clear();
                    return this.View();

                case ActionType.Cancel:
                    this.logger.LogInformation("TestPassing action: Switch Cancel");

                    return this.Redirect("/Tests");

                default:
                    return this.Redirect("~/");
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult TestPassFinished(int testId, int previousesQuestionCorrectAnswersAmount)
        {
            this.logger.LogInformation("TestPassFinished(post) action");

            string userName = this.User.Identity.Name;
            string userId = this.dataManager.Users.GetIdByEmail(userName);
            StatiscticViewModel statiscticViewModel = new StatiscticViewModel() { testId = testId, Result = previousesQuestionCorrectAnswersAmount };
            this.servicesManager.Statistics.SaveStatisticEditModelIntoDb(new StatisticEditModel() { userId = userId, testId = testId, Result = previousesQuestionCorrectAnswersAmount });

            var testTitle = this.servicesManager.Tests.TestFromDbToViewModelById(testId);
            var questionsAmount = this.servicesManager.Questions.GetAllViewQuestionsByTestId(testId).Count;

            double devide = (double)statiscticViewModel.Result / (double)questionsAmount;
            var percentage = devide * 100;

            this.ViewBag.TestTitle = testTitle.Title;
            this.ViewBag.QuestionsAmount = questionsAmount;
            this.ViewBag.PercentEquivalent = percentage;

            return this.View(statiscticViewModel);
        }

        // TODO: edit form (response test data into view)
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult CreateOrEditTest(int testId = 0, string isPrivate = "false")
        {
            this.logger.LogInformation("CreateOrEditTest action");

            TestEditModel model;
            if (testId != 0)
            {
                model = this.servicesManager.Tests.GetTestEditModel(testId);
            }
            else
            {
                model = this.servicesManager.Tests.GetTestEditModel();
            }

            this.ViewBag.testId = testId;
            this.ViewBag.isPrivate = bool.Parse(isPrivate);
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public RedirectToActionResult CreateOrEditTest(TestEditModel model)
        {
            this.logger.LogInformation("CreateOrEditTest(post) action");

            string owner = this.User.Identity.Name;
            int testId = this.servicesManager.Tests.SaveTestFromViewIntoDb(model, owner);
            return this.RedirectToAction("CreateOrEditQuestion", new { testId });
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult CreateOrEditQuestion(int testId, QuestionEditModel questionModel, QuestionsActionType actionType, int answerIdToDelete)
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

            this.logger.LogInformation("CreateOrEditQuestion action");

            this.ViewBag.testId = testId;
            bool detected = false;
            /*the value may be null when we have just created a test and gone to the questions page*/
            if (questionModel.Answers != null)
            {
                foreach (var item in questionModel.Answers)
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
                    logger.LogInformation("CreateOrEditQuestion action: Switch AddAnswer");

                    questionModel.Answers.Add(new AnswerEditModel() { });
                    break;
                case QuestionsActionType.NextQuestion:
                    this.logger.LogInformation("CreateOrEditQuestion action: Switch NextQuestion");
                    // if any answer is empty - detected and throw error on frontend

                    QuestionEditModel lastModelInList = this.servicesManager.Questions.GetLastQuestionFromTestQuestionsList(testId);
                    if (questionModel.Name != null && questionModel.questionId != 0 && /*not last in list*/ lastModelInList.questionId != questionModel.questionId)
                    {
                        if (detected)
                        {
                            this.ViewBag.Error = "Answer cannot be empty. Input or delete it.";
                        }
                        else
                        {
                            QuestionEditModel aheadQuestion = this.servicesManager.Questions.GetQuestionThatIsOneStepAheadFromTestQuestionsList(questionModel, testId);
                            this.servicesManager.Questions.SaveQuestionFromViewIntoDb(questionModel);
                            questionModel = aheadQuestion;
                        }
                    }
                    else if (questionModel.Name == null)
                    {
                        this.ViewBag.Error = "Question cannot be empty";
                    }
                    else
                    {
                        if (detected)
                        {
                            this.ViewBag.Error = "Answer cannot be empty. Input or delete it.";
                        }
                        else
                        {
                            this.servicesManager.Questions.SaveQuestionFromViewIntoDb(questionModel);
                            questionModel.Name = null;
                            questionModel.questionId = 0;
                            questionModel.Answers = null;
                        }
                    }

                    this.ModelState.Clear(); // For clear razor state
                    break;
                case QuestionsActionType.RemoveAnswer:
                    this.logger.LogInformation("CreateOrEditQuestion action: Switch RemoveAnswer");

                    AnswerEditModel answerToDelete = questionModel.Answers[answerIdToDelete];
                    if (questionModel.Answers.Count > 1) // if this input not last
                    {
                        if (questionModel.questionId != 0)
                        {
                            this.servicesManager.Questions.DeleteAnswer(answerToDelete);
                        }

                        questionModel.Answers.Remove(answerToDelete);
                        this.ModelState.Clear();
                    }
                    else
                    {
                        this.ViewBag.Error = "You cannot delete the last answer";
                    }

                    break;
                case QuestionsActionType.PreviousQuestion:
                    this.logger.LogInformation("CreateOrEditQuestion action: Switch PreviousQuestion");

                    if (questionModel.questionId == 0 && questionModel.Name != null && !detected)
                    {
                        /*
                         * if the current question have non-empty inputs
                        */
                        QuestionEditModel lastQuestion = this.servicesManager.Questions.GetLastQuestionFromTestQuestionsList(testId);
                        this.servicesManager.Questions.SaveQuestionFromViewIntoDb(questionModel);
                        questionModel = lastQuestion;
                    }
                    else if (questionModel.questionId != 0) // If question has been already exist (in db)
                    {
                        /*
                         * if the current question has been exist in db (for edit from or if we are already there in previouses questions in test creating form)
                         * logic : indexOf currentQeustuoion in questions by testId and take a step back
                        */

                        List<QuestionEditModel> currentTestQuestions = this.servicesManager.Questions.GetAllEditQuestionsByTestId(testId);
                        int currentQuestionId = this.servicesManager.Questions.GetIndexOfSpecifierQuestionById(testId, questionModel.questionId);
                        QuestionEditModel previousQuestion = currentTestQuestions[currentQuestionId - 1];
                        questionModel = previousQuestion;
                    }
                    else
                    {
                        /*
                         * if current question creating inputs are empty and we need to take step to back
                        */
                        QuestionEditModel lastQuestion = this.servicesManager.Questions.GetLastQuestionFromTestQuestionsList(testId);
                        questionModel = lastQuestion;
                    }
                    this.ModelState.Clear();
                    break;
                case QuestionsActionType.RemoveQuestion:
                    this.logger.LogInformation("CreateOrEditQuestion action: Switch RemoveQuestion");

                    this.servicesManager.Questions.RemoveEditQuestionFromDb(questionModel);
                    QuestionEditModel lastQuestionModel = this.servicesManager.Questions.GetLastQuestionFromTestQuestionsList(testId);
                    questionModel = lastQuestionModel != null ? lastQuestionModel : new QuestionEditModel() { testId = testId };
                    this.ModelState.Clear();
                    break;
                case QuestionsActionType.Finish:
                    if (questionModel.Name != null)
                    {
                        if (detected)
                        {
                            this.ViewBag.Error = "Answer cannot be empty. Input or delete it.";
                        }
                        else
                        {
                            this.servicesManager.Questions.SaveQuestionFromViewIntoDb(questionModel);
                            return this.RedirectToAction("FinishTestCreating", new { testId });
                        }
                    }

                    this.ViewBag.Error = "Fields must be filled";
                    break;
                default:
                    break;
            }
            int questionsAmount = this.servicesManager.Questions.GetAllEditQuestionsByTestId(testId).Count;
            this.ViewBag.QuestionsAmount = questionsAmount;
            int currentQuestionIndexInList = this.servicesManager.Questions.GetIndexOfSpecifierQuestionById(testId, questionModel.questionId);
            this.ViewBag.CurrentQuestionIndex = currentQuestionIndexInList;
            return this.View(questionModel);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult FinishTestCreating(int testId)
        {
            this.logger.LogInformation("FinishTestCreating action");

            this.ViewData["testId"] = testId;
            return this.View("FinishTestCreating");
        }
    }
}
