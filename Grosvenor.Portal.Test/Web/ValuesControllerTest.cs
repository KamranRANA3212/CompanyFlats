using Grosvenor.Portal.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Grosvenor.Portal.Test.Web
{
    public class ValuesControllerTest
    {
        private readonly ValuesController controller;
        public ValuesControllerTest()
        {
            this.controller = new ValuesController();
        }

        [Fact]
        public void Get()
        {
            //Arrange
            //Act
            var result = this.controller.Get();
            //Assert
            Assert.True(result.Count() == 2);
        }
    }
}
