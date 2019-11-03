using System;
using System.IO;
using System.Net;
using System.Threading;
using static Havoc.DataSize;

namespace Havoc
{
   public class Disk : Scenario
   {
      /// <summary>
      /// Write the same buffer to disk repeatedly.
      /// </summary>
      /// <param name="path">The file path to write to. If the file exists, the file will be overwritten.</param>
      /// <param name="buffer">The buffer to write to disk.</param>
      /// <param name="writeDelay">The amount of time to wait before each buffered write. When null, no delay - write as fast as possible.</param>
      public void CachedWrites(string path, byte[] buffer, TimeSpan? writeDelay = null, CancellationToken cancellationToken = default)
      {
         while (!cancellationToken.IsCancellationRequested)
         {
            File.WriteAllBytes(path, buffer);
            if (writeDelay.HasValue)
            {
               Thread.Sleep(writeDelay.Value);
            }
         }
      }

      /// <summary>
      /// Allocate an array of data and repeatedly write the buffer to disk.
      /// </summary>
      /// <param name="path">The file path to write to. If the file exists, the file will be over written.</param>
      /// <param name="size">The size in bytes of the buffer to allocate. Default, 1MB.</param>
      /// <param name="writeDelay">The amount of time to wait before each buffered write. When null, no delay - write as fast as possible.</param>
      public void CachedWrites(string path, int size = OneMB, TimeSpan? writeDelay = null, CancellationToken cancellationToken = default)
      {
         var buffer = this.Faker.Random.Bytes(size);
         CachedWrites(path, buffer, writeDelay, cancellationToken);
      }
   }
}