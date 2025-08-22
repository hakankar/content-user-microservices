using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Content
{
    public class GetContentHandler : IRequestHandler<GetContentQuery, GetContentResponse>
    {
        private readonly IContentManagement _contentManagement;
        private readonly IMapper _mapper;
        public GetContentHandler(IContentManagement contentManagement, IMapper mapper)
        {
            _contentManagement = contentManagement;
            _mapper = mapper;
        }

        public async Task<GetContentResponse> Handle(GetContentQuery request, CancellationToken cancellationToken)
        {
            var response = new GetContentResponse();

            response.Data = await _contentManagement.GetAll(false)
                .Where(x=>x.Id == request.Id)
                .ProjectTo<ContentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (response.Data == null)
                throw new CustomException("Content not found.", ExceptionType.NotFound);

            return response;
        }
    }
}
