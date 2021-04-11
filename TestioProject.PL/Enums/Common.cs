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
            PreviousQuestion = 5,
            Finish = 6
        }
        public enum TestsActionType
        {
            CreateTest = 1,
            EditTest = 2
        }
    }
}
