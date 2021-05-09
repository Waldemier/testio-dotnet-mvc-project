using TestioProject.BLL;

namespace TestProject.xUnit.Mocks
{
    using TestioProject.BLL.Interfaces;
    public class DataManagerMock: IDataManager
    {
        public DataManagerMock(IAnswersRepository answersRepository, IQuestionsRepository questionsRepository, 
            ITestsRepository testsRepository, IStatisticRepository statisticRepository, IUsersRepository usersRepository, IWrittenLettersRepository writtenLettersRepository)
        {
            Answers = answersRepository;
            Questions = questionsRepository;
            Tests = testsRepository;
            Statistic = statisticRepository;
            Users = usersRepository;
            WrittenLetters = writtenLettersRepository;
        }

        public IAnswersRepository Answers { get; }
        public IQuestionsRepository Questions { get; }
        public ITestsRepository Tests { get; }
        public IStatisticRepository Statistic { get; }
        public IUsersRepository Users { get; }
        public IWrittenLettersRepository WrittenLetters { get; }
    }
}