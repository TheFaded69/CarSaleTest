namespace CarSaleSystem.WPF.ViewModels;

public class CarSaleForYearInformationViewModel : ViewModelBase
{
    public string Brand { get; set; }
    
    public string Model { get; set; }

    public List<CarSaleForMonthInformationViewModel> SaleForMonth { get; set; }
}