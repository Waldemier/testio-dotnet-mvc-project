using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestioProject.DAL.Models
{
    public class WrittenLetter
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string Reason { get; set; }
        [DefaultValue(null)]
        public DateTime CreatedAt { get; set; }
    }
}
