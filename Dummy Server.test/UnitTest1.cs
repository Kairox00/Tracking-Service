using Dummy_Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;

namespace Dummy_Server.Tests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void GetGPropTest()
        {
            //Arrange
            var id = "123";
            var gIndex = "3";
            var controller = new SpecController();

            //Act
            var actionResult = controller.GetGProp(id, gIndex);

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var gprop = okObjectResult.Value as Dictionary<string, string>;
            Assert.NotNull(gprop);
            output.WriteLine(string.Join(", ", gprop.Select(x => $"{{{x.Key}: {x.Value}}}").ToArray()));

        }
    }
}