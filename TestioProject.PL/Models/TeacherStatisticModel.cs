using System;
using System.Collections.Generic;

namespace TestioProject.PL.Models
{
    public class TeacherStatisticViewModel
    {
        public int UsersAmount { get; set; }
        public float GPA { get; set; }
        public TestViewModel Test { get; set; }
    }

    public class TeacherStatisticEditModel
    {
    }
}
