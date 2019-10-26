using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Havoc.Tests
{
   public class CpuTests
   {
      [Test]
      public async Task stress()
      {
         var c = new Cpu();
         var cts = new CancellationTokenSource(5000);
         c.Stress(cts.Token);
      }
   }
}