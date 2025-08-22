using Application.Common;
using Domain.BoundedContexts.UserContext.UserAggregate;
using MediatR;

namespace Application.Features.Commands.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse>
    {
        private readonly IUserManagement _userManagement;
        public CreateUserHandler(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        public async Task<BaseResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var user = await _userManagement.CreateUserAsync(request.FullName, request.Email, request.Password, cancellationToken);
            await _userManagement.CompleteCreateAsync(user,cancellationToken);
            return response;
        }
    }
}
