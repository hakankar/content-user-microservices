using Application.Abstraction.InternalApiServices;
using Application.Common;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Commands.Content
{
    public class CreateContentHandler : IRequestHandler<CreateContentCommand, BaseResponse>
    {
        private readonly IContentManagement _contentManagement;
        private readonly IUserApiService _userApiService;
        public CreateContentHandler(IContentManagement contentManagement, IUserApiService userApiService)
        {
            _contentManagement = contentManagement;
            _userApiService = userApiService;
        }

        public async Task<BaseResponse> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var user = await _userApiService.GetUserAsync(Guid.Parse(request.UserId), cancellationToken);
            if (user.Data == null)
                throw new CustomException("User not found.", ExceptionType.BadRequest);

            var content = await _contentManagement.CreateContentAsync(request.Title, request.Body, Guid.Parse(request.UserId), cancellationToken);
            await _contentManagement.CompleteCreateAsync(content,cancellationToken);
            return response;
        }
    }
}
