using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestioProject.DAL.Models;

namespace TestioProject.PL.Models
{
    public class WrittenLetterModel
    {
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
