using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; } 
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
    }
}