using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Infrastructure.Persistence;
using ReportService.Api.Models;
using ServiceCommon.Exceptions;

namespace ReportService.Api.Feature.Report;

public class UpdateReportDataController : ApiControllerBase
{
    [HttpPut("/api/reports/{id}")]
    public async Task<ActionResult> Update(int id, UpdateReportDataCommand command)
    {
        await Mediator.Send(new UpdateReportDataCommand() {Id = id, ReportData = command.ReportData});

        return NoContent();
    }
}

public class UpdateReportDataCommand : IRequest
{
    public int Id { get; set; }
    public string ReportData { get; set; }
}

public class UpdateReportDataCommandValidator : AbstractValidator<UpdateReportDataCommand>
{
    public UpdateReportDataCommandValidator()
    {
        RuleFor(v => v.ReportData)
            .NotEmpty();
    }
}

internal class UpdateReportDataCommandHandler : IRequestHandler<UpdateReportDataCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateReportDataCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateReportDataCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Reports
            .FindAsync(new object[] {request.Id}, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Entities.Report), request.Id);
        }

        entity.ReportData = request.ReportData;
        entity.Status = "completed";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}