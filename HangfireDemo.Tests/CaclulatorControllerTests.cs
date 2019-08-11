using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using HangfireDemo.Controllers;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckAddLater_SchedulesAdd_WhenCalled()
        {
            // Arrange
            var client = new Mock<IBackgroundJobClient>();
            var controller = new CalculatorController(client.Object);
            int a = 1, b = 2;

            // Act
            controller.AddLater(a, b);

            // Assert
            client.Verify(x => x.Create(
                It.Is<Job>(job => job.Method.Name == "AddNumbers"
                               && job.Args[0].ToString() == a.ToString()
                               && job.Args[1].ToString() == b.ToString()),
                It.IsAny<EnqueuedState>()));
        }
    }


}