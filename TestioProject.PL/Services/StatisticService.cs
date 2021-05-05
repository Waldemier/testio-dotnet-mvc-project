namespace TestioProject.PL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TestioProject.BLL;
    using TestioProject.DAL.Models;
    using TestioProject.PL.Models;

    public class StatisticService
    {
        private readonly DataManager dataManager;

        public StatisticService(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public void SaveStatisticEditModelIntoDb(StatisticEditModel model)
        {
            Statistic result = new Statistic() { UserId = model.userId, TestId = model.testId, Result = model.Result };
            this.dataManager.Statistic.SaveStatistic(result);
        }
    }
}
