using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeConcept.Models
{
    public class SeededNodes
    {
        public int? Seed { get; set; }
        public IOrderedEnumerable<Node> Nodes { get; set; }
        static public bool IsAscending { get; set; } = true;
    }
}
