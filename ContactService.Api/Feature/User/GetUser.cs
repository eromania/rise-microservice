using AutoMapper;
using ContactService.Api.Common.Mappings;
using ContactService.Api.Common.Models;
using ContactService.Api.Infrastrcuture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCommon.Exceptions;

namespace ContactService.Api.Feature.User;

public class GetUserController : ApiControllerBase
{
    [HttpGet("/api/users/{id}")]
    public Task<UserVm> GetUser(int id)
    {
        return Mediator.Send(new GetUserQuery()
        {
            Id = id
        });
    }
}

public class GetUserQuery : IRequest<UserVm>
{
    public int Id { get; set; }    
}

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}

public class UserVm
{
    public UserVm()
    {
        ContactItems = new List<ContactItemDto>();
    }
    
    public UserDto User { get; set; }
    public List<ContactItemDto> ContactItems { get; set; }
}

public class ContactItemDto : IMapFrom<Entities.ContactItem>
{
    public int Id { get; set; }
    public int ContactItemTypeId { get; set; }
    public string ContactItemType { get; set; }
    public string Value { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.ContactItem, ContactItemDto>()
            .ForMember(d => d.ContactItemType, opt => opt.MapFrom(s => s.ContactItemType.Type));
    }
}

internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .Include(x => x.ContactItems).ThenInclude(x=>x.ContactItemType)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Entities.User), request.Id);
        }
        
        return new UserVm
        {
            User = _mapper.Map<UserDto>(entity),
            ContactItems = _mapper.Map<List<ContactItemDto>>(entity.ContactItems.Where(c=>c.IsValid == 1))
        };
    }
}