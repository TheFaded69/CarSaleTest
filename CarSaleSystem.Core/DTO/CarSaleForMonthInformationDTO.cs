using System.ComponentModel;

namespace CarSaleSystem.Core.DTO;

public class CarSaleForMonthInformationDTO
{
    [Description("Бренд")]
    public string Brand { get; set; }
    
    [Description("Бренд")]
    public string Model { get; set; }
    
    [Description("Цвет")]
    public string Color { get; set; }
    
    [Description("Конфигурация")]
    public string Configuration { get; set; }
    
    [Description("Итого продаж")]
    public decimal TotalSales { get; set; }
    
    public int Month { get; set; }
    
    public int Count { get; set; }
    
   
}