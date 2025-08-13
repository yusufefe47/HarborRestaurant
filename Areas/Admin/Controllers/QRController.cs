using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class QRController : AdminControllerBase
    {
        [HttpGet]
        public IActionResult GenerateMenuQR()
        {
            try
            {
                string menuUrl = $"{Request.Scheme}://{Request.Host}/Menu";
                
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(menuUrl, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeBytes = qrCode.GetGraphic(20);
                
                return File(qrCodeBytes, "image/png", "menu-qr-code.png");
            }
            catch (Exception ex)
            {
                SetErrorMessage("QR kod oluşturulurken hata oluştu: " + ex.Message);
                return RedirectToAction("Index", "Dashboard");
            }
        }
        
        [HttpGet]
        public IActionResult GenerateReservationQR()
        {
            try
            {
                string reservationUrl = $"{Request.Scheme}://{Request.Host}/Reservation";
                
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(reservationUrl, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeBytes = qrCode.GetGraphic(20);
                
                return File(qrCodeBytes, "image/png", "reservation-qr-code.png");
            }
            catch (Exception ex)
            {
                SetErrorMessage("QR kod oluşturulurken hata oluştu: " + ex.Message);
                return RedirectToAction("Index", "Dashboard");
            }
        }

        [HttpGet]
        public IActionResult QRCodes()
        {
            ViewBag.BaseUrl = $"{Request.Scheme}://{Request.Host}";
            return View();
        }
    }
}
