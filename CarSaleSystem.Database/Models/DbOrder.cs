using CarSaleSystem.Database.Models.EntityTypes;

namespace CarSaleSystem.Database.Models;

public class DbOrder  : DbEntityGuid
{
    public Guid CarId { get; set; }
    
    public virtual DbCar SoldCar { get; set; }
    
    public DateTime SaleDate { get; set; }
    
    public decimal PurchasePrice { get; set; }
}