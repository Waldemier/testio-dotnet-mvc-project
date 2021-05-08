using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestioProject.PL.Models
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Avatar url")]
        public string AvatarUri { get; set; }
        [Required]
        //[RegularExpression(@"/\S+@\S+\.\S+/i", ErrorMessage = "Email didnt valid")]
        public string Email { get; set; }
        public bool Baned { get; set; }
    }
    
    public class UserEditModel
    {
    }
}
