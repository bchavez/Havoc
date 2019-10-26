using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Havoc
{
   public class Memory : Scenario
   {
      public void StackOverflow()
      {
         StackOverflow();
      }

      public Task<Void> MemoryLeak(int bufferSize, TimeSpan allocationDelay, CancellationToken cancellationToken = default)
      {
         var list = new List<byte[]>();

         this.SetupDedicatedThread(out var thread, out var tcs, Start, nameof(MemoryLeak));

         thread.Start();

         return tcs.Task;


         void Start()
         {
            using( cancellationToken.Register(CleanUp) )
            {
               while( !cancellationToken.IsCancellationRequested )
               {
                  list.Add(new byte[bufferSize]);

                  if ( allocationDelay != TimeSpan.Zero )
                  {
                     Thread.Sleep(allocationDelay);
                  }
               }
            }
         }

         void CleanUp()
         {
            list.Clear();
         }
      }

      public void OutOfMemory()
      {
         //1GB allocation, as fast as possible.
         MemoryLeak(1024 * 1024 * 1024, TimeSpan.Zero);
      }
   }
}