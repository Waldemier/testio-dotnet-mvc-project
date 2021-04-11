using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using static TestioProject.PL.Models.AnswerModel;

namespace TestioProject.PL.Services
{
    public class AnswerService
    {
        private readonly DataManager dataManager;
        public AnswerService(DataManager _dataManager)
        {
            dataManager = _dataManager;
        }
    }
}
