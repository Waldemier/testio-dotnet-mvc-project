using System;
using System.Collections.Generic;
using TestioProject.DAL.Models;
using TestioProject.PL.Models;
using TestioProject.PL.Services;
using TestProject.xUnit.Mocks;
using Xunit;

namespace TestProject.xUnit.UnitTests
{
    public class WrittenLetterServiceUnit
    {
        [Fact]
        public void GetAllUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
            
            var writtenLetterService = new WrittenLetterService(managerMock);

            List<WrittenLetterModel> models = new List<WrittenLetterModel>()
            {
                new WrittenLetterModel() { User = new ApplicationUser() { Id = "1" }, WrittenAt = DateTime.Parse("01/01/2021"), Experience = "experience1", Reason = "reason1" },
                new WrittenLetterModel() { User = new ApplicationUser() { Id = "2" }, WrittenAt = DateTime.Parse("01/01/2021"), Experience = "experience2", Reason = "reason2" }
            };

            var actual = writtenLetterService.GetAll();
            
            Assert.Equal(actual[0].User.Id, models[0].User.Id);
            Assert.Equal(actual[0].Reason, models[0].Reason);
            
            Assert.Equal(actual[1].User.Id, models[1].User.Id);
            Assert.Equal(actual[1].Reason, models[1].Reason);
        }

        [Fact]
        public void SaveWrittenLetterModelIntoDbUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
            
            var writtenLetterService = new WrittenLetterService(managerMock);

            WrittenLetterModel writtenLetterModel = new WrittenLetterModel() { User = new ApplicationUser() { Id = "1" }, Reason  = "reason1", Experience = "experience1" };

            writtenLetterService.SaveWrittenLetterModelIntoDb(writtenLetterModel);
            
            Assert.Equal(true, WrittenLetterRepositoryMock.SavedToDb);
        }
    }
}