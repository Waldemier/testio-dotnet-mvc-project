using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.DAL.Models;

namespace TestioProject.BLL.Interfaces
{
    public interface IAnswersRepository
    {
        void SaveAnswer(Answer answer);
        void DeleteAnswer(Answer answer);
    }
}
