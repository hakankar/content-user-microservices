using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.BoundedContexts.UserContext.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.User
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly IUserManagement _userManagement;
        private readonly IMapper _mapper;
        public GetUsersHandler(IUserManagement userManagement, IMapper mapper)
        {
            _userManagement = userManagement;
            _mapper = mapper;
        }

        public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var response = new GetUsersResponse();

            response.Data = await _userManagement.GetAll(false)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return response;
        }
    }
}
