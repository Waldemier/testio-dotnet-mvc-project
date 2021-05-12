using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;

namespace TestProject.xUnit.Mocks
{
    public class StatisticRepositoryMock: IStatisticRepository
    {
        public static bool SavedToDb { get; set; }
        public IEnumerable<Statistic> GetAllByTestId(int testId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Statistic> GetAllByUserId(string userId)
        {
            if (userId == "1")
            {
                return new List<Statistic>()
                {
                    new Statistic() { Id = 1, UserId = "1", TestId = 1, Result = 1, PassedAt = DateTime.Parse("01/01/2021"), Test = new Test() { Name = "test1" }, User = new ApplicationUser() { FirstName = "firstname2", LastName = "lastname2" } },
                    new Statistic() { Id = 2, UserId = "1", TestId = 2, Result = 2, PassedAt = DateTime.Parse("01/01/2021"), Test = new Test() { Name = "test2" }, User = new ApplicationUser() { FirstName = "firstname2", LastName = "lastname2" } }
                };
            }
            return null;
        }

        public void SaveStatistic(Statistic result)
        {
            if (result != null && result.UserId == "1" && result.TestId == 1 && result.Result == 1) SavedToDb = true;
        }

        public void DeleteStatistic(Statistic element)
        {
            throw new System.NotImplementedException();
        }
    }
}