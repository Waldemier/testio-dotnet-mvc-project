using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestioProject.DAL.Models
{
    public class Restricted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public DateTime HowLong { get; set; }
    }
}
