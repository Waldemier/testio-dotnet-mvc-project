using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;
using TestioProject.DAL.Models;

namespace TestioProject.BLL.Implementations
{
    public class EFQuestionsRepository : IQuestionsRepository
    {
        private readonly TestioDbContext context;
        public EFQuestionsRepository(TestioDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<Question> GetQuestionsByTestId(int testId, bool includeAnswers = true)
        {
            if (includeAnswers)
                return context.Questions.Include(x => x.Answers).Where(x => x.TestId == testId).ToList();
            else
                return context.Questions.Where(x => x.TestId == testId).ToList();
        }

        public Question GetQuestionById(int questionId, bool includeAnswers = true)
        {
            if (includeAnswers)
                return context.Questions.Include(x => x.Answers).Where(x => x.Id == questionId).FirstOrDefault();
            else
                return context.Questions.Where(x => x.Id == questionId).FirstOrDefault();
        }

        public IEnumerable<Question> GetAllQuestions(bool includeAnswers = true)
        {
            if(includeAnswers)
                return context.Questions.Include(x => x.Answers).ToList();
            else
                return context.Questions.ToList();
        }

        public void DeleteQuestion(Question question)
        {
            context.Questions.Remove(question);
            context.SaveChanges();
        }

        public void SaveQuestion(Question question)
        {
            if (question.Id == 0)
                context.Questions.Add(question);
            else
                context.Entry(question).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
