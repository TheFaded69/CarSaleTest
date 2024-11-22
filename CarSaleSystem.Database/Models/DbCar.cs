using CarSaleSystem.Database.Models.EntityTypes;

namespace CarSaleSystem.Database.Models;

public class DbCar : DbEntityGuid
{
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public DateTime YearOfProduction { get; set; }
    
    public string Color { get; set; }
    
    public string Configuration { get; set; }
    
    public virtual List<DbOrder> Orders { get; set; } 
}