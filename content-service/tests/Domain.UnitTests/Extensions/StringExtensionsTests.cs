using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        // SnakeCase
        [Theory]
        [InlineData("PascalCase", "pascal_case")]
        [InlineData("İstanbulCase", "istanbul_case")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ToLowerSnakeCase_Works(string input, string expected)
        {
            var result = input.ToLowerSnakeCase();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("PascalCase", "PASCAL_CASE")]
        [InlineData("İstanbulCase", "ISTANBUL_CASE")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ToUpperSnakeCase_Works(string input, string expected)
        {
            var result = input.ToUpperSnakeCase();
            Assert.Equal(expected, result);
        }

        private enum TestEnum
        {
            [Description("First Value")]
            First = 1,
            [Description("Second Value")]
            Second = 2
        }

        [Fact]
        public void GetEnumValueFromDescription_Should_Return_Default_When_NotFound()
        {
            var value = "Not Exist".GetEnumValueFromDescription<TestEnum>();
            Assert.Equal(default, value);
        }

        [Fact]
        public void GetEnumValueFromDescription_Should_Throw_When_NotEnum()
        {
            Assert.Throws<InvalidEnumArgumentException>(() =>
                "x".GetEnumValueFromDescription<string>());
        }

        // Parsers
        [Fact]
        public void ToInt32_ToInt64_ToDecimal_ToDouble_ToDateTime_Works()
        {
            Assert.Equal(123, "123".ToInt32());
            Assert.Equal(123L, "123".ToInt64());
            Assert.Equal(123m, "123".ToDecimal());
            Assert.Equal(123d, "123".ToDouble());
            Assert.NotEqual(default, "2024-01-01".ToDateTime());
        }

        [Fact]
        public void ToInt32_Invalid_Returns_Default()
        {
            Assert.Equal(0, "abc".ToInt32());
        }

        // Null / NotNull / Numeric
        [Fact]
        public void IsNull_IsNotNull_IsNumeric_Works()
        {
            Assert.True(((string)null).IsNull());
            Assert.True("".IsNull());
            Assert.True("abc".IsNotNull());
            Assert.True("123".IsNumeric());
            Assert.False("12a3".IsNumeric());
        }

        // Boolean
        [Fact]
        public void ToBoolean_String_Works()
        {
            Assert.True("true".ToBoolean());
            Assert.False("false".ToBoolean());
            Assert.False("abc".ToBoolean());
        }

        [Fact]
        public void ToBoolean_Object_Works()
        {
            Assert.True(((object)"true").ToBoolean());
            Assert.False(((object)null).ToBoolean());
        }

        // MaskEmail
        [Fact]
        public void MaskEmail_Should_Mask_Correctly()
        {
            var result = "abcdef@mail.com".MaskEmail(3);
            Assert.Equal("abc***@mail.com", result);
        }

        [Fact]
        public void MaskEmail_Should_Throw_When_Invalid()
        {
            Assert.Throws<ArgumentException>(() => "".MaskEmail());
            Assert.Throws<ArgumentException>(() => "nomail".MaskEmail());
            Assert.Throws<ArgumentOutOfRangeException>(() => "a@mail.com".MaskEmail(10));
        }

        // Encoding / Decoding
        [Fact]
        public void Encoding_Decoding_Should_Work()
        {
            var encoded = "test".StringEncoding();
            Assert.True(encoded.Decoding(out var decoded));
            Assert.Equal("test", decoded);
        }

        [Fact]
        public void Decoding_Should_Fail_For_Invalid()
        {
            Assert.False("abc".Decoding(out _)); 
            Assert.False("$$$=".Decoding(out _)); 
        }

        // ReplaceTurkishCharacters
        [Fact]
        public void ReplaceTurkishCharacters_Should_Replace()
        {
            var input = "çÇğĞıİöÖşŞüÜ";
            var expected = "cCgGiIoOsSuU";
            var result = input.ReplaceTurkishCharacters();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReplaceTurkishCharacters_Should_Return_Input_When_NullOrEmpty()
        {
            Assert.Null(((string)null).ReplaceTurkishCharacters());
            Assert.Equal("", "".ReplaceTurkishCharacters());
        }
    }
}
