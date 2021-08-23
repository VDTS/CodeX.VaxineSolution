using NUnit.Framework;
using UtilityLib.Validations;

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

        [Test]
        public void IsEmailValidatorWorksCorrectly()
        {
            // Arrange
            var email = "someone@example.com";

            // Act
            var result = EmailValidators.IsEmailValid(email);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}