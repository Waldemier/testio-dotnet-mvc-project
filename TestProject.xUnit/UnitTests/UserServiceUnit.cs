using TestioProject.PL.Models;
using TestioProject.PL.Services;
using TestProject.xUnit.Mocks;
using Xunit;

namespace TestProject.xUnit.UnitTests
{
    public class UserServiceUnit
    {
        [Fact]
        public void GetUserModelFromDbToViewByIdUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                            new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                            new WrittenLetterRepositoryMock());
                        
            var userService = new UserService(managerMock);
            UserViewModel model = new UserViewModel() { Id = "1", FirstName = "firstname1", LastName = "lastname1", AvatarUri = "/", Email = "user1@gmail.com", Baned = false };

            const string userId = "1";
            var actual = userService.GetUserModelFromDbToViewById(userId);
            
            Assert.Equal(actual.Id, model.Id);
            Assert.Equal(actual.Email, model.Email);
            Assert.Equal(actual.Baned, model.Baned);
        }

        [Fact]
        public void BanByUserIdUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
                        
            var userService = new UserService(managerMock);

            const string userId = "1";
            userService.BanByUserId(userId);
            
            Assert.Equal(UsersRepositoryMock.IsUserBanned, true);
        }

        [Fact]
        public void UnbanByUserIdUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
                        
            var userService = new UserService(managerMock);

            const string userId = "1";
            userService.UnbanByUserId(userId);
            
            Assert.Equal(UsersRepositoryMock.IsUserUnbanned, true);
        }
    }
}