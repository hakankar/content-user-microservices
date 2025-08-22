using Application.Common;
using Application.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Features.Commands.User
{
    public sealed class DeleteUserCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }

}
