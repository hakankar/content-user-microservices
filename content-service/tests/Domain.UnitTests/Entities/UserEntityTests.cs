using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Entities
{
    public class ContentEntityTests
    {
        [Fact]
        public void Constructor_Should_Set_Properties()
        {
            // Act
            var userId = Guid.NewGuid();
            var content = new Content("Test", "testbody", userId);

            // Assert
            Assert.NotEqual(Guid.Empty, content.Id);
            Assert.Equal("Test", content.Title);
            Assert.Equal("testbody", content.Body);
            Assert.Equal(userId, content.UserId);
        }

        [Fact]
        public void Update_Should_Change()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());

            // Act
            content.Update("New Name", "testbody", content.UserId);

            // Assert
            Assert.Equal("New Name", content.Title);
            Assert.Equal("testbody", content.Body);
            Assert.Equal(content.UserId, content.UserId);
        }



        [Fact]
        public void Delete_Should_Set_IsDeleted_And_DeletedDate()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());

            // Act
            content.Delete();

            // Assert
            Assert.True(content.IsDeleted);
            Assert.NotNull(content.DeletedDate);
        }
    }
}
