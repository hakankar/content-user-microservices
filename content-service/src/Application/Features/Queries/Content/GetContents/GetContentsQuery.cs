using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.Content
{
    public sealed class GetContentsQuery : IRequest<GetContentsResponse>
    {
    }

    public sealed class GetContentsResponse : BaseResponse<List<ContentDto>>
    {

    }
}
