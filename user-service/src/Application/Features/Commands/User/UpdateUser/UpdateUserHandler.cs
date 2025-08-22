using Application.Common;
using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Commands.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse>
    {
        private readonly IUserManagement _userManagement;
        public UpdateUserHandler(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        public async Task<BaseResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var user = await _userManagement.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
                throw new CustomException("User not found.", ExceptionType.NotFound);
            user = await _userManagement.UpdateUserAsync(user, request.FullName, request.Email, cancellationToken);
            await _userManagement.CompleteUpdateAsync(user, cancellationToken);
            return response;
        }
    }
}
