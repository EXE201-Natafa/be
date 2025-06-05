using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml;
using System.IO;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.Services.Implements;
using Natafa.Api.Routes;

namespace Natafa.Api.Controllers
{
    public class ExcelController : BaseApiController
    {
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet]
        [Route(Router.ExcelRoute.GetMonthlyRevenueExcel)]
        public async Task<IActionResult> ExportMonthlyRevenueExcel()
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Monthly Revenue Chart");

                worksheet.Cells[1, 1].Value = $"Month/Year";
                worksheet.Cells[1, 2].Value = "Total Revenue";

                var result = await _excelService.GetRevenueByMonthAsync();

                int row = 2;
                foreach (var data in result)
                {
                    worksheet.Cells[row, 1].Value = $"{data.Month}/{data.Year}";
                    worksheet.Cells[row, 2].Value = data.Amount;
                    row++;
                }

                var chart = worksheet.Drawings.AddChart("MonthlyRevenueChart", eChartType.ColumnClustered);
                chart.Title.Text = $"Revenue by Month";
                chart.SetPosition(1, 0, 4, 0);
                chart.SetSize(800, 400);

                var series = chart.Series.Add(worksheet.Cells[2, 2, row - 1, 2], worksheet.Cells[2, 1, row - 1, 1]);
                series.Header = "Total Revenue";

                // Lưu dữ liệu vào stream
                package.SaveAs(stream);
            }

            // Trả về file Excel dưới dạng response
            stream.Position = 0;
            string excelName = "MonthlyRevenue.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpGet]
        [Route(Router.ExcelRoute.GetQuarterlyRevenueExcel)]
        public async Task<IActionResult> ExportQuarterlyRevenueExcel()
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Quarterly Revenue Chart");

                worksheet.Cells[1, 1].Value = $"Quarte/Year";
                worksheet.Cells[1, 2].Value = "Total Revenue";

                var result = await _excelService.GetRevenueByQuarterAsync();

                int row = 2;
                foreach (var data in result)
                {
                    worksheet.Cells[row, 1].Value = $"{data.Quarter}/{data.Year}";
                    worksheet.Cells[row, 2].Value = data.Amount;
                    row++;
                }

                var chart = worksheet.Drawings.AddChart("QuarterlyRevenueChart", eChartType.ColumnClustered);
                chart.Title.Text = $"Revenue by Quarter";
                chart.SetPosition(1, 0, 4, 0);
                chart.SetSize(800, 400);

                var series = chart.Series.Add(worksheet.Cells[2, 2, row - 1, 2], worksheet.Cells[2, 1, row - 1, 1]);
                series.Header = "Total Revenue";

                // Lưu dữ liệu vào stream
                package.SaveAs(stream);
            }

            // Trả về file Excel dưới dạng response
            stream.Position = 0;
            string excelName = "QuarterlyRevenue.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpGet]
        [Route(Router.ExcelRoute.GetYearlyRevenueExcel)]
        public async Task<IActionResult> ExportYearlyRevenueExcel()
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Yearly Revenue Chart");

                worksheet.Cells[1, 1].Value = $"Year";
                worksheet.Cells[1, 2].Value = "Total Revenue";

                var result = await _excelService.GetRevenueByYearAsync();

                int row = 2;
                foreach (var data in result)
                {
                    worksheet.Cells[row, 1].Value = data.Year;
                    worksheet.Cells[row, 2].Value = data.Amount;
                    row++;
                }

                var chart = worksheet.Drawings.AddChart("YearlyRevenueChart", eChartType.ColumnClustered);
                chart.Title.Text = $"Revenue by Year";
                chart.SetPosition(1, 0, 4, 0);
                chart.SetSize(800, 400);

                var series = chart.Series.Add(worksheet.Cells[2, 2, row - 1, 2], worksheet.Cells[2, 1, row - 1, 1]);
                series.Header = "Total Revenue";

                // Lưu dữ liệu vào stream
                package.SaveAs(stream);
            }

            // Trả về file Excel dưới dạng response
            stream.Position = 0;
            string excelName = "YearlyRevenue.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
