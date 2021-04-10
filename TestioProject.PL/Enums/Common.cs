using System;
using System.Collections.Generic;
using System.Text;

namespace TestioProject.PL.Enums
{
    public class Common
    {
        public enum QuestionsActionType
        {
            AddAnswer = 1,
            NextQuestion = 2,
            RemoveAnswer = 3,
            RemoveQuestion = 4,
            Finish = 5
        }
        public enum TestsActionType
        {
            CreateTest = 1,
            EditTest = 2
        }
    }
}
