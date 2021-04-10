using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using TestioProject.PL.Models;

namespace TestioProject.PL.Services
{
    public class QuestionService
    {
        private readonly DataManager dataManager;
        public QuestionService(DataManager _dataManager)
        {
            dataManager = _dataManager;
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
