using Application.Common;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Commands.User
{
    public sealed class UpdateUserCommand : IRequest<BaseResponse>
    {
        [FromRoute]
        public Guid Id { get; set; }
        [FromBody]
        public string FullName { get; set; } = string.Empty;
        [FromBody]
        public string Email { get; set; } = string.Empty;

    }

    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(r => r.FullName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Full name is required.")
                .MaximumLength(200)
                .WithMessage("Full name cannot exceed 200 characters.");

            RuleFor(r => r.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("Invalid email address format.")
                .MaximumLength(50)
                .WithMessage("Email cannot exceed 50 characters.");
        }
    }

}
