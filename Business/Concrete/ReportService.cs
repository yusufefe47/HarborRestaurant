using OfficeOpenXml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Entities.Enums;
using HarborRestaurant.Business.Abstract;
using System.Text;

namespace HarborRestaurant.Business.Concrete
{
    public interface IReportService
    {
        Task<byte[]> GenerateReservationReportPdfAsync(DateTime startDate, DateTime endDate);
        Task<byte[]> GenerateReservationReportExcelAsync(DateTime startDate, DateTime endDate);
        Task<byte[]> GenerateMenuReportPdfAsync();
        Task<byte[]> GenerateMenuReportExcelAsync();
    }

    public class ReportService : IReportService
    {
        private readonly IReservationService _reservationService;
        private readonly IMenuService _menuService;

        public ReportService(IReservationService reservationService, IMenuService menuService)
        {
            _reservationService = reservationService;
            _menuService = menuService;
            
            // EPPlus lisans ayarını sadece burada ayarla
            try
            {
#pragma warning disable CS0618
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
#pragma warning restore CS0618
            }
            catch
            {
                // Lisans ayarı başarısız olursa sessizce devam et
            }
        }

        public async Task<byte[]> GenerateReservationReportPdfAsync(DateTime startDate, DateTime endDate)
        {
            var reservations = await _reservationService.GetReservationsByDateRangeAsync(startDate, endDate);
            var reservationList = reservations.ToList();

            using var stream = new MemoryStream();
            var document = new Document();
            PdfWriter.GetInstance(document, stream);
            document.Open();

            // Header
            var headerFont = FontFactory.GetFont("Helvetica-Bold", 18f);
            var header = new Paragraph("Harbor Restaurant - Rezervasyon Raporu", headerFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };
            document.Add(header);

            // Date range
            var dateRangeText = $"Tarih Aralığı: {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}";
            var dateRange = new Paragraph(dateRangeText)
            {
                SpacingAfter = 20
            };
            document.Add(dateRange);

            // Summary
            var totalCount = reservationList.Count;
            var confirmedCount = 0;
            var pendingCount = 0;
            var cancelledCount = 0;

            foreach (var res in reservationList)
            {
                if (res.Status == ReservationStatus.Confirmed) confirmedCount++;
                else if (res.Status == ReservationStatus.Pending) pendingCount++;
                else if (res.Status == ReservationStatus.Cancelled) cancelledCount++;
            }

            document.Add(new Paragraph($"Toplam Rezervasyon: {totalCount}"));
            document.Add(new Paragraph($"Onaylanmış: {confirmedCount}"));
            document.Add(new Paragraph($"Beklemede: {pendingCount}"));
            document.Add(new Paragraph($"İptal Edilmiş: {cancelledCount}") { SpacingAfter = 20 });

            // Table
            var table = new PdfPTable(7) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 2, 2, 1.5f, 1, 1.5f, 1, 1.5f });

            // Table headers
            var headerCellFont = FontFactory.GetFont("Helvetica-Bold", 10f);
            var grayColor = new BaseColor(200, 200, 200);

