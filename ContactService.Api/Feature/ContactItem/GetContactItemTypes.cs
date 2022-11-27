using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactService.Api.Common.Mappings;
using ContactService.Api.Common.Models;
using ContactService.Api.Entities;
using ContactService.Api.Infrastrcuture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Api.Feature.ContactItem;

public class GetContactItemTypesController : ApiControllerBase
{
    [HttpGet("/api/contact-item-types")]
    public Task<IList<ContactItemTypeDto>> GetContactItemTypes()
    {
        return Mediator.Send(new GetContactItemTypesQuery());
    }
}

public class GetContactItemTypesQuery : IRequest<IList<ContactItemTypeDto>>
{
    
}

public class GetContactItemTypesQueryValidator : AbstractValidator<GetContactItemTypesQuery>
{
}

public class ContactItemTypeDto : IMapFrom<ContactItemType>
{
    public int Id { get; set; }

    public string Type { get; set; }
}

internal class GetContactItemTypesQueryHandler : IRequestHandler<GetContactItemTypesQuery, IList<ContactItemTypeDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetContactItemTypesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<ContactItemTypeDto>> Handle(GetContactItemTypesQuery request, CancellationToken cancellationToken)
    {
        return await _context.ContactItemTypes
            .Where(x => x.IsValid == 1)
            .AsNoTracking()
            .ProjectTo<ContactItemTypeDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.Type)
            .ToListAsync(cancellationToken);
    }
}