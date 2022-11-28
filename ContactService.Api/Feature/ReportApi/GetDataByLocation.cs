using AutoMapper;
using ContactService.Api.Common.Models;
using ContactService.Api.Infrastrcuture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Api.Feature.ReportApi;

public class GetDataByLocationController : ApiControllerBase
{
    [HttpGet("/api/report/{id}/data-by-location")]
    public Task<GetDataByLocationVm> GetDataByLocation(int id)
    {
        return Mediator.Send(new GetDataByLocationQuery()
        {
            ReportId = id
        });
    }
}

public class GetDataByLocationQuery : IRequest<GetDataByLocationVm>
{
    public int ReportId { get; set; }
}

public class GetDataByLocationQueryValidator : AbstractValidator<GetDataByLocationQuery>
{
}

public class GetDataByLocationVm
{
    public int ReportId { get; set; }
    public List<GetDataByLocationDto> Data { get; set; } = new List<GetDataByLocationDto>();
}

public class GetDataByLocationDto
{
    public string Location { get; set; }
    public int UserCount { get; set; }
    public int TelephoneCount { get; set; }
}

internal class GetDataByLocationQueryHandler : IRequestHandler<GetDataByLocationQuery, GetDataByLocationVm>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDataByLocationQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetDataByLocationVm> Handle(GetDataByLocationQuery request,
        CancellationToken cancellationToken)
    {
        return new GetDataByLocationVm()
        {
            ReportId = request.ReportId,
            Data = await _context.ContactItems
                .Include(i => i.User)
                .Include(i => i.ContactItemType)
                .Where(c => c.ContactItemType.Type == "Location" && c.IsValid == 1 && c.User.IsValid == 1)
                .GroupBy(g => g.Value)
                .Select(s => new GetDataByLocationDto
                {
                    Location = s.Key,
                    UserCount = s.Count(),
                    TelephoneCount = s.Sum(x =>
                        x.User.ContactItems.Count(c => c.ContactItemType.Type == "Telephone" && c.IsValid == 1))
                })
                .AsNoTracking()
                .ToListAsync()
        };
    }
}