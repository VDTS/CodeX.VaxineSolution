using NUnit.Framework;
using System;
using Utility.DateTimeFuncs;
using Utility.Extensions;

namespace Test
{
    public class DateTimeFuncs
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(2024, 10, 6, 2021, 10, 11)]
        [TestCase(2022, 4, 3, 2021, 9, 4)]
        [TestCase(2022, 8, 10, 2022, 3, 4)]
        [TestCase(2022, 1, 10, 2022, 1, 2)]
        public void DateTimeFuncsExceptionTest(int sYear, int sMonth, int sDay,
                                                int eYear, int eMonth, int eDay)
        {
            // Arrange
            DateTime startDate = new DateTime(sYear, sMonth, sDay);
            DateTime endDate = new DateTime(eYear, eMonth, eDay);


            // Assert
            Assert.Throws<Exception>(() => { CustomDateTime.DatesDifference(startDate, endDate); });
        }

        [Test]
        [TestCase(2021, 10, 6, 2021, 10, 11, 5)]
        [TestCase(2021, 10, 1, 2021, 10, 4, 3)]
        [TestCase(2021, 12, 15, 2021, 12, 18, 3)]
        public void DateTimeFuncsDays(int sYear, int sMonth, int sDay,
                                       int eYear, int eMonth, int eDay,
                                       int days)
        {
            // Arrange
            DateTime startDate = new DateTime(sYear, sMonth, sDay);
            DateTime endDate = new DateTime(eYear, eMonth, eDay);


            // Act
            var result = CustomDateTime.DatesDifference(startDate, endDate);

            // Assert
            Assert.AreEqual(result, days);
        }

    }
}