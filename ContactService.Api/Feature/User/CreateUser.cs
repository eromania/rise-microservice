using ContactService.Api.Common.Models;
using ContactService.Api.Infrastrcuture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Api.Feature.User;

public class CreateUserController : ApiControllerBase
{
    [HttpPost("/api/users")]
    public async Task<ActionResult<int>> Create(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }
}

public class CreateUserCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
        
        RuleFor(v => v.Surname)
            .MaximumLength(200)
            .NotEmpty();
        
        RuleFor(v => v.Company)
            .MaximumLength(200)
            .NotEmpty();
    }
}

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new Entities.User
        {
            Name = request.Name,
            Surname = request.Surname,
            Company = request.Company
        };

        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}