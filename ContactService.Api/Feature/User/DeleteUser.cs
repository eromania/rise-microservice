using ContactService.Api.Common.Exceptions;
using ContactService.Api.Common.Models;
using ContactService.Api.Infrastrcuture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Api.Feature.User;

public class DeleteUserController : ApiControllerBase
{
    [HttpDelete("/api/users/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteUserCommand {Id = id});

        return NoContent();
    }
}

public class DeleteUserCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}

internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .FindAsync(new object[] {request.Id}, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Core.Entities.User), request.Id);
        }

        entity.IsValid = 0;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}