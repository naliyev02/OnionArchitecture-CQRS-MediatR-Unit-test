using MediatR;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Commands.ProductTypes;

public class CreateProductTypeCommand : IRequest<ResponseWrapper<Guid>>
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
