using NUnit.Framework;
using System;
using VaxineApp.Converters;

namespace VaxineAppTest.Converters
{
    public class BornDateToAgeInMonthsConverterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AgeInMonths()
        {
            // Arrange
            var dOB = new DateTime(2021, 09, 28);
            BornDateToAgeInMonthsConverter bornDateToAgeInMonthsConverter = new BornDateToAgeInMonthsConverter();
            // Act
            var s = bornDateToAgeInMonthsConverter.Convert(dOB, null, null, null);

            // Assert
            Assert.AreEqual(8, s);
        }
    }
}