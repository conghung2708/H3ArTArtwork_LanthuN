using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models.Models;
using H3ArT.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace H3ArTArtwork.Areas.Moderator.Controllers
{
    [Area(SD.Role_Moderator)]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index_Artwork()
        {

            return View();
        }

        public IActionResult Index_Creator()
        {
            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAllReportArtwork()
        {
            List<ReportArtwork> reportArtworks = _unitOfWork.ReportArtworkObj.GetAll(
                includeProperties: "artwork,applicationUser"
            ).ToList();
            return Json(new { data = reportArtworks });
        }

        [HttpDelete]
        public IActionResult Delete_Artwork(int? id)
        {
            var productToBeDeleted = _unitOfWork.ArtworkObj.Get(u => u.artworkId == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error during deleting" });
            }

            if (!string.IsNullOrEmpty(productToBeDeleted.imageUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.imageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.ArtworkObj.Remove(productToBeDeleted);
            _unitOfWork.Save();

            List<Artwork> listProduct = _unitOfWork.ArtworkObj.GetAll(includeProperties: "category,applicationUser").ToList();
            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpGet]
        public IActionResult GetAllReportArtist()
        {
            List<ReportArtist> reportArtist = _unitOfWork.ReportArtistObj.GetAll(includeProperties: "artist,reporter").ToList();
            return Json(new { data = reportArtist });
        }
        #endregion

    }


}