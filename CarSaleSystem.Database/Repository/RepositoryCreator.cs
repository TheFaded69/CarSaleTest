using CarSaleSystem.Database.DbContext;
using CarSaleSystem.Database.Models.EntityTypes;
using Microsoft.EntityFrameworkCore;

namespace CarSaleSystem.Database.Repository;

public class RepositoryCreator<TModelType, TKeyType> : IRepositoryCreator<TModelType, TKeyType> where TModelType : DbEntity<TKeyType>
{
    public RepositoryCreator(IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    private readonly IDbContextFactory<DataContext> _contextFactory;

    public async Task<Repository<TModelType, TKeyType>> CreateRepositoryAsync()
        => new(await _contextFactory.CreateDbContextAsync());

    public Repository<TModelType, TKeyType> CreateRepository()
        => new(_contextFactory.CreateDbContext());
}