using System;

namespace SMTPOrders
{
    public class WebTimeoutRetry
    {
        public WebTimeoutRetry(int numberOfRetries)
        {
            NumberOfRetries = numberOfRetries;
        }

        public int NumberOfRetries { get; set; }

        public void ExecuteAction(Action action)
        {
            int currentRetry = 0;

            for (; ; )
            {
                try
                {
                    action();

                    //lanzamos una excepción para simular que se ha agotado el tiempo de la solicitud
                    throw new TimeoutException();
                }
                catch (Exception ex)
                {                    
                    currentRetry++;
                    
                    if (ex is TimeoutException)
                        Console.WriteLine("El envío de correo está tardando. Este es el intento número " + (currentRetry).ToString());

                    
                    if (currentRetry >= NumberOfRetries)
                    {
                        Console.WriteLine("El servicio tarda demasiado, se ha cancelado");
                        Console.WriteLine(ex.ToString());
                        throw;
                    }
                        
                }
                
                
            }
        }

    }
}
