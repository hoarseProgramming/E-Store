using EStore.Api.Repositories;

namespace EStore.Api.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    public ICategoryRepository CategoryRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IOrderProductRepository OrderProductRepository { get; }
    public IUserRepository UserRepository { get; }
    public Task<int> SaveChangesAsync();
    public Task<int> SaveChangesWithModifiedIdentityAsync();
}