using System;
using System.Collections.Generic;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;

namespace TestProject.xUnit.Mocks
{
    public class TestsRepositoryMock: ITestsRepository
    {
        public IEnumerable<Test> GetAllTests(bool includeQuestionsWithAnswers = true)
        {
            return new List<Test>()
            {
                new Test() { Id = 1, Name = "TEST1", Description = "DESC1", User = new ApplicationUser(){ Id = "1" , FirstName = "FIRSTNAME1", LastName = "LASTNAME1", Email = "user1@gmail.com" }, CodeLock = "111", UserId = "1" },
                new Test() { Id = 2, Name = "TEST2", Description = "DESC2", User = new ApplicationUser(){ Id="2", FirstName = "FIRSTNAME2", LastName = "LASTNAME2", Email = "user2@gmail.com" }, UserId = "2" }
            };
        }

        public Test GetTestById(int testId, bool includeQuestionsWithAnswers = true)
        {
            if (testId == 1)
            {
                return new Test()
                {
                    Id = 1, Name = "TEST1", Description = "DESC1",
                    User = new ApplicationUser()
                        {Id = "1", FirstName = "FIRSTNAME1", LastName = "LASTNAME2", Email = "user1@gmail.com"},
                    CodeLock = "111", UserId = "1"
                };
            }
            else if (testId == 2)
            {
                return new Test()
                {
                    Id = 2, Name = "TEST2", Description = "DESC2",
                    User = new ApplicationUser()
                        {Id = "2", FirstName = "FIRSTNAME2", LastName = "LASTNAME2", Email = "user2@gmail.com"},
                    UserId = "2"
                };
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Test> GetTestsByUserId(string userId, bool includeQuestionsWithAnswers = true)
        {
            throw new NotImplementedException();
        }

        public void SaveTest(Test test)
        {
            throw new NotImplementedException();
        }

        public void DeleteTest(Test test)
        {
            throw new NotImplementedException();
        }

        public int GetTestIdByReferrerToken(Guid referrerToken)
        {
            throw new NotImplementedException();
        }
    }
}