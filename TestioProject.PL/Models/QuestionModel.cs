using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static TestioProject.PL.Models.AnswerModel;

namespace TestioProject.PL.Models
{
    public class QuestionViewModel
    {
        public int questionId { get; set; }
        public int testId { get; set; }
        public string Name { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
    public class QuestionEditModel
    {
        [DefaultValue(0)]
        public int questionId { get; set; }
        [DefaultValue(0)]
        public int testId { get; set; }
        [DefaultValue("")]
        public string Name { get; set; }
        [DefaultValue(null)]
        public List<AnswerEditModel> Answers { get; set; }
    }
}
