using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestioProject.DAL.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [DefaultValue(false)]
        public bool isTruth { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
