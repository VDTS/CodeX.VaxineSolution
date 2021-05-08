using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace VaxineAppUITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void ProfileEditModeMatchProfileInfo()
        {
            // Arrange
            app.EnterText("EditProfilePage_Email", "email@example.com");

            // Act
            app.Tap("EditProfile_SaveButton");

            // Assert
            var result = app.Query("ProfilePage_Email").First(x => x.Label == "email@example.com");
        }
    }
}
