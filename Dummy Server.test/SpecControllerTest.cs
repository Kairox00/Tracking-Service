using Dummy_Server.Controllers;
using Dummy_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;

namespace Dummy_Server.Tests
{
    public class SpecControllerTest
    {
        private readonly ITestOutputHelper output;

        public SpecControllerTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void IdentifyTest()
        {
            var controller = new SpecController();

            var actionResult = controller.Identify();

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as SpecMessage;
            Assert.NotNull(response);
            output.WriteLine(string.Join(", ", response.properties.Select(x => $"{{{x.Key}: {x.Value}}}").ToArray()));

        }

        [Fact]
        public void TrackTest()
        {
            var controller = new SpecController();

            var actionResult = controller.Track();

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as SpecMessage;
            Assert.NotNull(response);
            output.WriteLine(string.Join(", ", response.properties.Select(x => $"{{{x.Key}: {x.Value}}}").ToArray()));

        }

        [Fact]
        public async void GetCommonPropTest()
        {
            //Arrange
            var id = "123";
            var gIndex = "2";
            var controller = new SpecController();

            //Act
            var actionResult = controller.GetCommonProp(id, gIndex);

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as Dictionary<string, string>;
            Assert.NotNull(response);
            output.WriteLine(string.Join(", ", response.Select(x => $"{{{x.Key}: {x.Value}}}").ToArray()));

        }
    }
}