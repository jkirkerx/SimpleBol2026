using Azure.Core;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;
using SimpleBol.Models.Smtp;
using NLog;
using System.IO;
using System.Net.Http;

namespace SimpleBol.Services.sendEngine
{
    public interface IOutlook365Sender
    {
        Task<SmtpResponse?> SendEmailAsync(
            SimpleBol.Models.Outlook365 settings,
            SimpleBol.Models.Smtp.MailMessage mailMessage,
            CancellationToken cancellationToken = default);
    }

    public class Outlook365Sender : IOutlook365Sender
    {
        // Initialize the logger
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<SmtpResponse?> SendEmailAsync(
            SimpleBol.Models.Outlook365 settings,
            SimpleBol.Models.Smtp.MailMessage mailMessage,
            CancellationToken cancellationToken = default)
        {
            SmtpResponse? smtpResponse = null;

            if (settings.ClientId != null && settings.ClientSecret != null && settings.TenantId != null)
            {
                ClientSecretCredential credential = new ClientSecretCredential(settings.TenantId, settings.ClientId, settings.ClientSecret);
                GraphServiceClient graphClient = new GraphServiceClient(credential);

                List<Recipient> toRecipients = new List<Recipient>();
                if (mailMessage.SendTo != null && mailMessage.SendTo.Any())
                {
                    foreach (var recipient in mailMessage.SendTo)
                    {
                        var recipientObject = new Recipient
                        {
                            EmailAddress = new Microsoft.Graph.Models.EmailAddress
                            {
                                Address = recipient.Email,
                                Name = recipient.Name,
                            }
                        };
                        toRecipients.Add(recipientObject);
                    }
                }

                List<Recipient> ccRecipients = new List<Recipient>();
                if (mailMessage.CcTo != null && mailMessage.CcTo.Any())
                {
                    foreach (var recipient in mailMessage.CcTo)
                    {
                        var recipientObject = new Recipient
                        {
                            EmailAddress = new Microsoft.Graph.Models.EmailAddress
                            {
                                Address = recipient.Email,
                                Name = recipient.Name,
                            }
                        };
                        ccRecipients.Add(recipientObject);
                    }
                }

                List<Recipient> bccRecipients = new List<Recipient>();
                if (mailMessage.BccTo != null && mailMessage.BccTo.Any())
                {
                    foreach (var recipient in mailMessage.BccTo)
                    {
                        var recipientObject = new Recipient
                        {
                            EmailAddress = new Microsoft.Graph.Models.EmailAddress
                            {
                                Address = recipient.Email,
                                Name = recipient.Name,
                            }
                        };
                        bccRecipients.Add(recipientObject);
                    }
                }

                Recipient? senderEmail = null;
                if (mailMessage.SendFrom != null)
                {
                    senderEmail = new Recipient
                    {
                        EmailAddress = new Microsoft.Graph.Models.EmailAddress
                        {
                            Address = mailMessage.SendFrom.Email,
                            Name = mailMessage.SendFrom.Name,
                        }
                    };
                }

                ItemBody? htmlContent = null;
                if (!string.IsNullOrEmpty(mailMessage.HtmlContent))
                {
                    htmlContent = new ItemBody
                    {
                        ContentType = BodyType.Html,
                        Content = mailMessage.HtmlContent
                    };
                    // Include plain text content as an additional property
                    htmlContent.AdditionalData["PlainTextContent"] = mailMessage.PlainTextContent;
                }

                List<Microsoft.Graph.Models.Attachment> attachments = new List<Microsoft.Graph.Models.Attachment>();
                if (mailMessage.Attachments != null && mailMessage.Attachments.Any())
                {
                    foreach (var attachment in mailMessage.Attachments)
                    {
                        byte[]? contentBytes = null;
                        if (!string.IsNullOrEmpty(attachment.FilePath) && File.Exists(attachment.FilePath))
                        {
                            contentBytes = File.ReadAllBytes(attachment.FilePath);
                        }
                        var fileAttachment = new FileAttachment
                        {
                            Name = attachment.FileName,
                            ContentBytes = contentBytes,
                        };
                        attachments.Add(fileAttachment);
                    }
                }

                Microsoft.Graph.Models.Message message = new Microsoft.Graph.Models.Message
                {
                    Sender = senderEmail,
                    ToRecipients = toRecipients,
                    CcRecipients = ccRecipients,
                    BccRecipients = bccRecipients,
                    Subject = mailMessage.Subject,
                    Body = htmlContent,
                    Attachments = attachments
                };

                Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody body = new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
                {
                    Message = message,
                    SaveToSentItems = settings.SaveToSentItems
                };

                try
                {
                    await graphClient.Users[settings.UserAccount].SendMail.PostAsync(body, null, cancellationToken);                    
                    Console.WriteLine("Email sent successfully using Outlook365!");

                    // Manufacture synthetic response
                    System.Net.Http.HttpContent responseBody = new System.Net.Http.StringContent("Email sent successfully!");
                    HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    responseMessage.Content = responseBody;
                    smtpResponse = new(true, System.Net.HttpStatusCode.OK, responseBody, responseMessage.Headers, SmtpError.None, "");

                }
                catch (Microsoft.Graph.ServiceException ex)
                {                    
                    Console.WriteLine($"Error sending email using Outlook365: {ex.Message}");
                    Console.WriteLine($"Status code: {ex.ResponseStatusCode}");

                    // Handle error and create SmtpResponse accordingly
                    smtpResponse = new SmtpResponse(false, System.Net.HttpStatusCode.BadRequest, null, null, SmtpError.None, ex.Message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email using Outlook365: {ex.Message}");

                    // Handle error and create SmtpResponse accordingly
                    smtpResponse = new SmtpResponse(false, System.Net.HttpStatusCode.InternalServerError, null, null, SmtpError.Program_Code, ex.Message);
                }
            }
            

            return smtpResponse;
        }


    }
}
