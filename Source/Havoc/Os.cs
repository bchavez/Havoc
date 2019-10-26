using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace Havoc
{
   public class Os : Scenario
   {
      /// <summary>
      /// Greedy allocate resources from the OS and runtime Mutex WaitHandles.
      /// </summary>
      /// <param name="cancellationToken"></param>
      public void MassMutex(CancellationToken cancellationToken = default)
      {
         var list = new List<Mutex>();

         while ( !cancellationToken.IsCancellationRequested )
         {
            list.Add(new Mutex());
         }

         foreach( var mutex in list )
         {
            mutex.Dispose();
         }
      }

      public void MassSystemTimer(CancellationToken cancellationToken = default)
      {
         var list = new ConcurrentQueue<System.Timers.Timer>();

         using (cancellationToken.Register(CleanUp))
         while( !cancellationToken.IsCancellationRequested )
         {
            var interval = this.Faker.Random.Int(0, 100);
            var t = new System.Timers.Timer(interval);
            t.Elapsed += Callback;
            t.Start();
            list.Enqueue(t);
         }

         void Callback(object sender, ElapsedEventArgs e)
         {
            var sleep = this.Faker.Random.Int(0, 50);
            Thread.Sleep(sleep);
         }

         void CleanUp()
         {
            while (!list.IsEmpty)
            {
               if (list.TryDequeue(out var timer))
               {
                  try
                  {
                     timer.Stop();
                     timer.Elapsed -= Callback;
                     timer.Dispose();
                  }
                  catch { }
               }
            }
         }
      }
   }
}