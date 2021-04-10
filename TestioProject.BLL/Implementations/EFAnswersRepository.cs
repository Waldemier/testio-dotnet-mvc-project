using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;
using TestioProject.DAL.Models;

namespace TestioProject.BLL.Implementations
{
    public class EFAnswersRepository : IAnswersRepository
    {
        private readonly TestioDbContext context;
        public EFAnswersRepository(TestioDbContext _context)
        {
            context = _context;
        }

        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            return context.Answers.Where(x => x.QuestionId == questionId).ToList();
        }

        public void DeleteAnswer(Answer answer)
        {
            context.Answers.Remove(answer);
            context.SaveChanges();
        }

        public void SaveAnswer(Answer answer)
        {
            if (answer.Id == 0)
                context.Answers.Add(answer);
            else
                context.Entry(answer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
