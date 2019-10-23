using System;
using System.Collections.Concurrent;
using System.Threading;
using Bogus;

namespace Havoc
{
   public static class EvilData
   {
      public const string Eicar = "";
   }

   public class Scenario
   {
      public Faker Faker { get; set; } = new Faker("en");
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
      /// Causes a process to exit and terminate when called.
      /// </summary>
      public void ProcessExit()
      {
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
      ///// <param name="threadCount">The number of threads to spawn. Default is no-limit, as many as the operating system will allow.</param>
      public void MassThreads(TimeSpan? spawnInterval = null, CancellationToken cancellationToken = default)
      {
         using( var gate = new ManualResetEventSlim(false) )
         {
            using( cancellationToken.Register(Release) )
            {
               while( true )
               {
                  var t = new Thread(() => gate.Wait());
                  if(spawnInterval is TimeSpan s) Thread.Sleep(s);
               }
            }

            void Release()
            {
               gate.Set();
            }
         }
      }

   }

}