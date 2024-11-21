namespace CarSaleSystem.Database.Models.EntityTypes;

public class DbEntityString : DbEntity<string>
{
    protected override bool IsEmpty(string id)
    {
        return string.IsNullOrEmpty(id);
    }
}