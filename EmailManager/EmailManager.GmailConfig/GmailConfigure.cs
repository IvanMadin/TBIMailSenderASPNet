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
                string credPath = "token.json";
                credential =
                    GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
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

                        string dateReceived = emailFullResponse.Payload.Headers
                            .FirstOrDefault(x => x.Name == "Date")
                            .Value;

                        string sender = emailFullResponse.Payload.Headers
                           .FirstOrDefault(x => x.Name == "From")
                           .Value;

                        string subject = emailFullResponse.Payload.Headers
                            .FirstOrDefault(x => x.Name == "Subject")
                            .Value;

                        var stringBuilder = new StringBuilder();

                        //TODO: Probably Recursion is gonna be used here.
                        var emailToResolve = emailFullResponse.Payload.Parts[1];

                        if (emailToResolve.MimeType == "text/html")
                        {
                            //TODO: Move the encoding/Decoding somewhere else.
                            String codedBody = emailToResolve.Body.Data.Replace("-", "+");
                            codedBody = codedBody.Replace("_", "/");
                            byte[] data = Convert.FromBase64String(codedBody);
                            var result = Encoding.UTF8.GetString(data);

                            stringBuilder.Append(result);
                        }
                        else
                        {
                            String codedBody = emailToResolve.Parts[1].Body.Data.Replace("-", "+");
                            codedBody = codedBody.Replace("_", "/");
                            byte[] data = Convert.FromBase64String(codedBody);
                            var result = Encoding.UTF8.GetString(data);

                        }

                        string body = stringBuilder.ToString();

                        var IsAlreadyAtDatabase = await emailService.CheckIfEmailExists(originalMailId);

                        if (!IsAlreadyAtDatabase)
                        {
                            await emailService.CreateAsync(originalMailId, sender, dateReceived, subject, body);
                        }
                    }
                }
            }
        }
    }
}
