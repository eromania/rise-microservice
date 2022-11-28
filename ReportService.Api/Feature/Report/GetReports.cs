using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportService.Api.Infrastructure.Persistence;
using ReportService.Api.Mappings;
using ReportService.Api.Models;

namespace ReportService.Api.Feature.Report;

public class GetReportsController : ApiControllerBase
{
    [HttpGet("/api/reports")]
    public Task<IList<ReportDto>> GetUsers()
    {
        return Mediator.Send(new GetReportsQuery());
    }
}

public class GetReportsQuery : IRequest<IList<ReportDto>>
{
    
}

public class GetUsersQueryValidator : AbstractValidator<GetReportsQuery>
{
}

public class ReportDto : IMapFrom<Entities.Report>
{
    public int Id { get; set; }
    public DateTime RequestDateTime { get; set; }
    public string Status { get; set; }
    public string ReportData { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Report, ReportDto>()
            .ForMember(d => d.RequestDateTime, opt => opt.MapFrom(s => s.Created));
    }
}

internal class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, IList<ReportDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetReportsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<ReportDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reports
            .Where(x => x.IsValid == 1)
            .AsNoTracking()
            .ProjectTo<ReportDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => x.Status)
            .ToListAsync(cancellationToken);
    }
}