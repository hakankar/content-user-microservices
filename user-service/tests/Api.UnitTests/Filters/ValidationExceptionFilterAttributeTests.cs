using Api.Filters;
using Domain.Common;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.UnitTests.Filters
{
    public class ValidationExceptionFilterAttributeTests
    {
        [Fact]
        public void OnActionExecuting_WhenModelStateIsValid_ShouldNotThrow()
        {
            // Arrange
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()
            );
            
            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                new object()
            );

            var filter = new ValidationExceptionFilterAttribute();

            // Assert 
            var ex = Record.Exception(() => filter.OnActionExecuting(context));
            Assert.Null(ex);
        }

        [Fact]
        public void OnActionExecuting_WhenModelStateIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()
            );

            actionContext.ModelState.AddModelError("Name", "Name is required");

            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                new object()
            );

            var filter = new ValidationExceptionFilterAttribute();

            // Assert
            var ex = Assert.Throws<CustomException>(() => filter.OnActionExecuting(context));
            Assert.Equal("Name is required", ex.Message);
            Assert.Equal(ExceptionType.ValidationError, ex.Type);
        }
    }
}
