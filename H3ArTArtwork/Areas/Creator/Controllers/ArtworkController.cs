using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using H3ArT.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using H3ArT.Utility;


namespace H3ArTArtwork.Areas.Creator.Controllers
{
    [Area("Creator")]
    [Authorize(Roles = SD.Role_Creator)]
    public class ArtworkController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ArtworkController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //get the id

            return View();
        }

        public IActionResult Upsert(int? id)
        {

            ArtworkVM artworkVM = new()
            {
                CategoryList = _unitOfWork.CategoryObj.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString(),
                }),
                Artwork = new Artwork()
            };

            if (id == null || id == 0)
            {
                //create
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                artworkVM.Artwork.ArtistId = userId;
                artworkVM.Artwork.ApplicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);

                return View(artworkVM);
            }
            else
            {
                //update
                artworkVM.Artwork = _unitOfWork.ArtworkObj.Get(u => u.ArtworkId == id, includeProperties: "Category,ApplicationUser");
                return View(artworkVM);
            }

        }

        [HttpPost]
        public IActionResult Upsert(ArtworkVM artworkVM, IFormFile? file)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = Path.Combine(wwwRootPath, @"image\artwork");

                        if (!string.IsNullOrEmpty(artworkVM.Artwork.ImageUrl))
                        {
                            // Delete the old image
                            var oldImagePath = Path.Combine(wwwRootPath, artworkVM.Artwork.ImageUrl.TrimStart('\\'));

                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        artworkVM.Artwork.ImageUrl = @"\image\artwork\" + fileName;
                    }
                    if (artworkVM.Artwork.ArtworkId == 0)
                    {

                        // Add product
                        artworkVM.Artwork.CreateAt = DateTime.Now;
                        artworkVM.Artwork.ReportedConfirm = false;
                        _unitOfWork.ArtworkObj.Add(artworkVM.Artwork);
                        _unitOfWork.Save();
                 

                        artworkVM.Artwork.ArtistId = userId;
                        var unknownUser = artworkVM.Artwork.ApplicationUser;
                        _unitOfWork.ArtworkObj.Update(artworkVM.Artwork);
                     
                    
                        _unitOfWork.ApplicationUserObj.Remove(unknownUser);

                        _unitOfWork.Save();


                        TempData["success"] = "Artwork created successfully";
                    }
                    else
                    {
                        // Update product
                        _unitOfWork.ArtworkObj.Update(artworkVM.Artwork);

                        _unitOfWork.Save();

                        TempData["success"] = "Artwork updated successfully";
                    }
                    return RedirectToAction("Index", "Artwork");
                }
                else
                {
                    artworkVM.CategoryList = _unitOfWork.CategoryObj.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.CategoryName,
                        Value = u.CategoryId.ToString(),
                    });

                    return View(artworkVM);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                TempData["error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index", "Artwork");
            }
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<Artwork> artworkList = _unitOfWork.ArtworkObj.GetAll(u => u.ArtistId == userId, includeProperties: "Category,ApplicationUser").ToList();
            return Json(new { data = artworkList });
        }
        public IActionResult Delete(int? id)
        {
            try
            {
                var productToBeDeleted = _unitOfWork.ArtworkObj.Get(u => u.ArtworkId == id);
                if (productToBeDeleted == null)
                {
                    return Json(new { success = false, message = "Error: Artwork not found" });
                }

                // Check if there are any order details associated with this artwork
                bool hasOrderDetails = _unitOfWork.OrderDetailObj.GetAll(od => od.ArtworkId == id).Any();
                if (hasOrderDetails)
                {
                    // Set error message in TempData
                    TempData["error"] = "Cannot delete artwork with associated orders";
                    return Json(new { success = false, message = "Cannot delete artwork with associated orders" });
                }

                if (!string.IsNullOrEmpty(productToBeDeleted.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _unitOfWork.ArtworkObj.Remove(productToBeDeleted);
                _unitOfWork.Save();

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                List<Artwork> listArtwork = _unitOfWork.ArtworkObj.GetAll(u => u.ArtistId == userId, includeProperties: "Category,ApplicationUser").ToList();

                // Set success message in TempData
                TempData["success"] = "Delete Successful";

                return Json(new { success = true, message = "Delete Successful" });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "An error occurred while deleting the artwork" });
            }
        }

        #endregion

    }
}