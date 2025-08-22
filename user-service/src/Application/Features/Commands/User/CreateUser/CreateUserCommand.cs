using Application.Common;
using Application.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Features.Commands.User
{
    public sealed class CreateUserCommand : IRequest<BaseResponse>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
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

            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password is required.")
                .MaximumLength(50)
                .WithMessage("Password cannot exceed 50 characters.");
        }
    }

}
