using System;
using TestioProject.PL.Models;
using TestioProject.PL.Services;
using TestProject.xUnit.Mocks;
using Xunit;

namespace TestProject.xUnit.UnitTests
{
    public class StatisticServiceUnit
    {
        [Fact]
        public void GetAllByUserIdUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
            
            var statisticService = new StatisticService(managerMock);

            StatiscticViewModel model = new StatiscticViewModel() { testId = 1, ownerId = "1", Result = 1, PassedAt = DateTime.Parse("01/01/2021") };
            StatiscticViewModel model2 = new StatiscticViewModel() { testId = 2, ownerId = "1", Result = 2, PassedAt = DateTime.Parse("01/01/2021") };

            const string userId = "1";
            
            var actual = statisticService.GetAllByUserId(userId);
            
            Assert.Equal(actual[0].testId, model.testId);
            Assert.Equal(actual[0].ownerId, model.ownerId);
            Assert.Equal(actual[0].Result, model.Result);
            
            Assert.Equal(actual[1].testId, model2.testId);
            Assert.Equal(actual[1].ownerId, model2.ownerId);
            Assert.Equal(actual[1].Result, model2.Result);
        }

        [Fact]
        public void SaveStatisticEditModelIntoDbUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
            
            var statisticService = new StatisticService(managerMock);

            StatisticEditModel emodel = new StatisticEditModel() { testId = 1, userId = "1", Result = 1 };

            statisticService.SaveStatisticEditModelIntoDb(emodel);
            
            Assert.Equal(StatisticRepositoryMock.SavedToDb, true);
        }
    }
}