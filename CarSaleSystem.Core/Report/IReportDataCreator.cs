using System.Data;
using CarSaleSystem.Core.DTO;

namespace CarSaleSystem.Core.Report;

public interface IReportDataCreator
{
    DataTable DataTableCreate(List<CarSaleForMonthInformationDTO> carSaleForMonthInformationDto);
}