using NUnit.Framework;

namespace VaxineAppNUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SampleTest()
        {
            // Arrange
            int a = 1;
            int b = 2;

            // Act
            int c = a + b;
            
            // Assert
            Assert.AreEqual(c, 3);
        }
    }
}