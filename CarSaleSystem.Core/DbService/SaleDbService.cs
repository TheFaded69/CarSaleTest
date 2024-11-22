using AutoMapper;
using CarSaleSystem.Core.DTO;
using CarSaleSystem.Database.Models;
using CarSaleSystem.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CarSaleSystem.Core.DbService;

public class SaleDbService : ISaleDbService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryCreator<DbOrder, Guid> _repositoryCreator;

    public SaleDbService(IMapper mapper, IRepositoryCreator<DbOrder, Guid> repositoryCreator)
    {
        _mapper = mapper;
        _repositoryCreator = repositoryCreator;
    }

    
    public async Task<List<CarSaleForMonthInformationDTO>> GetCarSaleForYearInformationAsync(DateTime selectedDate)
    {
        try
        {
            var repository = await _repositoryCreator.CreateRepositoryAsync();

            var data = await repository
                .Query
                .Include(order => order.SoldCar)
                .Where(order => order.SaleDate.Year == selectedDate.Year)
                .GroupBy(order => new
                {
                    order.SoldCar.Model,
                    Month = order.SaleDate.Month,
                    order.SoldCar.Brand
                }).Select(group => new CarSaleForMonthInformationDTO()
                {
                    Model = group.Key.Model,
                    Brand = group.Key.Brand,
                    Month = group.Key.Month,
                    TotalSales = group.Sum(o => o.PurchasePrice),
                    Count = group.Count()
                })
                .OrderBy(dto => dto.Brand)
                .ThenBy(dto => dto.Month)
                .ToListAsync();

            return data;
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            return null;
        }    
    }

    public async Task AddRandomOrder(Guid carId)
    {
        var random = new Random();
        var start = new DateTime(2000, 1, 1);
        var range = (DateTime.Today - start).Days;
        
        var orderDb = new DbOrder
        {
            CarId = carId,
            SaleDate = start.AddDays(random.Next(range)),
            PurchasePrice = random.Next(500000, 5000000),
        };
        
        var rep = await _repositoryCreator.CreateRepositoryAsync();

        rep.Insert(orderDb);
        await rep.CommitAsync();
    }
}