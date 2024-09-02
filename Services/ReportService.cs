using Forecast_Master.Interfaces;
using Forecast_Master.Models.Forecast.Models;
using Forecast_Master.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;

namespace Forecast_Master.Services
{
    public class ReportService : IReportService
    {
        private readonly ForecastContext _context;
        private readonly IMemoryCache _memoryCache; // Agregado para el caché

        public ReportService(ForecastContext context, IMemoryCache memoryCache)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public List<string> GetReportTypes()
        {
            return Enum.GetNames(typeof(ReportConfigurationModel.ReportType)).ToList();
        }

        public void SaveConfiguration(ReportConfigurationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var reportConfig = new ReportConfiguration
            {
                Tipo = model.ReportTypes.ToString(),
                FechaInicio = model.StartDate,
                FechaFin = model.EndDate,
                Criterios = model.Criteria
            };

            _context.ReportConfigurations.Add(reportConfig);
            _context.SaveChanges();
        }

        public async Task<ReportViewModel<T>> GenerateReport<T>(ReportConfigurationModel model) where T : class
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var cacheKey = $"Report_{typeof(T).Name}_{model.StartDate}_{model.EndDate}";
            if (!_memoryCache.TryGetValue(cacheKey, out ReportViewModel<T> cachedReport))
            {
                cachedReport = model.ReportTypes switch
                {
                    ReportConfigurationModel.ReportType.Sales => await GenerateSalesReport(model) as ReportViewModel<T>,
                    ReportConfigurationModel.ReportType.Purchases => await GeneratePurchasesReport(model) as ReportViewModel<T>,
                    ReportConfigurationModel.ReportType.Products => await GenerateProductsReport(model) as ReportViewModel<T>,
                    _ => throw new ArgumentException("Tipo de reporte no válido")
                };

                // Guardar el reporte en la caché por 30 minutos
                _memoryCache.Set(cacheKey, cachedReport, TimeSpan.FromMinutes(30));
            }
            
            return cachedReport;
        }

        private async Task<ReportViewModel<SalesReportItem>> GenerateSalesReport(ReportConfigurationModel model)
        {
            var salesData = await _context.Sales
                .AsNoTracking()
                .Where(s => s.Fecha >= model.StartDate && s.Fecha <= model.EndDate)
                .Select(s => new SalesReportItem
                {
                    SaleDate = s.Fecha,
                    ProductId = s.ProductoId,
                    Quantity = s.Cantidad,
                    Price = s.Precio,
                    ClientId = s.ClienteId,
                    VendorId = s.VendedorId
                })
                .ToListAsync();

            return new ReportViewModel<SalesReportItem>
            {
                ReportType = "Ventas",
                ReportData = model.IsSummary ? GetSummaryData(salesData) : salesData,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Title = "Reporte de Ventas"
            };
        }

        private async Task<ReportViewModel<PurchasesReportItem>> GeneratePurchasesReport(ReportConfigurationModel model)
        {
            var purchaseData = await _context.Purchases
                .AsNoTracking()
                .Where(p => p.Fecha >= model.StartDate && p.Fecha <= model.EndDate)
                .Select(p => new PurchasesReportItem
                {
                    PurchaseDate = p.Fecha,
                    ProductId = p.ProductoId,
                    Quantity = p.Cantidad,
                    Price = p.Precio,
                    ProviderId = p.ProveedorId
                })
                .ToListAsync();

            return new ReportViewModel<PurchasesReportItem>
            {
                ReportType = "Compras",
                ReportData = model.IsSummary ? GetSummaryData(purchaseData) : purchaseData,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Title = "Reporte de Compras"
            };
        }

