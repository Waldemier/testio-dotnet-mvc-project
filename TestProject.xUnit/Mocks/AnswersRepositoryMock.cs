using System.Collections.Generic;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;

namespace TestProject.xUnit.Mocks
{
    public class AnswersRepositoryMock: IAnswersRepository
    {
        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            throw new System.NotImplementedException();
        }

        public void SaveAnswer(Answer answer)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAnswer(Answer answer)
        {
            throw new System.NotImplementedException();
        }
    }
}