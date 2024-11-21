using CarSaleSystem.Database.Models.EntityTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarSaleSystem.Database.DbContext;

public sealed class DataContext : Microsoft.EntityFrameworkCore.DbContext, IDataContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    private IDbContextTransaction _transaction;

    /// <summary>
    /// Начать транзакцию
    /// </summary>
    public async Task BeginTransactionAsync() => _transaction = await Database.BeginTransactionAsync();

    private void DetachAllEntities()
    {
        var changedEntriesCopy = ChangeTracker.Entries()
            .Where(e => e.State != EntityState.Detached)
            .ToArray();

        foreach (var entry in changedEntriesCopy)
        {
            entry.State = EntityState.Detached;
        }
    }

    /// <summary>
    /// Принять изменения транзакции, или сохранить изменения,
    /// если транзакция не была запущена
    /// </summary>
    public async Task CommitAsync()
    {
        try
        {
            await SaveChangesAsync();
            if (_transaction != null) await _transaction.CommitAsync();
        }
        finally
        {
            _transaction?.Dispose();
            DetachAllEntities();
        }
    }

    /// <summary>
    /// Принять изменения транзакции, или сохранить изменения,
    /// если транзакция не была запущена
    /// </summary>
    public void Commit()
    {
        try
        {
            SaveChanges();
            _transaction?.Commit();
        }
        finally
        {
            _transaction?.Dispose();
            DetachAllEntities();
        }
    }

    /// <summary>
    /// Откат транзакции
    /// </summary>
    public async Task RollbackAsync()
    {
        try
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
            DetachAllEntities();
        }
        catch
        {
            // absorb all exceptions
        }
    }

    /// <summary>
    /// Создание моделей БД
    /// </summary>
    /// <param name="modelBuilder">билдер модели</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }


    /// <summary>
    /// Создание основных свойств модели
    /// </summary>
    /// <param name="modelBuilder">билдер модели</param>
    /// <typeparam name="TEntityType">тип модели БД</typeparam>
    /// <typeparam name="TKeyType">тип ключа модели БД</typeparam>
    private void CreateBaseEntity<TEntityType, TKeyType>(ModelBuilder modelBuilder) where TEntityType : DbEntity<TKeyType>
    {
        modelBuilder.Entity<TEntityType>().Property(p => p.Id).HasMaxLength(45);
        modelBuilder.Entity<TEntityType>().Property(e => e.CreateTime).HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<TEntityType>().Property(e => e.Deleted);
    }

    
    
}