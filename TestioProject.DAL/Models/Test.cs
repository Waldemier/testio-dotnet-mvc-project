﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestioProject.DAL.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DefaultValue("")]
        public string Description { get; set; }
        [DefaultValue(null)]
        public string CodeLock { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<Question> Questions { get; set; }
    }
}
