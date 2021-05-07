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
        private readonly WrittenLetterService writtenLetterService;
        private readonly StatisticService statisticService;
        private readonly UserService userService;
        public ServicesManager(DataManager dataManager)
        {
            this.dataManager = dataManager;
            this.testService = new TestService(this.dataManager);
            this.questionService = new QuestionService(this.dataManager);
            this.answerService = new AnswerService(this.dataManager);
            this.writtenLetterService = new WrittenLetterService(this.dataManager);
            this.statisticService = new StatisticService(this.dataManager);
            this.userService = new UserService(this.dataManager);
        }
        public TestService Tests { get { return this.testService; } }
        public QuestionService Questions { get { return this.questionService; } }
        public AnswerService Answers { get { return this.answerService; } }
        public WrittenLetterService WrittenLetter { get { return this.writtenLetterService; } }
        public StatisticService Statistics { get { return this.statisticService; } }
        public UserService Users { get { return this.userService; } }

    }
}
