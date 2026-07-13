using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models.Smtp
{
    public class SmtpResend
    {
        public int ID { get; set; }
        public string? ComputerName { get; set; }
        public string? TypeModule { get; set; }
        public string? TypeNumber { get; set; }
        public SmtpResponse? TypeError { get; set; }
        public DateTime? TimeStampError { get; set; }
        public DateTime? TimeStampSent { get; set; }
        public int? SendTries { get; set; }
        public int? ErrorCode { get; set; }
        public string? ErrorText { get; set; }
        public string? ServerAddress { get; set; }
        public string? ServerPort { get; set; }
        public string? PathTemplate { get; set; }
        public string? PathAttachment { get; set; }
        public string? PathDocument { get; set; }
        public string? EmailDestination { get; set; }
        public string? EmailName { get; set; }
        public string? EmailSubject { get; set; }
        public string? EmailHtml { get; set; }

    }
}
