using System.ComponentModel;
using System.Threading;

namespace Bogus.Havoc
{
   public static class Program
   {
      /// <summary>
      /// Consumes threads from the thread pool.
      /// </summary>
      public static void ThreadPoolStarvation(CancellationToken cancellationToken = default)
      {
         var r = new Randomizer();

         var gate = new ManualResetEventSlim(false);

         using( cancellationToken.Register(Release) )
         {
            while( true )
            {
               ThreadPool.GetAvailableThreads(out var workerAvail, out var ioAvail);
               for ( int i = 0; i < workerAvail; i++ )
               {
                  ThreadPool.QueueUserWorkItem(_ =>
                     {
                        gate.Wait();
                     });
               }

               if( workerAvail == 0 ) break;
            }
         }

         void Release()
         {
            gate.Set();
         }
      }
   }
}