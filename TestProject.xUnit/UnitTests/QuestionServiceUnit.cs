using System.Collections.Generic;
using TestioProject.PL.Models;
using TestioProject.PL.Services;
using TestProject.xUnit.Mocks;
using Xunit;

namespace TestProject.xUnit.UnitTests
{
    public class QuestionServiceUnit
    {
        [Fact]
        public void GetAllViewQuestionsByTestIdUnit()
        {
            var managerMock = new DataManagerMock(new AnswersRepositoryMock(), new QuestionsRepositoryMock(), 
                new TestsRepositoryMock(), new StatisticRepositoryMock(), new UsersRepositoryMock(), 
                new WrittenLetterRepositoryMock());
            
            var questionService = new QuestionService(managerMock);
            
            List<AnswerModel.AnswerViewModel> answers = new List<AnswerModel.AnswerViewModel>()
            {
                new AnswerModel.AnswerViewModel() { Name="answer1", isTruth = true },
                new AnswerModel.AnswerViewModel() { Name="answer2", isTruth = false }
            };
            
            QuestionViewModel model = new QuestionViewModel() { questionId = 1, Name = "question1", Answers = answers, testId = 1 };

            const int testId = 1;
            
            var actual = questionService.GetAllViewQuestionsByTestId(testId);
            
            Assert.Equal(actual.Count, 2);
            Assert.Equal(actual[0].testId, model.testId);
            Assert.Equal(actual[0].questionId, model.questionId);
            Assert.Equal(actual[0].Answers.Count, model.Answers.Count);
            Assert.Equal(actual[0].Answers[0].isTruth, model.Answers[0].isTruth);
        }
    }
}