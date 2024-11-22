using CarSaleSystem.Core.DTO;

namespace CarSaleSystem.Core.DbService;

public interface ISaleDbService
{
    /// <summary>
    /// Получить список продаж модели в месяце. Содержит в течении года (до 12 элементов для каждого месяца)
    /// </summary>
    /// <param name="selectedDate"></param>
    /// <returns></returns>
    Task<List<CarSaleForMonthInformationDTO>> GetCarSaleForYearInformationAsync(DateTime selectedDate);

    Task AddRandomOrder(Guid carId);
}