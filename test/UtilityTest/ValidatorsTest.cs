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
        public void EmailValidatorTest()
        {
            // Arrange
            string email = "someone@example.com";

            // Act
            bool result = EmailValidators.IsEmailValid(email);

            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void PhoneNumberWithoutCodeValidatorTest()
        {
            // Arrange
            string phoneNumber = "0700342312";

            // Act
            bool result = PhoneNumberValidator.IsPhoneNumberValid(phoneNumber);

            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void PhoneNumberWithInternationalCode1ValidatorTest()
        {
            // Arrange
            string phoneNumber = "0093700342312";

            // Act
            bool result = PhoneNumberValidator.IsPhoneNumberValid(phoneNumber);

            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void PhoneNumberWithInternationalCode2ValidatorTest()
        {
            // Arrange
            string phoneNumber = "+93700342312";

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