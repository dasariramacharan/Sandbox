using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Playground.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Playground.Web.UnitTests
{
    [TestFixture]
    public class CalculatorControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

       /* TODO: fix up tests
        * [Test]
        public void TestkAddLater_SchedulesAdd_WhenCalled()
        {
            // Arrange
            var client = new Mock<IBackgroundJobClient>();
            var controller = new CalculatorController(client.Object);
            int a = 1, b = 2;

            // Act
            controller.AddLater(new AddNumbersRequest { a = a, b = b };

            // Assert
            client.Verify(x => x.Create(
                It.Is<Job>(job => job.Method.Name == "AddNumbers"
                               && job.Args[0].ToString() == a.ToString()
                               && job.Args[1].ToString() == b.ToString()),
                It.IsAny<EnqueuedState>()));
        }


        [Test]
        public void TestAddNumbers_AddPostiveNumbers_ReturnsSum()
        {
            var client = new Mock<IBackgroundJobClient>();
            var controller = new CalculatorController(client.Object);
            int a = 1, b = 2;

            // Act
            var result = controller.AddNumbers(a, b);

            Assert.AreEqual(result, 3);
        }*/
    }


}