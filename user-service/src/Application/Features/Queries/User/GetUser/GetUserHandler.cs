using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.User
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private readonly IUserManagement _userManagement;
        private readonly IMapper _mapper;
        public GetUserHandler(IUserManagement userManagement, IMapper mapper)
        {
            _userManagement = userManagement;
            _mapper = mapper;
        }

        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var response = new GetUserResponse();

            response.Data = await _userManagement.GetAll(false)
                .Where(x=>x.Id == request.Id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (response.Data == null)
                throw new CustomException("User not found.", ExceptionType.NotFound);

            return response;
        }
    }
}
