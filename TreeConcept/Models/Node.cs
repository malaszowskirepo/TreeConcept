using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeConcept.Models
{
    public class Node
    {
        [Key]
        public int AutoIncrement { get; set; }
        public int ID { get; set; }
        public int? Parent_ID { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Name must be longer than 3 characters.")]
        public string Name { get; set; }
       
    }
}
