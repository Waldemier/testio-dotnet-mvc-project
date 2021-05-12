using System;
using System.Collections.Generic;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Models;

namespace TestProject.xUnit.Mocks
{
    public class WrittenLetterRepositoryMock: IWrittenLettersRepository
    {
        public static bool SavedToDb { get; set; }
        public static bool DeletedFromDb { get; set; }
        public void SaveWrittenLetterIntoDb(WrittenLetter _model)
        {
            if (_model != null && _model.UserId == "1" && _model.Reason == "reason1") SavedToDb = true;
        }

        public List<WrittenLetter> GetAllFromDb()
        {
            return new List<WrittenLetter>()
            {
                new WrittenLetter() { Id = 1, UserId = "1", CreatedAt = DateTime.Parse("01/01/2021"), Experience = "experience1", Reason = "reason1", User = new ApplicationUser() { Id = "1" } },
                new WrittenLetter() { Id = 2, UserId = "2", CreatedAt = DateTime.Parse("01/01/2021"), Experience = "experience2", Reason = "reason2", User = new ApplicationUser() { Id = "2" } }
            };
        }

        public void DeleteWriteLetterFromDb(string userId)
        {
            if (userId == "1") DeletedFromDb = true;
        }
    }
}