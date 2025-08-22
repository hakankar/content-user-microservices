using Application.Common;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Commands.Content
{
    public class UpdateContentHandler : IRequestHandler<UpdateContentCommand, BaseResponse>
    {
        private readonly IContentManagement _contentManagement;
        public UpdateContentHandler(IContentManagement contentManagement)
        {
            _contentManagement = contentManagement;
        }

        public async Task<BaseResponse> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var content = await _contentManagement.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (content == null)
                throw new CustomException("Content not found.", ExceptionType.NotFound);
            content = await _contentManagement.UpdateContentAsync(content, request.Title, request.Body, Guid.Parse(request.UserId), cancellationToken);
            await _contentManagement.CompleteUpdateAsync(content, cancellationToken);
            return response;
        }
    }
}
