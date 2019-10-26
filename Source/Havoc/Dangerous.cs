using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Havoc
{
   public class Dangerous : Scenario
   {
      public Task<Void> DiskFull(string filePath, int bufferSize, TimeSpan allocationDelay, CancellationToken cancellationToken = default)
      {
         var path = filePath ?? Path.GetTempFileName();

         this.SetupDedicatedThread(out var thread, out var tcs, Start, nameof(DiskFull));

         thread.Start();

         return tcs.Task;

         void Start()
         {
            using (cancellationToken.Register(CleanUp))
            {
               while (!cancellationToken.IsCancellationRequested)
               {
                  using(var file =  File.OpenWrite(path) )
                  {
                     file.Write();
                  }
               }
            }
         }

         void CleanUp()
         {
            while (!listeners.IsEmpty)
            {
               if (listeners.TryDequeue(out var listener))
               {
                  try
                  {
                     listener.Stop();
                  }
                  catch { }
               }
            }
         }
      }
   }
}