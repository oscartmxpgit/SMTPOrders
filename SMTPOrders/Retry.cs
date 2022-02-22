using System;

namespace SMTPOrders
{
    public abstract class Retry
    {
        public int NumberOfRetries { get; set; }

        public void ExecuteAction(Action action)
        {
            int currentRetry = 0;

            for (int i=0;i<10;i++)
            {
                try
                {
                    action();
                   throw new TimeoutException("El servicio tarda demasiado, lo intentaremos hasta 5 veces, este es el intento número " + (currentRetry + 1).ToString());
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());    
                    currentRetry++;

                    //if (currentRetry > NumberOfRetries || !IsTemporaryException(ex))
                    if (currentRetry > 4)
                        throw new TimeoutException("El servicio tarda demasiado, se ha cancelado"); 
                }
                
                
            }
        }

        public abstract bool IsTemporaryException(Exception ex);
    }
}
