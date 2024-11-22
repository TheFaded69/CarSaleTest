using System.Data;

namespace CarSaleSystem.Core.Report;

public interface IReportCreator
{
    void CreateReport(DataTable dataTable);
}