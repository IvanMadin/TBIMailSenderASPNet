using Google.Apis.Gmail.v1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using EmailManager.Service;
using System.Threading.Tasks;
using EmailManager.Service.Providers;
using EmailManager.Service.Contracts;
using Google.Apis.Gmail.v1.Data;
using System.Collections.Generic;

namespace EmailManager.GmailConfig
{
    public class GmailConfigure
    {
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";
        private readonly IEmailService emailService;
        private readonly IAttachmentsService attachmentsService;

        public GmailConfigure(IEmailService emailService, IAttachmentsService attachmentsService)
        {
            this.emailService = emailService;
            this.attachmentsService = attachmentsService;
        }


        public async Task GmailAPI()
        {
            UserCredential credential = ApproveCredentialFromFile();

            GmailService gmailService = CreateGmailService(credential);

            var emailListRequest = gmailService.Users.Messages.List("krisi.madin123@gmail.com");

            emailListRequest.LabelIds = "INBOX";
            emailListRequest.IncludeSpamTrash = false;

            var emailListResponse = emailListRequest.ExecuteAsync().Result;

            if (emailListResponse != null && emailListResponse.Messages != null)
            {
                foreach (var email in emailListResponse.Messages)
                {
                    var emailRequest = gmailService.Users.Messages.Get("krisi.madin123@gmail.com", email.Id);

                    Message emailFullResponse = emailRequest.ExecuteAsync().Result;

                    if (emailFullResponse != null)
                    {
                        string originalMailId = emailFullResponse.Id;

                        bool checkIfEmailIsInDB = await emailService.CheckIfEmailExists(originalMailId);

                        if (!checkIfEmailIsInDB && !emailFullResponse.LabelIds.Contains("CATEGORY_PROMOTIONS"))
                        {

                            GetEmailInformationByParts(emailFullResponse, out DateTime dateReceived, out string senderName, out string senderEmail, out string subject, out string body, out List<string> attachmentNames, out List<double> attachmentSizes);

                            var createdEmail = await emailService.CreateAsync(originalMailId, senderName, senderEmail, dateReceived, subject, body);

                            await this.attachmentsService.CreateAsync(createdEmail.Id, attachmentNames, attachmentSizes);
                        }
                    }
                }
            }

            gmailService.Dispose();
        }

        private static UserCredential ApproveCredentialFromFile()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("../EmailManager.GmailConfig/credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "../EmailManager.GmailConfig/token.json";
                credential =
                    GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }
        private static GmailService CreateGmailService(UserCredential credential)
        {
            return new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
        private void GetEmailInformationByParts(Message emailFullResponse, out DateTime dateReceived, out string senderName, out string senderEmail, out string subject, out string body, out List<string> attachmentNames, out List<double> attachmentSizes)
        {
            attachmentNames = new List<string>();
            attachmentSizes = new List<double>();
            long timeStamp = (long)emailFullResponse.InternalDate;
            dateReceived = DateTimeOffset.FromUnixTimeMilliseconds(timeStamp).DateTime.ToLocalTime();
            string sender = emailFullResponse.Payload.Headers
               .FirstOrDefault(x => x.Name == "From")
               .Value;
            (senderName, senderEmail) = SplitSender(sender);

            subject = emailFullResponse.Payload.Headers
                .FirstOrDefault(x => x.Name == "Subject")
                .Value;

            var multiPartToResolve = emailFullResponse.Payload;
            if (multiPartToResolve.MimeType == "multipart/mixed")
            {
                foreach (var part in multiPartToResolve.Parts.Where(x => !x.MimeType.StartsWith("multi")))
                {
                    attachmentNames.Add(part.Filename);
                    attachmentSizes.Add((double)part.Body.Size);
                }

                body = multiPartToResolve.Parts[0].Parts[1].Body.Data;
            }
            else
            {
                body = multiPartToResolve.Parts[1].Body.Data;
            }

            //var bodyToResolve = emailFullResponse.Payload.Parts[1];
            //// Attachments: "multipart/mixed"
            ////multipart/alternative
            //if (bodyToResolve.MimeType == "text/html")
            //{
            //    body = bodyToResolve.Body.Data;
            //}
            //else
            //{
            //    body = bodyToResolve.Parts[1].Body.Data;
            //}
        }

        /// <summary>
        /// Returns 2 strings first is the SenderName and 2nd SenderEmail
        /// </summary>
        private (string, string) SplitSender(string sender)
        {
            var input = sender.Split();
            var senderName = "";

            for (int i = 0; i < input.Length - 1; i++)
            {
                senderName += input[i] + " ";
            }
            var senderEmail = input[input.Length - 1].Replace("<", "").Replace(">", "");

            return (senderName.TrimEnd(), senderEmail);
        }
    }
}
