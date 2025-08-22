using Application.Common;
using MediatR;

namespace Application.Features.Commands.Content
{
    public sealed class DeleteContentCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }

}
