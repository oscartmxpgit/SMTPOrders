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
            //hasta 5 intentos
            new WebTimeoutRetry(5).ExecuteAction(() =>
            {
                //Realiza una solicitud a un identificador uniforme de recursos (URI)
                //sería el endpoint de nuestra API de correo
                WebRequest myWebRequest = WebRequest.Create("http://www.contoso.com");
                
                // tiempo de espera, en milisegunddos
                myWebRequest.Timeout = 50000;

                using (var client = new HttpClient())
                {
                    WebResponse myWebResponse = myWebRequest.GetResponse();
                }
            });
        }

        
    }

}

