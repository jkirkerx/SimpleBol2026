using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models.Smtp
{    

    public class MailMessage
    {
        public EmailAddress? SendFrom { get; set; }
        public List<EmailAddress>? SendTo { get; set; }
        public List<EmailAddress>? CcTo { get; set; }
        public List<EmailAddress>? BccTo { get; set; }
        public string? Subject { get; set; }
        public string? HtmlContent {  get; set; }
        public string? PlainTextContent {  get; set; }
        public List<Attachment>? Attachments { get; set; }
    }

    public class EmailAddress
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
    }

    public class Attachment
    {
        public string? FilePath { get; set; }
        public string? FileName { get; set; }        
    }
}
