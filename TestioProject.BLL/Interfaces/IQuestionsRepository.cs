using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.DAL.Models;

namespace TestioProject.BLL.Interfaces
{
    public interface IQuestionsRepository
    {
        IEnumerable<Question> GetAllQuestions(bool includeAnswers = true);
        IEnumerable<Question> GetQuestionsByTestId(int testId, bool includeAnswers = true);
        void SaveQuestion(Question question);
        void DeleteQuestion(Question question);
    }
}
