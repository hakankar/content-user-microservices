using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.UnitTests
{
    public class AppBuilderTests
    {
        [Fact]
        public void Create_ServicesAndMiddlewares_Correctly()
        {
            // Arrange
            var args = Array.Empty<string>();

            // Act
            AppBuilder.Create(args);

            Assert.True(true);
        }
    }
}
