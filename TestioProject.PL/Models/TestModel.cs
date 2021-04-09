using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TestioProject.DAL.Models;

namespace TestioProject.PL.Models
{
    public class TestViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UserViewModel Owner { get; set; }
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
