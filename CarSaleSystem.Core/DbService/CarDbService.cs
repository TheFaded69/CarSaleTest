using AutoMapper;
using CarSaleSystem.Database.Models;
using CarSaleSystem.Database.Repository;
using Serilog;

namespace CarSaleSystem.Core.DbService;

public class CarDbService : ICarDbService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryCreator<DbCar, Guid> _repositoryCreator;

    public CarDbService(IMapper mapper, IRepositoryCreator<DbCar, Guid> repositoryCreator)
    {
        _mapper = mapper;
        _repositoryCreator = repositoryCreator;
    }

    public async Task AddRandomCarAsync()
    {
        try
        {
            var brands = new[] { "Toyota", "Honda", "Ford", "Chevrolet", "BMW", "Audi" };
            var models = new[] { "Sedan", "SUV", "Hatchback", "Coupe", "Convertible" };
            var colors = new[] { "Red", "Blue", "Green", "Black", "White", "Silver" };
            var configurations = new[] { "Standard", "Sport", "Luxury", "Eco" };
            var random = new Random();
            var start = new DateTime(2000, 1, 1);
            var range = (DateTime.Today - start).Days;
            
            var car = new DbCar
            {
                Brand = brands[random.Next(brands.Length)],
                Model = models[random.Next(models.Length)],
                YearOfProduction = start.AddDays(random.Next(range)),
                Color = colors[random.Next(colors.Length)],
                Configuration = configurations[random.Next(configurations.Length)],
            };

            var rep = await _repositoryCreator.CreateRepositoryAsync();

            rep.Insert(car);
            await rep.CommitAsync();
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw;
        }
    }

    public async Task<Guid> GetRandomCarIdAsync()
    {
        try
        {
            var rep = await _repositoryCreator.CreateRepositoryAsync();

            var count = rep.Query.Count();

            return rep.Query.Skip(new Random().Next(count))
                .Take(1)
                .FirstOrDefault().Id;
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw;
        }
    }
}