namespace CarSaleSystem.Core.DTO;

public class CarSaleForMonthInformationDTO
{
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public int Month { get; set; }
    
    public decimal TotalSales { get; set; }
    
    public int Count { get; set; }
    
    public string Color { get; set; }
    
    public string Description { get; set; }
}