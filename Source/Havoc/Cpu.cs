using System;
using System.Linq;
using System.Threading;

namespace Havoc
{
   public class Cpu : Scenario
   {
      /// <summary>
      /// Simulates an expensive CPU operation (all available cores).
      /// </summary>
      public void Stress(CancellationToken cancellationToken = default)
      {
         try
         {
            ExpensiveMethod(cancellationToken);
         }
         catch (OperationCanceledException)
         {
            // Expected exception in normal method flow
         }
      }

      private void ExpensiveMethod(CancellationToken cancellationToken)
      {
         ParallelEnumerable.Range(1, int.MaxValue)
            .WithCancellation(cancellationToken)
            .Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
      }
   }
}