using System;
using System.Collections.Generic;
using System.Text;
using TestioProject.BLL;
using TestioProject.DAL.Models;
using TestioProject.PL.Models;

namespace TestioProject.PL.Services
{
    public class WrittenLetterService
    {
        private readonly DataManager dataManager;
        public WrittenLetterService(DataManager _dataManager)
        {
            dataManager = _dataManager;
        }

        public List<WrittenLetterModel> GetAll()
        {
            List<WrittenLetter> _list = dataManager.WrittenLetters.GetAllFromDb();
            List<WrittenLetterModel> _listModel = new List<WrittenLetterModel>();
            foreach(var item in _list)
            {
                _listModel.Add(new WrittenLetterModel() { User = item.User, Reason = item.Reason, Experience = item.Experience });
            }
            return _listModel;
        }
        public void SaveWrittenLetterModelIntoDb(WrittenLetterModel _model)
        {
            WrittenLetter letter = new WrittenLetter() { UserId = _model.User.Id, Reason = _model.Reason, Experience = _model.Experience };
            dataManager.WrittenLetters.SaveWrittenLetterIntoDb(letter);
        }
    }
}
