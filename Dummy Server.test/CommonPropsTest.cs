using Dummy_Server.Controllers;
using Dummy_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Dummy_Server.Tests
{
    public class CommonPropsTest
    {
        private readonly ITestOutputHelper output;

        public CommonPropsTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetOneTest()
        {
            var commonProps = CommonProps.Instance;
            string id = "1234";
            string common = "2";

            var actionResult = commonProps.GetOne(id, common);
            Assert.IsType<Dictionary<string, string>>(actionResult);

            output.WriteLine(string.Join(", ", actionResult.Select(x => $"{{{x.Key}: {x.Value}}}").ToArray()));

        }

        [Fact]
        public void GetAllTest()
        {
            var commonProps = CommonProps.Instance;
            string id = "1234";
            string common = "2";

            var actionResult = commonProps.GetAll(id, common);
            Assert.IsType<Dictionary<string, string>>(actionResult);

            output.WriteLine(string.Join(", ", actionResult.Select(x => $"{{{x.Key}: {x.Value}}}").ToArray()));

        }
    }
}
