using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Entities
{
    public class UserEntityTests
    {
        [Fact]
        public void Constructor_Should_Set_Properties_And_HashPassword()
        {
            // Act
            var user = new User("Test", "test@mail.com", "123456");

            // Assert
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal("Test", user.FullName);
            Assert.Equal("test@mail.com", user.Email);
            Assert.NotEqual("123456", user.Password);
            Assert.True(BCrypt.Net.BCrypt.Verify("123456", user.Password));
        }

        [Fact]
        public void Update_Should_Change_FullName_And_Email()
        {
            var user = new User("Test", "test@mail.com", "123456");

            // Act
            user.Update("New Name", "new@mail.com");

            // Assert
            Assert.Equal("New Name", user.FullName);
            Assert.Equal("new@mail.com", user.Email);
        }

        [Fact]
        public void UpdatePassword_Should_Hash_NewPassword()
        {
            var user = new User("Test", "test@mail.com", "123456");
            var oldHash = user.Password;

            // Act
            user.UpdatePassword("newPass", "ignored@mail.com");

            // Assert
            Assert.NotEqual(oldHash, user.Password);
            Assert.True(BCrypt.Net.BCrypt.Verify("newPass", user.Password));
        }

        [Fact]
        public void Delete_Should_Set_IsDeleted_And_DeletedDate()
        {
            var user = new User("Test", "test@mail.com", "123456");

            // Act
            user.Delete();

            // Assert
            Assert.True(user.IsDeleted);
            Assert.NotNull(user.DeletedDate);
        }
    }
}
