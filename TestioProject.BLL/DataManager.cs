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
        private readonly IRestrictedRepository restrictedRepository;
        private readonly IUserAvatarsRepository userAvatarRepository;
        private readonly IWrittenLettersRepository writtenLettersRepository;

        public DataManager(IAnswersRepository _answersRepository, IQuestionsRepository _questionsRepository, ITestsRepository _testsRepository, IStatisticRepository _statisticRepository, IUsersRepository _usersRepository, IWrittenLettersRepository _writtenLettersRepository, IUserAvatarsRepository _userAvatarRepository, IRestrictedRepository _restrictedRepository)
        {
            answersRepository = _answersRepository;
            questionsRepository = _questionsRepository;
            testsRepository = _testsRepository;
            statisticRepository = _statisticRepository;
            writtenLettersRepository = _writtenLettersRepository;
            usersRepository = _usersRepository;
            userAvatarRepository = _userAvatarRepository;
            restrictedRepository = _restrictedRepository;
        }

        public IAnswersRepository Answers { get { return answersRepository; } }
        public IQuestionsRepository Questions { get { return questionsRepository; } }
        public ITestsRepository Tests { get { return testsRepository; } }
        public IStatisticRepository Statistic { get { return statisticRepository; } }
        public IUsersRepository Users { get { return usersRepository; } }
        public IWrittenLettersRepository WrittenLetters { get { return writtenLettersRepository; } }
        public IRestrictedRepository Restricted { get { return restrictedRepository; } }
        public IUserAvatarsRepository UserAvatars { get { return userAvatarRepository; } }
    }
}
