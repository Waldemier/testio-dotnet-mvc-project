using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.DAL.Models;

namespace TestioProject.BLL.Interfaces
{
    public interface ITestsRepository
    {
        IEnumerable<Test> GetAllTests(bool includeQuestionsWithAnswers = true);
        Test GetTestById(int testId, bool includeQuestionsWithAnswers = true);
        IEnumerable<Test> GetTestsByUserId(string userId, bool includeQuestionsWithAnswers = true);
        void SaveTest(Test test);
        void DeleteTest(Test test);
    }
}
