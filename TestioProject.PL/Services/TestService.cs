using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using TestioProject.PL.Models;

namespace TestioProject.PL.Services
{
    public class TestService
    {
        private readonly DataManager dataManager;
        public TestService(DataManager _dataManager)
        {
            dataManager = _dataManager;
        }

        public List<TestViewModel> GetTestsList()
        {
            var _tests = dataManager.Tests.GetAllTests(false);
            List<TestViewModel> _testModelsList = new List<TestViewModel>();

            foreach(var item in _tests)
            {
                _testModelsList.Add(TestFromDbToViewModelById(item.Id));
            }
            return _testModelsList;
        }

        public TestViewModel TestFromDbToViewModelById(int testId)
        {
            var _test = dataManager.Tests.GetTestById(testId);
            UserViewModel _userModel = new UserViewModel() { FirstName = _test.User.FirstName, LastName = _test.User.LastName };

            TestViewModel _model = new TestViewModel() { testId = testId, Title = _test.Name, Description = _test.Description, Owner = _userModel };
            return _model;
        }

        public int SaveTestFromViewIntoDb(TestEditModel _model, string OwnerEmail)
        {
            Test _test;
            if(_model.testId != 0)
            {
                _test = dataManager.Tests.GetTestById(_model.testId);
                _test.Name = _model.Title;
                _test.Description = _model.Description;
                _test.CodeLock = _model.CodeLock;
            }
            else
            {
                string userId = dataManager.Users.GetIdByEmail(OwnerEmail);
                _test = new Test()
                {
                    Name = _model.Title,
                    Description = _model.Description,
                    CodeLock = _model.CodeLock,
                    UserId = userId
                };
            }
            dataManager.Tests.SaveTest(_test);

            return _test.Id;
        }

        public TestEditModel GetTestEditModel(int testId = 0)
        {
            if(testId != 0)
            {
                var _test = dataManager.Tests.GetTestById(testId);
                var _testEditModel = new TestEditModel()
                {
                    testId = _test.Id,
                    Title = _test.Name,
                    Description = _test.Description,
                    CodeLock = _test.CodeLock
                };
                return _testEditModel;
            }
            else
            {
                return new TestEditModel() { };
            }
        }
    }
}
