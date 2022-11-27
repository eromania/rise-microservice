using ContactService.Api.Common.Models;
using ContactService.Api.Infrastrcuture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCommon.Exceptions;

namespace ContactService.Api.Feature.User;

public class DeleteUserContactItemController : ApiControllerBase
{
    [HttpDelete("/api/users/{id}/contact-items/{contactItemId}")]
    public async Task<ActionResult> Delete(int id,int contactItemId)
    {
        await Mediator.Send(new DeleteUserContactItemCommand {UserId = id, ContactItemId = contactItemId});

        return NoContent();
    }
}

public class DeleteUserContactItemCommand : IRequest
{
    public int UserId { get; set; }
    public int ContactItemId { get; set; }
}

public class DeleteUserContactItemCommandValidator : AbstractValidator<DeleteUserContactItemCommand>
{
    public DeleteUserContactItemCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty();
        
        RuleFor(v => v.ContactItemId)
            .NotEmpty();
    }
}

internal class DeleteUserContactItemCommandHandler : IRequestHandler<DeleteUserContactItemCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteUserContactItemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserContactItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ContactItems.FirstAsync(c=>c.UserId == request.UserId && c.Id == request.ContactItemId);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Entities.ContactItem), request.ContactItemId);
        }

        entity.IsValid = 0;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}