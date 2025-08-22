using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.InternalFeatures.Queries.User
{
    public sealed class GetInternalUserQuery : IRequest<GetInternalUserResponse>
    {
        public Guid Id { get; set; }
    }

    public sealed class GetInternalUserResponse : BaseResponse<UserDto>
    {

    }
}
