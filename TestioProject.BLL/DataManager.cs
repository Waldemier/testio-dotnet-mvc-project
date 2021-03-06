namespace TestioProject.BLL
{
    using System;
    using TestioProject.BLL.Implementations;
    using TestioProject.BLL.Interfaces;

    public class DataManager: IDataManager
    {
        private readonly IAnswersRepository answersRepository;
        private readonly IQuestionsRepository questionsRepository;
        private readonly ITestsRepository testsRepository;
        private readonly IStatisticRepository statisticRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IWrittenLettersRepository writtenLettersRepository;

        public DataManager(IAnswersRepository answersRepository, IQuestionsRepository questionsRepository, ITestsRepository testsRepository, IStatisticRepository statisticRepository, IUsersRepository usersRepository, IWrittenLettersRepository writtenLettersRepository)
        {
            this.answersRepository = answersRepository;
            this.questionsRepository = questionsRepository;
            this.testsRepository = testsRepository;
            this.statisticRepository = statisticRepository;
            this.writtenLettersRepository = writtenLettersRepository;
            this.usersRepository = usersRepository;
        }

        public IAnswersRepository Answers { get { return this.answersRepository; } }
        public IQuestionsRepository Questions { get { return this.questionsRepository; } }
        public ITestsRepository Tests { get { return this.testsRepository; } }
        public IStatisticRepository Statistic { get { return this.statisticRepository; } }
        public IUsersRepository Users { get { return this.usersRepository; } }
        public IWrittenLettersRepository WrittenLetters { get { return this.writtenLettersRepository; } }
    }
}
