using NUnit.Framework;
using System;
using Utility.Extensions;

namespace Test
{
    public class ExtensionsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EmailValidatorTest()
        {
            // Arrange
            DateTime firstDate = new DateTime(2021, 8, 27, 11, 32, 10);
            DateTime secondDate = new DateTime(2021, 8, 27, 11, 33, 30);

            // Act
            int result = firstDate.TimeDifferenceBySecs();

            // Assert
            Assert.AreEqual(result, 80);
        }

        [Test]
        public void EmailValidatorNegativeTest()
        {
            // Arrange
            DateTime firstDate = new DateTime(2021, 8, 27, 11, 32, 10);
            DateTime secondDate = new DateTime(2021, 8, 27, 11, 33, 30);

            // Act
            int result = firstDate.TimeDifferenceBySecs();

            // Assert
            Assert.AreNotEqual(result, 80);
        }

        [Test]
        public void IsEmptyOnInitializedObjectTest()
        {
            // Arrange
            TestClass test = new();

            // Act
            bool result = test.AreEmpty();

            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void IsEmptyOnNotInitializedObjectTest()
        {
            //// Arrange
            //TestClass test;

            //// Act
            //bool result = test.AreEmpty();

            //// Assert
            //Assert.IsTrue(result);
        }
        [Test]
        public void IsEmptyOnNotEmptyObjectTest()
        {
            // Arrange
            TestClass test = new();
            test.FirstTestProp = "test value 1";
            test.SecondTestPrep = "test value 2";
            test.ThirdTestPrep = 12;

            // Act
            bool result = test.AreEmpty();

            // Assert
            Assert.IsFalse(result);
        }

        public class TestClass
        {
            public string? FirstTestProp { get; set; }
            public string? SecondTestPrep { get; set; }
            public int ThirdTestPrep { get; set; }

            public bool IsEmpty()
            {
                if (FirstTestProp is null && SecondTestPrep is null && ThirdTestPrep == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}