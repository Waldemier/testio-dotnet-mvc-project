using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL.Interfaces;

namespace TestioProject.BLL
{
    public interface IDataManager
    {
        IAnswersRepository Answers { get; }
        IQuestionsRepository Questions { get; }
        ITestsRepository Tests { get; }
        IStatisticRepository Statistic { get; }
        IUsersRepository Users { get; }
        IWrittenLettersRepository WrittenLetters { get; }
    }
}
