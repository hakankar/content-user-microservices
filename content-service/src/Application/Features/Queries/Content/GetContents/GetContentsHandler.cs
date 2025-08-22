using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.Content
{
    public class GetContentsHandler : IRequestHandler<GetContentsQuery, GetContentsResponse>
    {
        private readonly IContentManagement _contentManagement;
        private readonly IMapper _mapper;
        public GetContentsHandler(IContentManagement contentManagement, IMapper mapper)
        {
            _contentManagement = contentManagement;
            _mapper = mapper;
        }

        public async Task<GetContentsResponse> Handle(GetContentsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetContentsResponse();

            response.Data = await _contentManagement.GetAll(false)
                .ProjectTo<ContentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return response;
        }
    }
}
