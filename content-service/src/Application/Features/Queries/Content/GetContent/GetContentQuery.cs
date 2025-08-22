using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.Content
{
    public sealed class GetContentQuery : IRequest<GetContentResponse>
    {
        public Guid Id { get; set; }
    }

    public sealed class GetContentResponse : BaseResponse<ContentDto>
    {

    }
}
