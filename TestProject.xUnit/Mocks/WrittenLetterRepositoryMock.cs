using System.Collections.Generic;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;

namespace TestProject.xUnit.Mocks
{
    public class WrittenLetterRepositoryMock: IWrittenLettersRepository
    {
        public void SaveWrittenLetterIntoDb(WrittenLetter _model)
        {
            throw new System.NotImplementedException();
        }

        public List<WrittenLetter> GetAllFromDb()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteWriteLetterFromDb(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}