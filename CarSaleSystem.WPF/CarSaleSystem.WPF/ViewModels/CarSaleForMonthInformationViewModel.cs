using CommunityToolkit.Mvvm.ComponentModel;

namespace CarSaleSystem.WPF.ViewModels;

public partial class CarSaleForMonthInformationViewModel : ViewModelBase
{
    public int Month { get; set; }

    [ObservableProperty] private decimal _totalSales;
}