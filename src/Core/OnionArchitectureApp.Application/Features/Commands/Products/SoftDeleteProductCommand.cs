using MediatR;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class SoftDeleteProductCommand : IRequest<ResponseWrapper<Guid>>
{
    public Guid Id { get; set; }
}
