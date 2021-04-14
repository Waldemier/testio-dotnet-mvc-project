using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.DAL.Models;

namespace TestioProject.BLL.Interfaces
{
    public interface IWrittenLettersRepository
    {
        void SaveWrittenLetterIntoDb(WrittenLetter _model);
        List<WrittenLetter> GetAllFromDb();
        void DeleteWriteLetterFromDb(string userId);
    }
}
