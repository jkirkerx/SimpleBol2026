using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace SimpleBol.Models
{
    public class PrintSettings
    {
        public string? ListViewIconSize { get; set; }
        public int PrintNumberOfCopies { get; set; }
        public PrintMethod PrintToMethod { get; set; }        
        public string? DefaultPrinter { get; set; }
        public string? DefaultDocument {  get; set; }
        public Duplex PrintDuplex { get; set; } = Duplex.Simplex;
    }

    public class PrintObject
    {
        public PrintSettings? PrintSettings { get; set; }
    }

    public enum PrintMethod {
        PrintDirect = 0,
        PrintPdf = 1
    }
}
