namespace CarSaleSystem.Core.DbService;

public interface ICarDbService
{
    Task AddRandomCarAsync();
    Task<Guid> GetRandomCarIdAsync();
}