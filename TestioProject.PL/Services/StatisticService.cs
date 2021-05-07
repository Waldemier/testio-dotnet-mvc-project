using System.Linq;
using Microsoft.AspNetCore.Identity;

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

        public List<StatiscticViewModel> GetAllByUserId(string userId)
        {
            IEnumerable<Statistic> statistics = this.dataManager.Statistic.GetAllByUserId(userId);

            return statistics.Select(s => new StatiscticViewModel()
                {
                    testId = s.TestId,
                    TestName = s.Test.Name,
                    ownerId = s.UserId,
                    OwnerName = $"{s.User.FirstName} {s.User.LastName}",
                    Result = s.Result,
                    PassedAt = s.PassedAt
                })
                .ToList();
        }
        public void SaveStatisticEditModelIntoDb(StatisticEditModel model)
        {
            Statistic result = new Statistic() { UserId = model.userId, TestId = model.testId, Result = model.Result };
            this.dataManager.Statistic.SaveStatistic(result);
        }

        public List<TeacherStatisticViewModel> GetAllTeacherStatModelsByTeacherId(string userId)
        {
            List<TeacherStatisticViewModel> models = new List<TeacherStatisticViewModel>();
            ApplicationUser owner = dataManager.Users.GetUserById(userId);
            UserViewModel ownerViewModel = new UserViewModel() { Id= owner.Id, Email = owner.Email, FirstName = owner.FirstName, LastName = owner.LastName, AvatarUri = owner.AvatarUri };
            IEnumerable<Test> tests = dataManager.Tests.GetTestsByUserId(userId);
            foreach (var test in tests)
            {
                TestViewModel testViewModel = new TestViewModel() { testId = test.Id, Title = test.Name, Description = test.Description, Owner = ownerViewModel, CreatedAt = test.CreatedAt, ReferrerToken = test.ReferrerToken };
                var allTestStat = dataManager.Statistic.GetAllByTestId(test.Id);
                var usersAmount = allTestStat.Count();
                var allMarks = allTestStat.Select(x => x.Result).ToList();
                var totalAmount = 0;
                foreach (var mark in allMarks)
                {
                    totalAmount += mark;
                }

                var GPA = totalAmount / usersAmount;
                models.Add(new TeacherStatisticViewModel(){ Test = testViewModel, UsersAmount = usersAmount, GPA = GPA });
            }

            return models;
        }
    }
}
