using System.Collections.Generic;
using System.Threading;

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
   }
}