using ContactService.Api.Common.Models;
using ContactService.Api.Infrastrcuture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Api.Feature.User;

public class CreateContactItemController : ApiControllerBase
{
    [HttpPost("/api/users/{id}/contact-items")]
    public async Task<ActionResult<int>> Create(int id, CreateUserContactItemCommand command)
    {
        if (id != command.UserId)
        {
            return BadRequest();
        }
        
        return await Mediator.Send(command);
    }
}

public class CreateUserContactItemCommand : IRequest<int>
{
    public int UserId { get; set; }
    public int ContactItemTypeId { get; set; }
    public string Value { get; set; }
}

public class CreateUserContactItemCommandValidator : AbstractValidator<CreateUserContactItemCommand>
{
    public CreateUserContactItemCommandValidator()
    {
        RuleFor(v => v.Value)
            .MaximumLength(200)
            .NotEmpty();
    }
}

internal class CreateUserContactItemCommandHandler : IRequestHandler<CreateUserContactItemCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateUserContactItemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateUserContactItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new Core.Entities.ContactItem
        {
            UserId = request.UserId,
            ContactItemTypeId = request.ContactItemTypeId,
            Value = request.Value,
        };

        _context.ContactItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}