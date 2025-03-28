using EStore.Api.Database;
using EStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EStore.Api.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly EStoreContext _eStoreContext;
    private readonly IServiceProvider _serviceProvider;

    private bool _disposed = false;

    public UnitOfWork(EStoreContext eStoreContext, IServiceProvider serviceProvider)
    {
        _eStoreContext = eStoreContext;
        _serviceProvider = serviceProvider;
    }

    public ICategoryRepository CategoryRepository => _serviceProvider.GetService<ICategoryRepository>();
    public ICustomerRepository CustomerRepository => _serviceProvider.GetService<ICustomerRepository>();
    public IProductRepository ProductRepository => _serviceProvider.GetService<IProductRepository>();
    public IOrderRepository OrderRepository => _serviceProvider.GetService<IOrderRepository>();
    public IOrderProductRepository OrderProductRepository => _serviceProvider.GetService<IOrderProductRepository>();
    public IUserRepository UserRepository => _serviceProvider.GetService<IUserRepository>();

    public async Task<int> SaveChangesAsync()
    {
        return await _eStoreContext.SaveChangesAsync();
    }

    public async Task<int> SaveChangesWithModifiedIdentityAsync()
    {
        await _eStoreContext.Database.OpenConnectionAsync();

        int result = 0;

        try
        {
            await _eStoreContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Products ON");
            result = await _eStoreContext.SaveChangesAsync();
            await _eStoreContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Products OFF");
        }
        finally
        {
            await _eStoreContext.Database.CloseConnectionAsync();
        }

        return result;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _eStoreContext.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


}