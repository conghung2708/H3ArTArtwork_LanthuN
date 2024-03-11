using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models;
using H3ArT.Models.Models;
using H3ArT.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;

namespace H3ArTArtwork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PackageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //Select *

            return View();
        }

        public IActionResult Upsert(int? packageID)
        {

            Package package = new Package();
            /*
                        if (packageID == null || packageID == 0)
                        {
                            //create
                            var claimsIdentity = (ClaimsIdentity)User.Identity;
                            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                            //ApplicationUser applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);
                            //if (artworkVM.artwork.applicationUser.AvaiblePost <= 0 || artworkVM.artwork.applicationUser.AvaiblePost == null)
                            //{
                            //    TempData["error"] = "You do not have enough posting credits to place an order. Please purchase a package to continue.";
                            //}


                            artworkVM.artwork.artistID = userId;
                            artworkVM.artwork.applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);
                            artworkVM.artwork.applicationUser.AvaiblePost -= 1;
                            return View(artworkVM);
                        }
                        else
                        {*/
            //update
            package = _unitOfWork.PackageObj.Get(u => u.packageID == packageID);
            return View(package);
            //}

        }

        [HttpPost]
        public IActionResult Upsert(Package package)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                if (ModelState.IsValid)
                {
                    //if (package.packageID == 0)
                    //{

                    //    // Add product
                    //    ApplicationUser applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);
                    //    if (applicationUser.AvaiblePost <= 0 || applicationUser.AvaiblePost == null)
                    //    {
                    //        TempData["error"] = "You do not have enough posting credits to place an order. Please purchase a package to continue.";
                    //        return RedirectToAction("Index", "Artwork");
                    //    }

                    //    // if user has pay for package
                    //    _unitOfWork.ArtworkObj.Add(package.artwork);
                    //    _unitOfWork.Save();
                    //    package.artwork.artistID = userId;
                    //    _unitOfWork.ArtworkObj.Update(package.artwork);
                    //    _unitOfWork.Save();

                    //    applicationUser.AvaiblePost -= 1;
                    //    _unitOfWork.ApplicationUserObj.Update(applicationUser);
                    //    _unitOfWork.Save();


                    //    TempData["success"] = "Artwork created successfully";
                    //}
                    //else
                    //{
                    // Update product
                    _unitOfWork.PackageObj.Update(package);

                    _unitOfWork.Save();

                    TempData["success"] = "Artwork updated successfully";
                    //}
                    return RedirectToAction("Index", "Package");
                }
                else
                {
                    package = _unitOfWork.PackageObj.GetAll().FirstOrDefault();

                    return View(package);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                TempData["error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index", "Package");
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Package> listPackage = _unitOfWork.PackageObj.GetAll().ToList();


            return Json(new { data = listPackage });
        }

        #endregion

    }
}
