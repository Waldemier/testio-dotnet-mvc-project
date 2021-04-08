using System;
using TestioProject.BLL.Implementations;
using TestioProject.BLL.Interfaces;

namespace TestioProject.BLL
{
    public class DataManager
    {
        private readonly IAnswersRepository answersRepository;
        private readonly IQuestionsRepository questionsRepository;
        private readonly ITestsRepository testsRepository;
        private readonly IStatisticRepository statisticRepository;
        private readonly IUsersRepository usersRepository;

        public DataManager(IAnswersRepository _answersRepository, IQuestionsRepository _questionsRepository, ITestsRepository _testsRepository, IStatisticRepository _statisticRepository, IUsersRepository _usersRepository)
        {
            answersRepository = _answersRepository;
            questionsRepository = _questionsRepository;
            testsRepository = _testsRepository;
            statisticRepository = _statisticRepository;
            usersRepository = _usersRepository;
        }

        public IAnswersRepository Answers { get { return answersRepository; } }
        public IQuestionsRepository Questions { get { return questionsRepository; } }
        public ITestsRepository Tests { get { return testsRepository; } }
        public IStatisticRepository Statistic { get { return statisticRepository; } }
        public IUsersRepository Users { get { return usersRepository; } }
    }
}
