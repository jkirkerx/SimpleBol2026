using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models
{
    public class NLogObject
    {
        public string? Time { get; set; }
        public string? Level { get; set; }
        public string? Nested { get; set; }
    }

    public class NLogNestedMessage
    {
        public string? Message { get; set; }
    }

    public class NLogNestedException
    {
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
