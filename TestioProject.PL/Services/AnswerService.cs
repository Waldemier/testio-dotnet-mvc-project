namespace TestioProject.PL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TestioProject.BLL;
    using TestioProject.DAL.Models;
    using static TestioProject.PL.Models.AnswerModel;

    public class AnswerService
    {
        private readonly DataManager dataManager;
        public AnswerService(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
    }
}
