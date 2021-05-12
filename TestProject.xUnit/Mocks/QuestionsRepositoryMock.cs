using System.Collections.Generic;
using System.Linq;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;

namespace TestProject.xUnit.Mocks
{
    public class QuestionsRepositoryMock: IQuestionsRepository
    {
        public IEnumerable<Question> GetAllQuestions(bool includeAnswers = true)
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer() { Id = 1, QuestionId = 1, Text = "answer1", isTruth = true },
                new Answer() { Id = 2, QuestionId = 1, Text = "answer2", isTruth = false, }
            };
            List<Answer> answers2 = new List<Answer>()
            {
                new Answer() { Id = 3, QuestionId = 2, Text = "answer3", isTruth = true },
                new Answer() { Id = 4, QuestionId = 2, Text = "answer4", isTruth = false, }
            };
            return new List<Question>()
            {
                new Question() { Id = 1, Name = "question1", Answers = answers, TestId  = 1 },
                new Question() { Id = 2, Name = "question2", Answers = answers2, TestId = 1 }
            };
        }

        public IEnumerable<Question> GetQuestionsByTestId(int testId, bool includeAnswers = true)
        {
            return this.GetAllQuestions().Where(x => x.TestId == testId);
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