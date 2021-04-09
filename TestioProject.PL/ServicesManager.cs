using System;
using TestioProject.BLL;
using TestioProject.PL.Services;

namespace TestioProject.PL
{
    //TODO: Сервіси слугують для того, щоб в самих контроллерах ми не зверталися напряму до бази даних, 
    //а використовуваои більш гнучкі та інтерактивні сутності, побудовані на основі бази. 
    //За допомогою них ми приймаємо дані на екшн, віддаємо у в'ю, та повертаємо з в'ю назад та через методи сервісів зберігаємо у базі.

    //* Посилання на тест (Передача параметрів через посилання) || asp-route-(назва параметру)
    public class ServicesManager
    {
        DataManager dataManager;
        private TestService testService;

        public ServicesManager(DataManager _dataManager)
        {
            dataManager = _dataManager;
            testService = new TestService(dataManager);
        }

        public TestService Tests { get { return testService; } }
    }
}
