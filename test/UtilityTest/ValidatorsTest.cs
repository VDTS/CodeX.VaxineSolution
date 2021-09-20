using NUnit.Framework;
using System;
using Utility.Validations;

namespace Test
{
    public class ValidatorsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("alsDkjf@32!!@")]
        [TestCase("ah123@9A8@@!!dfAA")]
        public void PasswordValidatorTest(string password)
        {
            // Arrange
            // passwords are in testcase

            // Act
            bool result = PasswordValidator.IsPasswordValid(password);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("someone@example.com")]
        [TestCase("A123@google.com")]
        public void EmailValidatorTest(string email)
        {
            // Arrange
            // emails are in testcase

            // Act
            bool result = EmailValidators.IsEmailValid(email);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("0700342312")]
        [TestCase("0093700342312")]
        [TestCase("+93700342312")]
        public void PhoneNumberValidatorTest(string phoneNumber)
        {
            // Arrange
            // phoneNumbers are in testcase

            // Act
            bool result = PhoneNumberValidator.IsPhoneNumberValid(phoneNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void VaccinePeriodValidatorTest()
        {
            // Arrange
            DateTime currentPeriod = new(2021, 8, 26);
            DateTime endDate = new(2021, 8, 28);
            DateTime startDate = new(2021, 8, 24);

            // Act
            bool result = VaccinePeriodValidator.IsPeriodAvailable(currentPeriod);

            // Assert
            Assert.IsTrue(result);
        }
    }
}