using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;
using TestioProject.DAL.Models;

namespace TestioProject.BLL.Implementations
{
    public class EFWrittenLettersRepository: IWrittenLettersRepository
    {
        private readonly TestioDbContext context;
        public EFWrittenLettersRepository(TestioDbContext _context)
        {
            context = _context;
        }
        public void SaveWrittenLetterIntoDb(WrittenLetter _model)
        {
            _model.CreatedAt = DateTime.Now;
            context.WrittenLetters.Add(_model);
            context.SaveChanges();
        }

        public void DeleteWriteLetterFromDb(string userId)
        {
            WrittenLetter _writtenLetter = context.WrittenLetters.Where(x => x.UserId == userId).FirstOrDefault();
            context.WrittenLetters.Remove(_writtenLetter);
            context.SaveChanges();
        }

        public List<WrittenLetter> GetAllFromDb()
        {
            return context.WrittenLetters.Include(x => x.User).ToList();
        }
    }
}
