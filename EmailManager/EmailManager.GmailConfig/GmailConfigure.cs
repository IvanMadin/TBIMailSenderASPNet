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

namespace EmailManager.GmailConfig
{
    public class GmailConfigure
    {
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";
        private readonly EmailService emailService;

        public GmailConfigure(EmailService emailService)
        {
            this.emailService = emailService;
        }


        public async Task GmailAPI()
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

            var gmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });



            var emailListRequest = gmailService.Users.Messages.List("krisi.madin123@gmail.com");

            emailListRequest.LabelIds = "INBOX";
            emailListRequest.IncludeSpamTrash = false;

            var emailListResponse = emailListRequest.ExecuteAsync().Result;

            if (emailListResponse != null && emailListResponse.Messages != null)
            {
                foreach (var email in emailListResponse.Messages)
                {
                    var emailRequest = gmailService.Users.Messages.Get("krisi.madin123@gmail.com", email.Id);

                    var emailFullResponse = emailRequest.ExecuteAsync().Result;

                    if (emailFullResponse != null)
                    {
                        string originalMailId = emailFullResponse.Id;

                        var IsAlreadyAtDatabase = await emailService.CheckIfEmailExists(originalMailId);

                        if (!IsAlreadyAtDatabase)
                        {
                            string dateReceived = emailFullResponse.Payload.Headers
                                .FirstOrDefault(x => x.Name == "Date")
                                .Value;

                            string sender = emailFullResponse.Payload.Headers
                               .FirstOrDefault(x => x.Name == "From")
                               .Value;
                            (var senderName, var senderEmail) = SplitSender(sender);

                            string subject = emailFullResponse.Payload.Headers
                                .FirstOrDefault(x => x.Name == "Subject")
                                .Value;

                            var stringBuilder = new StringBuilder();

                            string body = "";
                            var bodyToResolve = emailFullResponse.Payload.Parts[1];

                            if (bodyToResolve.MimeType == "text/html")
                            {
                                body = bodyToResolve.Body.Data;
                            }
                            else
                            {
                                body = bodyToResolve.Parts[1].Body.Data;
                            }



                            await emailService.CreateAsync(originalMailId, senderName, senderEmail, dateReceived, subject, body);
                        }
                    }
                }
            }
        }

        private (string, string) SplitSender(string sender)
        {
            var input = sender.Split();
            var senderName = "";
            for (int i = 0; i < input.Length - 1; i++)
            {
                senderName += input[i] + " ";
            }
            var senderEmail = input[input.Length - 1].Replace("<", "").Replace(">", "");

            return (senderName, senderEmail);
        }
    }
}