        private async Task<ReportViewModel<ProductsReportItem>> GenerateProductsReport(ReportConfigurationModel model)
        {
            var productData = await _context.Products
                .AsNoTracking()
                .Where(p => p.Stock > 0)
                .Select(p => new ProductsReportItem
                {
                    ProductId = p.Id,
                    ProductName = p.Nombre,
                    Category = p.Categoria,
                    Price = p.Precio,
                    Stock = p.Stock
                })
                .ToListAsync();

            return new ReportViewModel<ProductsReportItem>
            {
                ReportType = "Productos",
                ReportData = model.IsSummary ? GetSummaryData(productData) : productData,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Title = "Reporte de Productos"
            };
        }

        public ReportFile GetReportFile(int reportId, string format)
        {
            var report = _context.Reports
            .AsNoTracking()
            .FirstOrDefault(r => r.Id == reportId);  
            if (report == null) throw new InvalidOperationException("Reporte no encontrado.");

            var reportFile = new ReportFile(
                fileName: $"Reporte_{report.Tipo}_{DateTime.Now:yyyyMMddHHmmss}.{format}",
                mimeType: GetContentType(format),
                content: GenerateReportFileContent(report, format)
            );

            return reportFile;
        }

        private byte[] GenerateReportFileContent(Report report, string format)
        {
            if (format.Equals("pdf", StringComparison.OrdinalIgnoreCase))
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    var document = new Document();
                    var writer = PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // Agregar contenido al PDF
                    document.Add(new Paragraph($"Reporte: {report.Tipo}"));
                    document.Add(new Paragraph($"Fecha de Inicio: {report.FechaInicio}"));
                    document.Add(new Paragraph($"Fecha de Fin: {report.FechaFin}"));

                    document.Close();
                    writer.Close();

                    return stream.ToArray();
                }
            }
            else if (format.Equals("xlsx", StringComparison.OrdinalIgnoreCase))
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Reporte");

                    // Agregar contenido al archivo Excel
                    worksheet.Cells[1, 1].Value = "Reporte";
                    worksheet.Cells[1, 2].Value = report.Tipo;
                    worksheet.Cells[2, 1].Value = "Fecha de Inicio";
                    worksheet.Cells[2, 2].Value = report.FechaInicio;
                    worksheet.Cells[3, 1].Value = "Fecha de Fin";
                    worksheet.Cells[3, 2].Value = report.FechaFin;

                    return package.GetAsByteArray();
                }
            }

            return new byte[0];
        }

        private string GetContentType(string format)
        {
            return format switch
            {
                "pdf" => "application/pdf",
                "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
        }

        private List<T> GetSummaryData<T>(List<T> data) where T : class
        {
            // Optimización mantenida; lógica de resumen sin cambios
            if (typeof(T) == typeof(SalesReportItem))
            {
                var summaryData = data.Cast<SalesReportItem>()
                    .GroupBy(s => s.ProductId)
                    .Select(g => new SalesReportItem
                    {
                        ProductId = g.Key,
                        Quantity = g.Sum(s => s.Quantity),
                        Price = g.Sum(s => s.Price),
                        SaleDate = DateTime.MinValue,
                        ClientId = 0,
                        VendorId = 0
                    }).ToList();

                return summaryData as List<T>;
            }
            else if (typeof(T) == typeof(PurchasesReportItem))
            {
                var summaryData = data.Cast<PurchasesReportItem>()
                    .GroupBy(p => p.ProductId)
                    .Select(g => new PurchasesReportItem
                    {
                        ProductId = g.Key,
                        Quantity = g.Sum(p => p.Quantity),
                        Price = g.Sum(p => p.Price),
                        PurchaseDate = DateTime.MinValue,
                        ProviderId = 0
                    }).ToList();

                return summaryData as List<T>;
            }
            else if (typeof(T) == typeof(ProductsReportItem))
            {
                var summaryData = data.Cast<ProductsReportItem>()
                    .GroupBy(p => p.ProductId)
                    .Select(g => new ProductsReportItem
                    {
                        ProductId = g.Key,
                        ProductName = g.First().ProductName,
                        Category = g.First().Category,
                        Price = g.First().Price,
                        Stock = g.Sum(p => p.Stock)
                    }).ToList();

                return summaryData as List<T>;
            }

            return data;
        }
    }
}
