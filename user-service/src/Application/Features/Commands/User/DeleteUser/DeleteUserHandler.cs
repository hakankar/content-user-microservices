using Application.Common;
using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Commands.User
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, BaseResponse>
    {

        private readonly IUserManagement _userManagement;
        public DeleteUserHandler(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        public async Task<BaseResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var user = await _userManagement.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
                throw new CustomException("User not found.", Domain.Enums.ExceptionType.NotFound);

            //hard delete
            await _userManagement.CompleteDeleteAsync(user, cancellationToken);

            /*soft delete
             
            user = await _userManagement.DeleteUserAsync(user, cancellationToken);
            await _userManagement.CompleteUpdateAsync(user, cancellationToken);
             */


            return response;
        }
    }
}
