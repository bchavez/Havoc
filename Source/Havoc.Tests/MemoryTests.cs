using NUnit.Framework;

namespace Havoc.Tests
{
   [TestFixture]
   public class MemoryTests
   {
      private Memory m;

      [SetUp]
      public void BeforeEachTest()
      {
         m = new Memory();
      }


      [Test]
      public void can_stack_overflow()
      { 
         
      }
   }
}