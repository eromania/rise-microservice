using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Infrastructure.Persistence;
using ReportService.Api.Models;
using ServiceCommon.Interfaces;

namespace ReportService.Api.Feature.Report;

public class CreateReportRequestController : ApiControllerBase
{
    [HttpPost("/api/reports")]
    public async Task<ActionResult<int>> Create(CreateReportRequestCommand command)
    {
        return await Mediator.Send(command);
    }
}

public class CreateReportRequestCommand : IRequest<int>
{
}

public class CreateReportRequestCommandValidator : AbstractValidator<CreateReportRequestCommand>
{
   
}

internal class CreateReportRequestCommandHandler : IRequestHandler<CreateReportRequestCommand, int>
{
    private readonly ApplicationDbContext _context;
    private readonly IMessageProducer _messagePublisher;


    public CreateReportRequestCommandHandler(ApplicationDbContext context, IMessageProducer messagePublisher)
    {
        _context = context;
        _messagePublisher = messagePublisher;
    }
    
    public async Task<int> Handle(CreateReportRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = new Entities.Report()
        {
            Status = "waiting",
        };

        _context.Reports.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        
        //publish report-request-quee
        _messagePublisher.SendMessage(entity.Id.ToString(), "report-request-queue");

        return entity.Id;
    }
}