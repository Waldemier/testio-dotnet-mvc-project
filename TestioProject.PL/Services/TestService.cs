using System.Linq;
using System.Net.Mime;

namespace TestioProject.PL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TestioProject.BLL;
    using TestioProject.DAL.Models;
    using TestioProject.PL.Models;
    public class TestService
    {
        private readonly IDataManager dataManager;
        public TestService(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public List<TestViewModel> GetTestsList()
        {
            var tests = this.dataManager.Tests.GetAllTests(false);
            List<TestViewModel> testModelsList = new List<TestViewModel>();

            foreach (var item in tests)
            {
                testModelsList.Add(this.TestFromDbToViewModelById(item.Id));
            }

            return testModelsList;
        }

        public IEnumerable<TestViewModel> GetAllTestsViewModelFromDb()
        {
            var tests = this.dataManager.Tests.GetAllTests();
            IEnumerable<TestViewModel> viewModels = new List<TestViewModel>();
            foreach (var test in tests)
            {
                var user = this.dataManager.Users.GetUserById(test.UserId);
                UserViewModel viewUser = new UserViewModel() { Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, AvatarUri = user.AvatarUri, Baned = user.Baned };
                viewModels.Append(new TestViewModel() { testId = test.Id, Title = test.Name, Description =  test.Description, Owner = viewUser, CreatedAt = test.CreatedAt, ReferrerToken = test.ReferrerToken, CodeLock = test.CodeLock });
            }
            return viewModels;
        }
        
        public int GetTestIdByReferrerToken(Guid referrerToken)
        {
            int Id = dataManager.Tests.GetTestIdByReferrerToken(referrerToken);
            return Id;
        }

        public TestViewModel TestFromDbToViewModelById(int testId)
        {
            var test = this.dataManager.Tests.GetTestById(testId);
            UserViewModel userModel = new UserViewModel() { Id = test.UserId, FirstName = test.User.FirstName, LastName = test.User.LastName, AvatarUri = test.User.AvatarUri, Email = test.User.Email, Baned = test.User.Baned };
            TestViewModel model = new TestViewModel() { testId = testId, Title = test.Name, Description = test.Description, Owner = userModel, CreatedAt = test.CreatedAt, ReferrerToken = test.ReferrerToken, CodeLock = test.CodeLock };
            return model;
        }

        public int SaveTestFromViewIntoDb(TestEditModel model, string ownerEmail)
        {
            Test test;
            if (model.testId != 0)
            {
                test = this.dataManager.Tests.GetTestById(model.testId);
                test.Name = model.Title;
                test.Description = model.Description;
                test.CodeLock = model.CodeLock;
            }
            else
            {
                string userId = this.dataManager.Users.GetIdByEmail(ownerEmail);
                test = new Test()
                {
                    Name = model.Title,
                    Description = model.Description,
                    CodeLock = model.CodeLock,
                    UserId = userId,
                };
            }

            this.dataManager.Tests.SaveTest(test);

            return test.Id;
        }

        public TestEditModel GetTestEditModel(int testId = 0)
        {
            if(testId != 0)
            {
                var test = this.dataManager.Tests.GetTestById(testId);
                var testEditModel = new TestEditModel()
                {
                    testId = test.Id,
                    Title = test.Name,
                    Description = test.Description,
                    CodeLock = test.CodeLock,
                };
                return testEditModel;
            }
            else
            {
                return new TestEditModel();
            }
        }
    }
}
