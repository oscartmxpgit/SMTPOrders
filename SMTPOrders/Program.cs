using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SMTPOrders
{
    internal class Program
    {

        //https://docs.microsoft.com/en-us/answers/questions/630642/sending-smtp-email-asynchronously-with-multiple-re.html
        public static void Main(string[] args)
        {
            new WebTimeoutRetry().ExecuteAction(async () =>
            {
                WebRequest myWebRequest = WebRequest.Create("http://www.contoso.com");
                Console.WriteLine("\nThe Timeout time of the request before setting is : {0} milliseconds", myWebRequest.Timeout);

                // Set the 'Timeout' property in Milliseconds.
                myWebRequest.Timeout = 500000;

                //https://docs.microsoft.com/es-es/dotnet/api/system.net.webrequest.timeout?view=net-6.0
                using (var client = new HttpClient())
                {

                    WebResponse myWebResponse = myWebRequest.GetResponse();

                    //await client.GetAsync("http://some-website.com");


                }
            });
        }

        public class WebTimeoutRetry : Retry
        {            
            public override bool IsTemporaryException(Exception ex)
            {
                

                var webException = ex as WebException;
                
                if (webException?.Status == WebExceptionStatus.Timeout)
                    return true;

                return false;
            }
        }

        //https://docs.microsoft.com/es-es/dotnet/api/system.net.mail.smtpclient.send?view=net-6.0
        public static void CreateTestMessage(string server)
        {
            string to = "jane@contoso.com";
            string from = "ben@contoso.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Using the new SMTP client.";
            message.Body = @"Using this new feature, you can send an email message from an application very easily.";
            SmtpClient client = new SmtpClient(server);
            // Credentials are necessary if the server requires the client
            // to authenticate before it will send email on the client's behalf.
            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }
        }
    }

}

