using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeConcept.Models.ViewModels
{
    public class SwitchNodeViewModel
    {
        [Range(typeof(int), "1", "100")]
        public int ID1 { get; set; }

        [Range(typeof(int), "1", "100")]
        public int ID2 { get; set; }
    }
}
