using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactService.Api.Common.Mappings;
using ContactService.Api.Common.Models;
using ContactService.Api.Infrastrcuture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Api.Feature.User;

public class GetUsersController : ApiControllerBase
{
    [HttpGet("/api/users")]
    public Task<IList<UserDto>> GetUsers()
    {
        return Mediator.Send(new GetUsersQuery());
    }
}

public class GetUsersQuery : IRequest<IList<UserDto>>
{
    
}

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
}

public class UserDto : IMapFrom<Entities.User>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }
}

internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IList<UserDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(x => x.IsValid == 1)
            .AsNoTracking()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}