using Microsoft.EntityFrameworkCore;

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
            return this.context.Statistics.Include(x => x.Test).Include(x => x.User).Where(x => x.TestId == testId).ToList();
        }

        public IEnumerable<Statistic> GetAllByUserId(string userId)
        {
            return this.context.Statistics.Include(x => x.Test).Include(x => x.User).Where(x => x.UserId == userId).ToList();
        }

        public void SaveStatistic(Statistic result)
        {
            var dbStat = this.context.Statistics.FirstOrDefault(x => x.UserId == result.UserId && x.TestId == result.TestId);
            
            if (dbStat == null)
            {
                result.PassedAt = DateTime.Now;
                this.context.Statistics.Add(result);
            }
            else
            {
                dbStat.Result = result.Result;
                //this.context.Statistics.Update(dbStat); // The same as under line code
                this.context.Entry(dbStat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            this.context.SaveChanges();
        }

        public void DeleteStatistic(Statistic element)
        {
            this.context.Statistics.Remove(element);
            this.context.SaveChanges();
        }
    }
}
