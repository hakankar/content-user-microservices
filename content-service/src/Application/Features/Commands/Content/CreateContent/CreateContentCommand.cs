using Application.Common;
using Application.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Features.Commands.Content
{
    public sealed class CreateContentCommand : IRequest<BaseResponse>
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }

    public class CreateContentValidator : AbstractValidator<CreateContentCommand>
    {
        public CreateContentValidator()
        {
            RuleFor(r => r.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(50)
                .WithMessage("Title cannot exceed 50 characters.");

            RuleFor(r => r.Body)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Body cannot exceed 200 characters.");

            RuleFor(r => r.UserId)
             .NotEmpty()
             .WithMessage("UserId is required.")
             .Must(id => Guid.TryParse(id, out var g) && g != Guid.Empty)
             .WithMessage("User not found.");
        }
    }

}
