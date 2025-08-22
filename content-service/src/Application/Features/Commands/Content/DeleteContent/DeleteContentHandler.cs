using Application.Common;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Commands.Content
{
    public class DeleteContentHandler : IRequestHandler<DeleteContentCommand, BaseResponse>
    {

        private readonly IContentManagement _contentManagement;
        public DeleteContentHandler(IContentManagement contentManagement)
        {
            _contentManagement = contentManagement;
        }

        public async Task<BaseResponse> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var content = await _contentManagement.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (content == null)
                throw new CustomException("Content not found.", Domain.Enums.ExceptionType.NotFound);

            //hard delete
            await _contentManagement.CompleteDeleteAsync(content, cancellationToken);

            /*soft delete
             
            content = await _contentManagement.DeleteContentAsync(content, cancellationToken);
            await _contentManagement.CompleteUpdateAsync(content, cancellationToken);
             */


            return response;
        }
    }
}
