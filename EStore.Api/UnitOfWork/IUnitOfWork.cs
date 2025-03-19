using EStore.Api.Database;
using EStore.Api.Repositories;
using EStore.Api.Services;

namespace EStore.Api.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    public ICategoryRepository CategoryRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IOrderProductRepository OrderProductRepository { get; }
    public Task<int> SaveChangesAsync();
    public Task<int> SaveChangesWithModifiedIdentityAsync();
}