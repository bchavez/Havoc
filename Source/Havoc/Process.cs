using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Bogus;

namespace Havoc
{
   public static class EvilData
   {
      public const string Eicar = "g+NKiFT5m3s/2gpOikH1zWfHGIb0P2WqJfoCM4uTKN5o6R/71rIs5ZZl+OcpKz/UWo1n0JMriPWFNVcWDZ0iNbL++FS7hB0f";
   }

   public class Scenario
   {
      public Faker Faker { get; set; } = new Faker("en");

      protected void SetupDedicatedThread(out Thread thread, out TaskCompletionSource<Void> tcs, ThreadStart starter, string method)
      {
         thread = new Thread(starter)
            {
               Name = $"{this.GetType().FullName}.{method} Thread"
            };

         tcs = new TaskCompletionSource<Void>();
      }
   }


   /// <summary>
   /// Dangerous methods relating to an operating system process.
   /// </summary>
   public class Process : Scenario
   {
      /// <summary>
      /// Consumes threads from the thread pool until there are no available worker threads in the thread pool.
      /// </summary>
      /// <returns>Method returns when there are no workers available.</returns>
      public void ThreadPoolStarvation(CancellationToken cancellationToken = default)
      {
         using( var gate = new ManualResetEventSlim(false) )
         {
            using( cancellationToken.Register(Release) )
            {
               while( true )
               {
                  ThreadPool.GetAvailableThreads(out var workerAvail, out var ioAvail);
                  for( int i = 0; i < workerAvail; i++ )
                  {
                     ThreadPool.QueueUserWorkItem(_ =>
                        {
                           gate.Wait();
                        });
                  }

                  Thread.Sleep(35_000); // Thread Pool recalibrates every .5 seconds.

                  if( workerAvail == 0 ) break;
               }
            }

            void Release()
            {
               gate.Set();
            }
         }
      }

      /// <summary>
      /// Causes a process to exit and terminate after some delay.
      /// </summary>
      /// <param name="terminateDelay">If no delay is specified, a random time is chosen.</param>
      public void ProcessExit(TimeSpan? terminateDelay = null)
      {
         var delay = terminateDelay ?? this.Faker.Date.Timespan(TimeSpan.FromDays(1));
         Thread.Sleep(delay);
         Environment.FailFast("Havoc Terminated Process");
      }

      public void ThreadPoolChaos(CancellationToken cancellationToken = default)
      {
         while (!cancellationToken.IsCancellationRequested)
         {
            Thread target = null;
            ThreadPool.QueueUserWorkItem(_ => target = Thread.CurrentThread);
            Thread.Sleep(35_000);
            if (target is null) continue;
            try
            {
               target.Abort();
            }
            catch { }
         }
      }

      /// <summary>
      /// Causes an ever growing amount of threads to be spawned in the process.
      /// </summary>
      /// <param name="spawnInterval">The amount of time to wait before spawning a new thread. Default is zero delay and spawn as many threads as fast as possible.</param>
      /// <param name="cancellationToken">Canceling the token will release the amassed threads.</param>
      public void MassThreads(TimeSpan? spawnInterval = null, CancellationToken cancellationToken = default)
      {
         using( var gate = new ManualResetEventSlim(false) )
         {
            using( cancellationToken.Register(Release) )
            {
               while( true )
               {
                  var t = new Thread(() => gate.Wait());
                  t.Start();
                  if(spawnInterval.HasValue) Thread.Sleep(spawnInterval.Value);
               }
            }

            void Release()
            {
               gate.Set();
            }
         }
      }



      /// <summary>
      /// Open a massive amount of open file handles. This method does not clean up.
      /// </summary>
      public void OpenFileHandles(string directory = null)
      {
         var drives = Environment.GetLogicalDrives();
         foreach( var drive in drives )
         {
            var files = Directory.GetFiles(drive, "*.*", SearchOption.AllDirectories);
            foreach( var file in files )
            {
               File.OpenRead(file);
            }
         }
      }

   }

}