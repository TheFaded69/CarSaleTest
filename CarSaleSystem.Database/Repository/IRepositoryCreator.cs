using CarSaleSystem.Database.Models.EntityTypes;

namespace CarSaleSystem.Database.Repository;

public interface IRepositoryCreator<TModelType, TKeyType> where TModelType : DbEntity<TKeyType>
{
    Task<Repository<TModelType, TKeyType>> CreateRepositoryAsync();
    Repository<TModelType, TKeyType> CreateRepository();
}