using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Havoc
{
   public static class DataSize
   {
      public const int OneMB = 1024 * 1024 * 1;
   }

   public class Memory : Scenario
   {
      public void StackOverflow( TimeSpan? allocationDelay = null)
      {
         if( allocationDelay.HasValue ) Thread.Sleep(allocationDelay.Value);
         StackOverflow(allocationDelay);
      }

      public void MemoryLeak(int bufferSize = DataSize.OneMB, TimeSpan? allocationDelay = null, CancellationToken cancellationToken = default)
      {
         var list = new List<byte[]>();

         using (cancellationToken.Register(CleanUp))
         {
            while (!cancellationToken.IsCancellationRequested)
            {
               list.Add(new byte[bufferSize]);

               if (allocationDelay.HasValue)
               {
                  Thread.Sleep(allocationDelay.Value);
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