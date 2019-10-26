using System;
using System.IO;
using System.Threading;
using Havoc.Crypto;

namespace Havoc
{
   public class Dangerous : Scenario
   {
      /// <summary>
      /// Append the data array to a file as much as possible until the disk is full.
      /// </summary>
      /// <param name="filePath">The file path to write to. The path must be a valid path; create any directory before hand.</param>
      /// <param name="data">The data buffer to write to disk; repeatedly.</param>
      /// <param name="allocationDelay">Any delay in buffer writes to disk.</param>
      /// <param name="cancellationToken">Token to stop the operation, but not remove the file.</param>
      public void DiskFull(string filePath, byte[] data, TimeSpan? allocationDelay, CancellationToken cancellationToken = default)
      {
         var path = filePath ?? Path.GetTempFileName();

         using (var file = File.OpenWrite(path))
         {
            while (!cancellationToken.IsCancellationRequested)
            {
               try
               {
                  file.Write(data, 0, data.Length);
               }
               catch
               {
               }

               if (allocationDelay.HasValue)
               {
                  Thread.Sleep(allocationDelay.Value);
               }
            }
         }
      }

      /// <summary>
      /// Write a random buffer of data repeatedly to a file as much as possible until the disk is full.
      /// </summary>
      /// <param name="filePath">File path to write a very large file.</param>
      /// <param name="writeBufferSize">The write buffer size. Default 1MB.</param>
      /// <param name="allocationDelay">Delays between buffer writes. Default no delay.</param>
      public void DiskFull(string filePath, int writeBufferSize = DataSize.OneMB, TimeSpan? allocationDelay = null, CancellationToken cancellationToken = default)
      {
         var path = filePath ?? Path.GetTempFileName();

         var data = this.Faker.Random.Bytes(writeBufferSize);

         DiskFull(path, data, allocationDelay, cancellationToken);
      }
      
      /// <summary>
      /// Write an anti-virus test string to disk that triggers anti-virus alerts.
      /// </summary>
      /// <param name="path">Path to write the file. If null, a temporary file is used.</param>
      public void WriteEicar(string filePath = null)
      {
         var file = filePath ?? Path.GetTempFileName();
         var data = EvilData.Eicar.DecryptBase64StringToString();
         File.WriteAllText(file, data);
      }

      /// <summary>
      /// Write an anti-virus test string to a folder on disk, creating as many Eicar files as possible.
      /// The directory will be filled with random file names and their contents with the Eicar test value.
      /// </summary>
      public void WriteEicarMany(string directory, CancellationToken cancellationToken = default)
      {
         var data = EvilData.Eicar.DecryptBase64StringToString();
         int i = 0;
         while( !cancellationToken.IsCancellationRequested )
         {
            var file = this.Faker.System.FileName();
            var path = Path.Combine(directory, file);

            try
            {
               File.WriteAllText(path, data);
            }
            catch { }
         }
      }
   }
}