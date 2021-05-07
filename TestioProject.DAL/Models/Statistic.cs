using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestioProject.DAL.Models
{
    public class Statistic
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        [Required]
        public int TestId { get; set; }
        public Test Test { get; set; }
        
        [DefaultValue(null)]
        public DateTime PassedAt { get; set; }
        public int Result { get; set; }
    }
}
