using System.Data;
using CarSaleSystem.Core.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarSaleSystem.Core.Report.Excel;

public class DataTableCreator : IReportDataCreator
{
    public DataTableCreator()
    {
    }

    public DataTable DataTableCreate(List<CarSaleForMonthInformationDTO> carSaleForMonthInformationDto)
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("Бренд", typeof(string));
        dataTable.Columns.Add("Модель", typeof(string));
        dataTable.Columns.Add("Цвет", typeof(string));
        dataTable.Columns.Add("Конфигурация", typeof(string));
        dataTable.Columns.Add("Январь", typeof(decimal));
        dataTable.Columns.Add("Февраль", typeof(decimal));
        dataTable.Columns.Add("Март", typeof(decimal));
        dataTable.Columns.Add("Апрель", typeof(decimal));
        dataTable.Columns.Add("Май", typeof(decimal));
        dataTable.Columns.Add("Июнь", typeof(decimal));
        dataTable.Columns.Add("Июль", typeof(decimal));
        dataTable.Columns.Add("Август", typeof(decimal));
        dataTable.Columns.Add("Сентябрь", typeof(decimal));
        dataTable.Columns.Add("Октябрь", typeof(decimal));
        dataTable.Columns.Add("Ноябрь", typeof(decimal));
        dataTable.Columns.Add("Декабрь", typeof(decimal));


        var data = carSaleForMonthInformationDto.GroupBy(data => new { data.Brand, data.Model, data.Color, data.Configuration })
            .Select(g => new
            {
                g.Key.Model,
                g.Key.Brand,
                g.Key.Configuration,
                g.Key.Color,
                January = g.Where(p => p.Month == 1).Select(dto => dto.TotalSales).Sum(),
                February = g.Where(p => p.Month == 2).Select(dto => dto.TotalSales).Sum(),
                March = g.Where(p => p.Month == 3).Select(dto => dto.TotalSales).Sum(),
                April = g.Where(p => p.Month == 4).Select(dto => dto.TotalSales).Sum(),
                May = g.Where(p => p.Month == 5).Select(dto => dto.TotalSales).Sum(),
                June = g.Where(p => p.Month == 6).Select(dto => dto.TotalSales).Sum(),
                Jule = g.Where(p => p.Month == 7).Select(dto => dto.TotalSales).Sum(),
                August = g.Where(p => p.Month == 8).Select(dto => dto.TotalSales).Sum(),
                September = g.Where(p => p.Month == 9).Select(dto => dto.TotalSales).Sum(),
                October = g.Where(p => p.Month == 10).Select(dto => dto.TotalSales).Sum(),
                November = g.Where(p => p.Month == 11).Select(dto => dto.TotalSales).Sum(),
                December = g.Where(p => p.Month == 12).Select(dto => dto.TotalSales).Sum(),
            }).ToList();

        foreach (var itemGroup in data)
        {
            dataTable.Rows.Add(itemGroup.Brand, itemGroup.Model, itemGroup.Color, itemGroup.Configuration, 
                itemGroup.January, 
                itemGroup.February,
                itemGroup.March,
                itemGroup.April,
                itemGroup.May, 
                itemGroup.June,
                itemGroup.Jule,
                itemGroup.August,
                itemGroup.September,
                itemGroup.October,
                itemGroup.November, 
                itemGroup.December);
        }

        return dataTable;
    }
}