namespace TestioProject.BLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TestioProject.BLL.Interfaces;
    using TestioProject.DAL.Data;
    using TestioProject.DAL.Models;

    public class EFStatisticRepository : IStatisticRepository
    {
        private readonly TestioDbContext context;
        public EFStatisticRepository(TestioDbContext context)
        {
            this.context = context;
        }
        

        public IEnumerable<Statistic> GetAllByTestId(int testId)
        {
            return this.context.Statistics.Where(x => x.TestId == testId).ToList();
        }

        public IEnumerable<Statistic> GetAllByUserId(string userId)
        {
            return this.context.Statistics.Where(x => x.UserId == userId).ToList();
        }

        public void SaveStatistic(Statistic result)
        {
            if (result.Id == 0)
                this.context.Statistics.Add(result);
            else
                this.context.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            this.context.SaveChanges();
        }

        public void DeleteStatistic(Statistic element)
        {
            this.context.Statistics.Remove(element);
            this.context.SaveChanges();
        }
    }
}
