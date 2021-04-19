using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using TestioProject.PL.Models;

namespace TestioProject.PL.Services
{
    public class StatisticService
    {
        private readonly DataManager dataManager;
        public StatisticService(DataManager _dataManager)
        {
            dataManager = _dataManager;
        }
        public void SaveStatisticEditModelIntoDb(StatisticEditModel _model)
        {
            Statistic result = new Statistic() { UserId = _model.userId, TestId = _model.testId, Result = _model.Result };
            dataManager.Statistic.SaveStatistic(result);
        }  
    }
}
