using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        
        [MaxLength(800)]
        public String Todo { get; set; }
        
        public byte Priority { get; set; }
        
        public DateTime? DueDate { get; set; }
    }
}