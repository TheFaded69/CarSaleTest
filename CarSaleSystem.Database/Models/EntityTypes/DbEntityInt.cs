namespace CarSaleSystem.Database.Models.EntityTypes;

public class DbEntityInt : DbEntity<int>
{
    protected override bool IsEmpty(int id)
    {
        return id == 0;
    }
}