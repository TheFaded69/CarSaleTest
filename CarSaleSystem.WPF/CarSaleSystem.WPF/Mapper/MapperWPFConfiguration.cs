using AutoMapper;
using CarSaleSystem.Core.DTO;
using CarSaleSystem.WPF.ViewModels;

namespace CarSaleSystem.WPF.Mapper;

public class MapperWPFConfiguration : Profile
{
    public MapperWPFConfiguration()
    {
        CreateMap<CarSaleForYearInformationViewModel, CarSaleForMonthInformationDTO>()
            .ReverseMap();
    }
}