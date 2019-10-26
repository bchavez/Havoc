using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Havoc
{
   /// <summary>
   /// Special internal struct that we use to signify that we are not interested in a Task[Void]'s result. 
   /// </summary>
   [EditorBrowsable(EditorBrowsableState.Never)]
   public struct Void
   {
      public static readonly Void Value = default;
   }

   public class Network : Scenario
   {
      /// <summary>
      /// Exhausts the number of available TCP/IP ports on the local operating system.
      /// </summary>
      /// <param name="localAdapterAddress">The local adapter to use when opening the TCP/IP port. Default is IPAddress.Any.</param>
      public void LocalTcpPortExhaustion(IPAddress localAdapterAddress = null, CancellationToken cancellationToken = default)
      {
         var listeners = new ConcurrentQueue<TcpListener>();

         using (cancellationToken.Register(CleanUp))
         {
            while (!cancellationToken.IsCancellationRequested)
            {
               for (int port = 0; port < IPEndPoint.MaxPort; port++)
               {
                  try
                  {
                     var tcp = new TcpListener(new IPEndPoint(localAdapterAddress, port));
                     tcp.Start();
                     listeners.Enqueue(tcp);
                  }
                  catch
                  {
                  }
               }

               Thread.Sleep(35_000);
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