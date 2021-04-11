using System;
using TestioProject.BLL;
using TestioProject.PL.Services;

namespace TestioProject.PL
{
    //TODO: Сервіси слугують для того, щоб в самих контроллерах ми не зверталися напряму до бази даних, 
    //а використовуваои більш гнучкі та інтерактивні сутності, побудовані на основі бази. 
    //За допомогою них ми приймаємо дані на екшн, віддаємо у в'ю, та повертаємо з в'ю назад, і через методи сервісів зберігаємо у базі.

    //* Посилання на тест (Передача параметрів через посилання) || asp-route-(назва параметру)
    public class ServicesManager
    {
        private readonly DataManager dataManager;
        private readonly TestService testService;
        private readonly QuestionService questionService;
        private readonly AnswerService answerService;
        public ServicesManager(DataManager _dataManager)
        {
            dataManager = _dataManager;
            testService = new TestService(dataManager);
            questionService = new QuestionService(dataManager);
            answerService = new AnswerService(dataManager);
        }
        public TestService Tests { get { return testService; } }
        public QuestionService Questions { get { return questionService; } }
        public AnswerService Answers { get { return answerService; } }
    }
}
