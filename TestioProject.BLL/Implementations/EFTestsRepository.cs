namespace TestioProject.BLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using TestioProject.BLL.Interfaces;
    using TestioProject.DAL.Data;
    using TestioProject.DAL.Models; 

    public class EFTestsRepository : ITestsRepository
    {
        private readonly TestioDbContext context;
        public EFTestsRepository(TestioDbContext _context)
        {
            context = _context;
        }


        public IEnumerable<Test> GetAllTests(bool includeQuestionsWithAnswers = true)
        {
            if (includeQuestionsWithAnswers)
                return context.Tests.Include(x => x.Questions).ToList();
            else
                return context.Tests.ToList();
        }

        public Test GetTestById(int testId, bool includeQuestionsWithAnswers = true)
        {
            if (includeQuestionsWithAnswers)
                return context.Tests.Include(x => x.Questions).ThenInclude(x => x.Answers).Include(x => x.User).Where(x => x.Id == testId).FirstOrDefault();
            else 
                return context.Tests.Include(x => x.User).Where(x => x.Id == testId).FirstOrDefault();
        }

        //public void GetQuestionIndexFromList(int testId, int questionId, bool includeQuestionsWithAnswers = true)
        //{
        //    if (includeQuestionsWithAnswers)
        //        return context.Tests.Include(x => x.Questions).ThenInclude(x => x.Answers).Where(x => x. == testId).FirstOrDefault();
        //    else
        //        return context.Tests.Include(x => x.User).Where(x => x.Id == testId).FirstOrDefault();
        //}

        public IEnumerable<Test> GetTestsByUserId(string userId, bool includeQuestionsWithAnswers = true)
        {
            if (includeQuestionsWithAnswers)
                return context.Tests.Include(x => x.Questions).Where(x => x.UserId == userId).ToList();
            else
                return context.Tests.Where(x => x.UserId == userId).ToList();
        }

        public void SaveTest(Test test)
        {
            if (test.Id == 0)
                context.Tests.Add(test);
            else
                context.Entry(test).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        public void DeleteTest(Test test)
        {
            context.Tests.Remove(test);
            context.SaveChanges();
        }
    }
}
