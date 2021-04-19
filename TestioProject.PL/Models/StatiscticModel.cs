using System;
using System.Collections.Generic;
using System.Text;

namespace TestioProject.PL.Models
{
    public class StatiscticViewModel
    {
        public int testId { get; set; }
        public int Result { get; set; }
    }
    //public class StatiscticViewOwnerModel
    //{
    //    //Should be a test and our result.
    //}
    public class StatisticEditModel
    {
        public int testId { get; set; }
        public string userId { get; set; }
        public int Result { get; set; }
    }
}
