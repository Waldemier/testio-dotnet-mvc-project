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
        public EFTestsRepository(TestioDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Test> GetAllTests(bool includeQuestionsWithAnswers = true)
        {
            var tests = context.Tests.Include(x => x.Questions).ToList();

            // Because we need valid tests (with Questions)
            foreach (var test in tests)
            {
                if (test.Questions.Count == 0)
                {
                    this.context.Tests.Remove(test);
                }
            }

            this.context.SaveChanges();
            if (includeQuestionsWithAnswers)
            {
                return this.context.Tests.Include(x => x.Questions).ToList();
            }
            return this.context.Tests.ToList();
        }

        public int GetTestIdByReferrerToken(Guid referrerToken)
        {
            return context.Tests.FirstOrDefault(x => x.ReferrerToken == referrerToken).Id;
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
            {
                test.CreatedAt = DateTime.Now;
                test.ReferrerToken = Guid.NewGuid();
                context.Tests.Add(test);
            }
            else
            {
                context.Entry(test).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            context.SaveChanges();
        }
        public void DeleteTest(Test test)
        {
            context.Tests.Remove(test);
            context.SaveChanges();
        }
    }
}
