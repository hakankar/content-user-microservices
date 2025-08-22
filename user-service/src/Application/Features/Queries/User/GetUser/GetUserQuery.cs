using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.User
{
    public sealed class GetUserQuery : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }

    public sealed class GetUserResponse : BaseResponse<UserDto>
    {

    }
}