            table.AddCell(new PdfPCell(new Phrase("Ad Soyad", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("E-posta", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("Telefon", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("Kişi", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("Tarih", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("Saat", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("Durum", headerCellFont)) { BackgroundColor = grayColor });

            // Table data
            var cellFont = FontFactory.GetFont("Helvetica", 9f);
            foreach (var reservation in reservationList)
            {
                table.AddCell(new PdfPCell(new Phrase(reservation.FullName, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(reservation.Email, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(reservation.Phone, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(reservation.GuestCount.ToString(), cellFont)));
                table.AddCell(new PdfPCell(new Phrase(reservation.CheckInDate.ToString("dd.MM.yyyy"), cellFont)));
                table.AddCell(new PdfPCell(new Phrase(reservation.ReservationTime.ToString(@"HH\:mm"), cellFont)));
                table.AddCell(new PdfPCell(new Phrase(GetStatusText(reservation.Status), cellFont)));
            }

            document.Add(table);
            document.Close();

            return stream.ToArray();
        }

        public async Task<byte[]> GenerateReservationReportExcelAsync(DateTime startDate, DateTime endDate)
        {
            var reservations = await _reservationService.GetReservationsByDateRangeAsync(startDate, endDate);
            var reservationList = reservations.ToList();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Rezervasyon Raporu");

            // Headers
            worksheet.Cells[1, 1].Value = "Ad Soyad";
            worksheet.Cells[1, 2].Value = "E-posta";
            worksheet.Cells[1, 3].Value = "Telefon";
            worksheet.Cells[1, 4].Value = "Kişi Sayısı";
            worksheet.Cells[1, 5].Value = "Tarih";
            worksheet.Cells[1, 6].Value = "Saat";
            worksheet.Cells[1, 7].Value = "Durum";

            var range = worksheet.Cells[1, 1, 1, 7];
            range.Style.Font.Bold = true;
            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Data
            for (int i = 0; i < reservationList.Count; i++)
            {
                var reservation = reservationList[i];
                var row = i + 2;

                worksheet.Cells[row, 1].Value = reservation.FullName;
                worksheet.Cells[row, 2].Value = reservation.Email;
                worksheet.Cells[row, 3].Value = reservation.Phone;
                worksheet.Cells[row, 4].Value = reservation.GuestCount;
                worksheet.Cells[row, 5].Value = reservation.CheckInDate.ToString("dd.MM.yyyy");
                worksheet.Cells[row, 6].Value = reservation.ReservationTime.ToString(@"HH\:mm");
                worksheet.Cells[row, 7].Value = GetStatusText(reservation.Status);
            }

            worksheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }

        public async Task<byte[]> GenerateMenuReportPdfAsync()
        {
            var menuItems = await _menuService.GetAllMenuItemsAsync();
            var menuList = menuItems.ToList();

            using var stream = new MemoryStream();
            var document = new Document();
            PdfWriter.GetInstance(document, stream);
            document.Open();

            // Header
            var headerFont = FontFactory.GetFont("Helvetica-Bold", 18f);
            var header = new Paragraph("Harbor Restaurant - Menü Raporu", headerFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };
            document.Add(header);

            // Date
            var currentDate = new Paragraph($"Rapor Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm}")
            {
                SpacingAfter = 20
            };
            document.Add(currentDate);

            // Table
            var table = new PdfPTable(5) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 2, 3, 1.5f, 1, 1 });

            // Table headers
            var headerCellFont = FontFactory.GetFont("Helvetica-Bold", 10f);
            var grayColor = new BaseColor(200, 200, 200);

            table.AddCell(new PdfPCell(new Phrase("Kategori", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("Ürün Adı", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("Fiyat", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("Aktif", headerCellFont)) { BackgroundColor = grayColor });
            table.AddCell(new PdfPCell(new Phrase("Mevcut", headerCellFont)) { BackgroundColor = grayColor });

            // Table data
            var cellFont = FontFactory.GetFont("Helvetica", 9f);
            foreach (var item in menuList)
            {
                table.AddCell(new PdfPCell(new Phrase(item.Category?.Name ?? "", cellFont)));
                table.AddCell(new PdfPCell(new Phrase(item.Name, cellFont)));
                table.AddCell(new PdfPCell(new Phrase($"{item.Price:C}", cellFont)));
                table.AddCell(new PdfPCell(new Phrase(item.IsActive ? "Evet" : "Hayır", cellFont)));
                table.AddCell(new PdfPCell(new Phrase(item.IsAvailable ? "Evet" : "Hayır", cellFont)));
            }

            document.Add(table);
            document.Close();

            return stream.ToArray();
        }

        public async Task<byte[]> GenerateMenuReportExcelAsync()
        {
            var menuItems = await _menuService.GetAllMenuItemsAsync();
            var menuList = menuItems.ToList();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Menü Raporu");

            // Headers
            worksheet.Cells[1, 1].Value = "Kategori";
            worksheet.Cells[1, 2].Value = "Ürün Adı";
            worksheet.Cells[1, 3].Value = "Fiyat";
            worksheet.Cells[1, 4].Value = "Aktif";
            worksheet.Cells[1, 5].Value = "Mevcut";

            var range = worksheet.Cells[1, 1, 1, 5];
            range.Style.Font.Bold = true;
            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Data
            for (int i = 0; i < menuList.Count; i++)
            {
                var item = menuList[i];
                var row = i + 2;

                worksheet.Cells[row, 1].Value = item.Category?.Name ?? "";
                worksheet.Cells[row, 2].Value = item.Name;
                worksheet.Cells[row, 3].Value = item.Price;
                worksheet.Cells[row, 4].Value = item.IsActive ? "Evet" : "Hayır";
                worksheet.Cells[row, 5].Value = item.IsAvailable ? "Evet" : "Hayır";
            }

            worksheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }

        private string GetStatusText(ReservationStatus status)
        {
            return status switch
            {
                ReservationStatus.Pending => "Beklemede",
                ReservationStatus.Confirmed => "Onaylandı",
                ReservationStatus.Cancelled => "İptal Edildi",
                ReservationStatus.Completed => "Tamamlandı",
                _ => "Bilinmiyor"
            };
        }
    }
}
