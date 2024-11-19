using MediatR;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public double Price { get; set; }
}
