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
        
        [Fact]
        public void GetTestIdByReferrerTokenUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
            
            var testService = new TestService(managerMock);
            
            UserViewModel userModel = new UserViewModel() { Id = "1", FirstName = "FIRSTNAME1", LastName = "LASTNAME1", Email = "user1@gmail.com" };
            TestViewModel model = new TestViewModel() { testId = 1, Title = "TEST1", Description = "DESC1", Owner = userModel, CodeLock = "111" };

            var referrer = TestsRepositoryMock.referrer;
            
            var actual = testService.GetTestIdByReferrerToken(referrer);

            Assert.Equal(actual, 1);
        }

        [Fact]
        public void GetTestEditModelUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
            
            var testService = new TestService(managerMock);

            TestEditModel model = new TestEditModel() { testId = 1, Title = "TEST1", Description = "DESC1", CodeLock = "111" };
            TestEditModel model2 = new TestEditModel();

            const int testId = 1;
            const int zeroTestId = 0;
            
            var actual = testService.GetTestEditModel(testId);
            var actualWithZeroId = testService.GetTestEditModel(zeroTestId);

            Assert.Equal(actual.testId, model.testId);
            Assert.Equal(actual.Title, model.Title);
            
            Assert.Equal(actualWithZeroId.testId, model2.testId);
            Assert.Equal(actualWithZeroId.Title, model2.Title);
        }

        [Fact]
        public void TestFromDbToViewModelByIdUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
            
            var testService = new TestService(managerMock);

            UserViewModel userModel = new UserViewModel() { Id = "1", FirstName = "FIRSTNAME1", LastName = "LASTNAME1", Email = "user1@gmail.com" };
            TestViewModel model = new TestViewModel() { testId = 1, Title = "TEST1", Description = "DESC1", Owner = userModel, CodeLock = "111" };
            
            const int testId = 1;

            var actual = testService.TestFromDbToViewModelById(testId);
            
            Assert.Equal(actual.testId, model.testId);
            Assert.Equal(actual.Owner.Id, model.Owner.Id);
            Assert.Equal(actual.Title, model.Title);
        }
    }
}