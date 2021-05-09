using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TestioProject.DAL.Models;

namespace TestioProject.PL.Models
{
    public class TestViewModel
    {
        public int testId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserViewModel Owner { get; set; }
        
        [DisplayName("Created at")]
        public DateTime CreatedAt { get; set; }
        
        [DisplayName("Referrer special link")]
        public Guid ReferrerToken { get; set; }
        
        public string CodeLock { get; set; }
    }

    public class TestEditModel
    {
        [DefaultValue(0)]
        public int testId { get; set; }
        [DefaultValue("")]
        public string Title { get; set; }
        [DefaultValue("")]
        public string Description { get; set; }
        [DefaultValue(null)]
        public string CodeLock { get; set; }
    }
}
