using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;
using TestioProject.DAL.Models;

namespace TestioProject.BLL.Implementations
{
    public class EFStatisticRepository : IStatisticRepository
    {
        private readonly TestioDbContext context;
        public EFStatisticRepository(TestioDbContext _context)
        {
            context = _context;
        }
        

        public IEnumerable<Statistic> GetAllByTestId(int testId)
        {
            return context.Statistics.Where(x => x.TestId == testId).ToList();
        }

        public IEnumerable<Statistic> GetAllByUserId(string userId)
        {
            return context.Statistics.Where(x => x.UserId == userId).ToList();
        }

        public void SaveStatistic(Statistic result)
        {
            if (result.Id == 0)
                context.Statistics.Add(result);
            else
                context.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        public void DeleteStatistic(Statistic element)
        {
            context.Statistics.Remove(element);
            context.SaveChanges();
        }
    }
}
