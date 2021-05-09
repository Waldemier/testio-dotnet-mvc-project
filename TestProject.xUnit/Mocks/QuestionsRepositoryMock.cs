using System.Collections.Generic;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;

namespace TestProject.xUnit.Mocks
{
    public class QuestionsRepositoryMock: IQuestionsRepository
    {
        public IEnumerable<Question> GetAllQuestions(bool includeAnswers = true)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Question> GetQuestionsByTestId(int testId, bool includeAnswers = true)
        {
            throw new System.NotImplementedException();
        }

        public Question GetQuestionById(int questionId, bool includeAnswers = true)
        {
            throw new System.NotImplementedException();
        }

        public void SaveQuestion(Question question)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteQuestion(Question question)
        {
            throw new System.NotImplementedException();
        }
    }
}