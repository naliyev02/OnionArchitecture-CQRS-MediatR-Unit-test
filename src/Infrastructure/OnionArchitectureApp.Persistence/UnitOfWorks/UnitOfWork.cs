using OnionArchitectureApp.Application.Interfaces.UnitOfWork;

namespace OnionArchitectureApp.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }
}
