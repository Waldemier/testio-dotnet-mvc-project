namespace TestioProject.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TestioProject.DAL.Models;

    public interface IStatisticRepository
    {
        IEnumerable<Statistic> GetAllByTestId(int testId);

        IEnumerable<Statistic> GetAllByUserId(string userId);

        void SaveStatistic(Statistic result);

        void DeleteStatistic(Statistic element);
    }
}
