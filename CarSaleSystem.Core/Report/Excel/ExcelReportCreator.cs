using System.Data;
using OfficeOpenXml;

namespace CarSaleSystem.Core.Report.Excel;

public class ExcelReportCreator : IReportCreator
{
    public ExcelReportCreator()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    private string _reportPath = @".\Reports\ExcelReports\";
    
    public void CreateReport(DataTable dataTable)
    {
        if (!Directory.Exists(_reportPath))
        {
            Directory.CreateDirectory(_reportPath);
        }
        using var excelPackage = new ExcelPackage();

        var workSheet = excelPackage.Workbook.Worksheets.Add("Отчет");

        workSheet.Cells["B2"].LoadFromDataTable(dataTable, true);
        
        var fileInfo = new FileInfo(_reportPath + $"report.xlsx");
        excelPackage.SaveAs(fileInfo);
    }
}