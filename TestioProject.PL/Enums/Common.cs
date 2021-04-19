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

        public enum ActionType
        {
            //For admin actions
            Confirm = 1,
            Reject = 2,
            //For test passing
            Start = 3,
            Cancel = 4,
            Next = 5,
            Finish = 6,
            Previous = 7
        }
    }
}
