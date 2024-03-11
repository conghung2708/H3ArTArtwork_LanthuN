using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using H3ArT.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using H3ArT.Utility;
using H3ArT.Models;

namespace H3ArTArtwork.Areas.Creator.Controllers
{
    [Area("Creator")]
    // [Authorize(Roles = SD.Role_Creator)]
    public class ArtworkController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ArtworkController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Admin)]
        public IActionResult Index()
        {
            //get the id

            return View();
        }
        [Authorize(Roles = SD.Role_Creator)]
        public IActionResult Upsert(int? id)
        {

            ArtworkVM artworkVM = new()
            {
                categoryList = _unitOfWork.CategoryObj.GetAll().Select(u => new SelectListItem
                {
                    Text = u.categoryName,
                    Value = u.categoryId.ToString(),
                }),
                artwork = new Artwork()
            };

            if (id == null || id == 0)
            {
                //create
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                artworkVM.artwork.artistID = userId;
                artworkVM.artwork.applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);
                artworkVM.artwork.applicationUser.AvaiblePost -= 1;
                return View(artworkVM);
            }
            else
            {
                //update
                artworkVM.artwork = _unitOfWork.ArtworkObj.Get(u => u.artworkId == id, includeProperties: "category,applicationUser");
                return View(artworkVM);
            }

        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Creator)]
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

                        if (!string.IsNullOrEmpty(artworkVM.artwork.imageUrl))
                        {
                            // Delete the old image
                            var oldImagePath = Path.Combine(wwwRootPath, artworkVM.artwork.imageUrl.TrimStart('\\'));

                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        artworkVM.artwork.imageUrl = @"\image\artwork\" + fileName;
                    }
                    if (artworkVM.artwork.artworkId == 0)
                    {

                        // Add product
                        ApplicationUser applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);
                        if (applicationUser.AvaiblePost <= 0 || applicationUser.AvaiblePost == null)
                        {
                            TempData["error"] = "You do not have enough posting credits to place an order. Please purchase a package to continue.";
                            return RedirectToAction("Index", "Artwork");
                        }

                        // if user had pay for package
                        artworkVM.artwork.reportedConfirm = false;
                        _unitOfWork.ArtworkObj.Add(artworkVM.artwork);
                        _unitOfWork.Save();
                        applicationUser.AvaiblePost -= 1;
                        _unitOfWork.ApplicationUserObj.Update(applicationUser);
                        _unitOfWork.Save();

                        artworkVM.artwork.artistID = userId;
                        var unknownUser = artworkVM.artwork.applicationUser;
                        _unitOfWork.ArtworkObj.Update(artworkVM.artwork);


                        _unitOfWork.ApplicationUserObj.Remove(unknownUser);

                        _unitOfWork.Save();


                        TempData["success"] = "Artwork created successfully";
                    }
                    else
                    {
                        // Update product
                        _unitOfWork.ArtworkObj.Update(artworkVM.artwork);

                        _unitOfWork.Save();

                        TempData["success"] = "Artwork updated successfully";
                    }
                    return RedirectToAction("Index", "Artwork");
                }
                else
                {
                    artworkVM.categoryList = _unitOfWork.CategoryObj.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.categoryName,
                        Value = u.categoryId.ToString(),
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
            var user = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);
            List<Artwork> artworkList;
            if (User.IsInRole(SD.Role_Admin))
            {
                artworkList = _unitOfWork.ArtworkObj.GetAll(includeProperties: "category,applicationUser").ToList();
                return Json(new { data = artworkList });
            }
            artworkList = _unitOfWork.ArtworkObj.GetAll(u => u.artistID == userId, includeProperties: "category,applicationUser").ToList();
            return Json(new { data = artworkList });
        }
        public IActionResult Delete(int? id)
        {
            try
            {
                var productToBeDeleted = _unitOfWork.ArtworkObj.Get(u => u.artworkId == id);
                if (productToBeDeleted == null)
                {
                    return Json(new { success = false, message = "Error: Artwork not found" });
                }

                // Check if there are any order details associated with this artwork
                bool hasOrderDetails = _unitOfWork.OrderDetailObj.GetAll(od => od.artworkId == id).Any();
                if (hasOrderDetails)
                {
                    // Set error message in TempData
                    TempData["error"] = "Cannot delete artwork with associated orders";
                    return Json(new { success = false, message = "Cannot delete artwork with associated orders" });
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

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                List<Artwork> listArtwork = _unitOfWork.ArtworkObj.GetAll(u => u.artistID == userId, includeProperties: "category,applicationUser").ToList();

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