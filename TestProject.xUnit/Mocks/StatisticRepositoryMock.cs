using System.Collections.Generic;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;

namespace TestProject.xUnit.Mocks
{
    public class StatisticRepositoryMock: IStatisticRepository
    {
        public IEnumerable<Statistic> GetAllByTestId(int testId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Statistic> GetAllByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void SaveStatistic(Statistic result)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteStatistic(Statistic element)
        {
            throw new System.NotImplementedException();
        }
    }
}