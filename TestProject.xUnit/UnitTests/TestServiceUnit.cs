using TestioProject.PL.Models;
using TestioProject.PL.Services;
using TestProject.xUnit.Mocks;
using Xunit;

namespace TestProject.xUnit.UnitTests
{
    public class TestServiceUnit
    {
        [Fact]
        public void GetAllTestsUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
            
            var testService = new TestService(managerMock);

            UserViewModel userModel = new UserViewModel() { Id = "1", FirstName = "FIRSTNAME1", LastName = "LASTNAME1", Email = "user1@gmail.com" };
            TestViewModel model = new TestViewModel() { testId = 1, Title = "TEST1", Description = "DESC1", Owner = userModel, CodeLock = "111" };
            
            UserViewModel userModel2 = new UserViewModel() { Id = "2", FirstName = "FIRSTNAME2", LastName = "LASTNAME2", Email = "user2@gmail.com" };
            TestViewModel model2 = new TestViewModel() { testId = 2, Title = "TEST2", Description = "DESC2", Owner = userModel2 };

            var actual = testService.GetTestsList();
            
            Assert.Equal(actual[0].testId, model.testId);
            Assert.Equal(actual[0].Title, model.Title);
            Assert.Equal(actual[0].Owner.FirstName, model.Owner.FirstName);
            Assert.Equal(actual[0].CodeLock, model.CodeLock);
            
            Assert.Equal(actual[1].testId, model2.testId);
            Assert.Equal(actual[1].CodeLock, model2.CodeLock);
            
        }
    }
}