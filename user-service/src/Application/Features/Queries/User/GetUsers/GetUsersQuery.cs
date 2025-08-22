using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.User
{
    public sealed class GetUsersQuery : IRequest<GetUsersResponse>
    {
    }

    public sealed class GetUsersResponse : BaseResponse<List<UserDto>>
    {

    }
}
