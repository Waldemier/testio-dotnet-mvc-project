using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TestioProject.PL.Models
{
    public class StatiscticViewModel
    {
        public int testId { get; set; }
        public int Result { get; set; }
        
        [DisplayName("Passed at")]
        [DefaultValue(null)]
        public DateTime PassedAt { get; set; }
    }

    public class StatisticEditModel
    {
        public int testId { get; set; }
        public string userId { get; set; }
        public int Result { get; set; }
    }
}
