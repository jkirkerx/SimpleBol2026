using NLog;
using RestSharp;
using SendGrid;
using SendGrid.Helpers.Mail;
using SimpleBol.Models;
using SimpleBol.Models.MongoDb;
using SimpleBol.Models.Smtp;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace SimpleBol.Services.sendEngine
{
    public interface ISendGridSender
    {
        Task<EmailResponse?> SendEmailMessageAsync(
            SimpleBol.Models.SendGrid settings,
            SimpleBol.Models.Smtp.MailMessage mailMessage,
            CancellationToken cancellationToken = default);

    }

    public class SendGridSender : ISendGridSender
    {
        // Initialize the logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<EmailResponse?> SendEmailMessageAsync(
            SimpleBol.Models.SendGrid settings,
            SimpleBol.Models.Smtp.MailMessage mailMessage,
            CancellationToken cancellationToken = default)
        {
            EmailResponse? smtpResponse = null;
            Response? sendGridResponse = null;

            if (settings.ApiKey != null)
            {
                try
                {
                    string apiKey = settings.ApiKey;                    
                    var client = new SendGridClient(apiKey);
                    var message = new SendGridMessage()
                    {
                        From = new SendGrid.Helpers.Mail.EmailAddress(mailMessage.SendFrom?.Email, mailMessage.SendFrom?.Name),
                        Subject = mailMessage.Subject,
                        PlainTextContent = mailMessage.PlainTextContent,
                        HtmlContent = mailMessage.HtmlContent
                    };

                    // Add recipients
                    if (mailMessage.SendTo != null)
                    {
                        foreach (var sendTo in mailMessage.SendTo)
                        {
                            var emailAddress = sendTo.Email;
                            var name = sendTo.Name;
                            message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(emailAddress, name));
                        }
                    }

                    // Add BCC recipients
                    if (mailMessage.BccTo != null)
                    {
                        foreach (var bccTo in mailMessage.BccTo)
                        {
                            var emailAddress = bccTo.Email;
                            var name = bccTo.Name;
                            message.AddBcc(emailAddress, name);
                        }
                    }

                    // Add CC recipients
                    if (mailMessage.CcTo != null)
                    {
                        foreach (var ccTo in mailMessage.CcTo)
                        {
                            var emailAddress = ccTo.Email;
                            var name = ccTo.Name;
                            message.AddCc(emailAddress, name);
                        }
                    }

                    // Add attachments
                    if (mailMessage.Attachments != null)
                    {
                        foreach (var attachment in mailMessage.Attachments)
                        {
                            if (attachment.FilePath != null && attachment.FileName != null)
                            {
                                string filePath = attachment.FilePath;
                                string fileName = attachment.FileName;

                                if (File.Exists(filePath))
                                {
                                    using (FileStream fileStream = File.OpenRead(filePath))
                                    {
                                        using (MemoryStream memoryStream = new MemoryStream())
                                        {
                                            // Copy the content of the file to the MemoryStream
                                            fileStream.CopyTo(memoryStream);

                                            // Reset the MemoryStream position to the beginning
                                            memoryStream.Position = 0;

                                            // Add the attachment fileName and fileStream
                                            await message.AddAttachmentAsync(fileName, memoryStream);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Send the email message
                    sendGridResponse = await client.SendEmailAsync(message, cancellationToken);
                    if (sendGridResponse != null)
                    {                        
                        smtpResponse = BuildSendGripSmtpResponse(sendGridResponse, null);
                    }                    

                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine(ex.ToString());

                    if (sendGridResponse != null)
                    {
                        smtpResponse = BuildSendGripSmtpResponse(sendGridResponse, ex);
                    }

                    // Log the error message using NLog
                    logger.Error(ex, "An error occurred while sending email message: " + ex.Message.ToString());

                }
            }

            return smtpResponse;
        }

        private EmailResponse BuildSendGripSmtpResponse(Response response, Exception? ex)
        {
            HttpStatusCode statusCode = response.StatusCode;
            bool transmissionSuccess = (int)statusCode >= 200 && (int)statusCode < 300;
            System.Net.Http.HttpContent responseBody = response.Body;
            HttpResponseHeaders responseHeaders = response.Headers;
            string exceptionMessage = "";
                        
            // Calculate SmtpError
            SmtpError errorType = SmtpError.None;
            if (!transmissionSuccess)
            {
                errorType = SmtpError.Smtp_Code; // Consider other status codes for different errors
            }

            if (ex != null)
            {
                errorType = SmtpError.Program_Code;
                exceptionMessage = ex.ToString();
            } 

            // Process the response if needed
            var smtpResponse = new EmailResponse(
                success: transmissionSuccess,
                statusCode: statusCode,
                body: responseBody,
                headers: responseHeaders,
                typeError: errorType,
                exceptionMessage: exceptionMessage
            );

            return smtpResponse;

        }

    }


}
