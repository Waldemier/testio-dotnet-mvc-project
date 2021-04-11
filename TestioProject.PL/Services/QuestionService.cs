using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using TestioProject.PL.Models;
using static TestioProject.PL.Models.AnswerModel;

namespace TestioProject.PL.Services
{
    public class QuestionService
    {
        private readonly DataManager dataManager;
        public QuestionService(DataManager _dataManager)
        {
            dataManager = _dataManager;
        }

        public int GetIndexOfSpecifierQuestionById(int testId, int questionId)
        {
            Test _currentTest = dataManager.Tests.GetTestById(testId);
            int _currentQuestionIndex = -1;
            foreach(var item in _currentTest.Questions)
            {
                if(item.Id == questionId)
                {
                    _currentQuestionIndex = _currentTest.Questions.IndexOf(item);
                }
            }
            return _currentQuestionIndex;
        }

        public QuestionEditModel GetLastQuestionFromTestQuestionsList(int testId)
        {
            try
            {
                Question question = dataManager.Tests.GetTestById(testId).Questions[^1];
                List<AnswerEditModel> answerEditModels = new List<AnswerEditModel>();
                foreach (var item in question.Answers)
                {
                    answerEditModels.Add(new AnswerEditModel() { Name = item.Text, isTruth = item.isTruth, questionId = item.QuestionId });
                }
                QuestionEditModel questionEditModel = new QuestionEditModel() { Name = question.Name, testId = question.TestId, questionId = question.Id, Answers = answerEditModels };
                return questionEditModel;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public List<QuestionEditModel> GetAllEditQuestionsByTestId(int testId)
        {
            List<Question> _questions = dataManager.Tests.GetTestById(testId).Questions;
            List<QuestionEditModel> _questionEditModels = new List<QuestionEditModel>();
            foreach (var item in _questions)
            {
                List<AnswerEditModel> _answerEditModels = new List<AnswerEditModel>();
                foreach (var ans in item.Answers)
                {
                    _answerEditModels.Add(new AnswerEditModel() { Name = ans.Text, isTruth = ans.isTruth, questionId = ans.QuestionId });
                }
                _questionEditModels.Add(new QuestionEditModel() { Name = item.Name, Answers = _answerEditModels, testId = item.TestId, questionId = item.Id });
            }
            return _questionEditModels;
        }

        public QuestionEditModel GetQuestionThatIsOneStepAheadFromTestQuestionsList(QuestionEditModel _model, int testId)
        {
            List<Question> questions = dataManager.Tests.GetTestById(testId).Questions;
            bool detected = false;
            foreach (var item in questions)
            {
                if(item.Id == _model.questionId)
                {
                    detected = true;
                    continue;
                }
                if(detected == true)
                {
                    List<AnswerEditModel> answerEditModels = new List<AnswerEditModel>();
                    foreach (var ans in item.Answers)
                    {
                        answerEditModels.Add(new AnswerEditModel() { Name = ans.Text, isTruth = ans.isTruth, questionId = ans.QuestionId });
                    }
                    QuestionEditModel questionEditModel = new QuestionEditModel() { Name = item.Name, testId = item.TestId, questionId = item.Id, Answers = answerEditModels };
                    return questionEditModel;
                }
            }
            return GetLastQuestionFromTestQuestionsList(testId);
        }

        public void DeleteAnswer(AnswerEditModel _model)
        {
            List<Answer> _answers = dataManager.Questions.GetQuestionById(_model.questionId).Answers;
            Answer _answer = new Answer();
            foreach (var item in _answers)
            {
                if (item.Text == _model.Name)
                {
                    _answer = item;
                }
            }
            dataManager.Answers.DeleteAnswer(_answer);
        }

        public void RemoveEditQuestionFromDb(QuestionEditModel _model)
        {
            Question question = dataManager.Questions.GetQuestionById(_model.questionId);
            dataManager.Questions.DeleteQuestion(question);
        }

        public void SaveQuestionFromViewIntoDb(QuestionEditModel _model)
        {
            Question _question;
            Answer _answer;
            List<Answer> _answers;
            if (_model.questionId != 0)
            {
                _question = dataManager.Questions.GetQuestionById(_model.questionId);
                _question.Name = _model.Name;

                _answers = dataManager.Answers.GetAnswersByQuestionId(_model.questionId);
                for(int i = 0; i < _answers.Count; i ++)
                {
                    _answers[i].Text = _model.Answers[i].Name;
                    _answers[i].isTruth = _model.Answers[i].isTruth;
                    dataManager.Answers.SaveAnswer(_answers[i]);
                }
                dataManager.Questions.SaveQuestion(_question);
            }
            else
            {
                _question = new Question()
                {
                    Name = _model.Name,
                    TestId = _model.testId,
                };
                dataManager.Questions.SaveQuestion(_question);

                for (int i = 0; i < _model.Answers.Count; i++)
                {
                    _answer = new Answer();
                    _answer.Text = _model.Answers[i].Name;
                    _answer.QuestionId = _question.Id;
                    _answer.isTruth = _model.Answers[i].isTruth;

                    dataManager.Answers.SaveAnswer(_answer);
                }
            }
        }
    }
}
