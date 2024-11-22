using System.Collections.ObjectModel;
using CarSaleSystem.Core.DbService;
using CarSaleSystem.Core.DTO;
using CarSaleSystem.Core.Report;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarSaleSystem.WPF.ViewModels;

/// <summary>
/// ViewModel for MainWindow
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ISaleDbService _saleDbService;
    private readonly ICarDbService _carDbService;
    private readonly IReportCreator _reportCreator;
    private readonly IReportDataCreator _reportDataCreator;

    public MainWindowViewModel(ISaleDbService saleDbService, ICarDbService carDbService, IReportCreator reportCreator, IReportDataCreator reportDataCreator)
    {
        _saleDbService = saleDbService;
        _carDbService = carDbService;
        _reportCreator = reportCreator;
        _reportDataCreator = reportDataCreator;
    }

    [ObservableProperty] private DateTime _startDate = DateTime.Now;

    [ObservableProperty] private DateTime _endDate = DateTime.Now;

    private List<CarSaleForMonthInformationDTO> _currentData;
    
    public ObservableCollection<CarSaleForYearInformationViewModel> CarSaleForYear { get; set; } = [];

    [RelayCommand]
    private async void LoadData()
    {
        

        CarSaleForYear.Clear();
        
        _currentData = await _saleDbService.GetCarSaleForYearInformationAsync(StartDate);

        foreach (var carSaleForMonthInformationDto in _currentData)
        {
            if (CarSaleForYear.Any(cs => cs.Brand == carSaleForMonthInformationDto.Brand && cs.Model == carSaleForMonthInformationDto.Model))
            {
                CarSaleForYear.First(cs => cs.Brand == carSaleForMonthInformationDto.Brand && cs.Model == carSaleForMonthInformationDto.Model)
                    .SaleForMonth[carSaleForMonthInformationDto.Month - 1].TotalSales += carSaleForMonthInformationDto.TotalSales;
            }
            else
            {
                var item = new CarSaleForYearInformationViewModel()
                {
                    Brand = carSaleForMonthInformationDto.Brand,
                    Model = carSaleForMonthInformationDto.Model,
                    SaleForMonth = new List<CarSaleForMonthInformationViewModel>()
                    {
                        new(), new(), new(), new(), new(), new(), new(), new(), new(), new(), new(), new(),
                    },
                };

                item.SaleForMonth.Insert(carSaleForMonthInformationDto.Month - 1, new CarSaleForMonthInformationViewModel()); 
                
                CarSaleForYear.Add(item);
            }
        }
    }

    [RelayCommand]
    private void CreateReport()
    {
        _reportCreator.CreateReport(_reportDataCreator.DataTableCreate(_currentData));
    }
    
    [RelayCommand]
    private async void GenerateRandomData()
    {
        for (var i = 0; i < 300; i++)
        {
            await _carDbService.AddRandomCarAsync();
        }

        for (var i = 0; i < 10000; i++)
        {
            var carId = await _carDbService.GetRandomCarIdAsync();

            await _saleDbService.AddRandomOrder(carId);
        }
    }
}