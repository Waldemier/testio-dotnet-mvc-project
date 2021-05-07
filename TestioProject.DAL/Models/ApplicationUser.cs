using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TestioProject.DAL.Models
{
    public class ApplicationUser: IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        [DefaultValue("/any/DefaultAvatar.svg")]
        public string AvatarUri { get; set; }
        [PersonalData]
        [DefaultValue(false)]
        public bool Baned { get; set; }
    }
}
