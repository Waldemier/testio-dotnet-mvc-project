using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestioProject.PL.Models
{
    public class AnswerModel
    {
        public class AnswerViewModel
        {
            public string Name { get; set; }
            public bool isTruth { get; set; }
        }
        public class AnswerEditModel
        {
            [Required]
            public string Name { get; set; }
            [DefaultValue(false)]
            public bool isTruth { get; set; }
            [Required]
            public int questionId { get; set; }
        }
    }
}
