using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using H3ArT.Models.ViewModels;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Authorization;
using H3ArT.Utility;


namespace H3ArTArtwork.Areas.Creator.Controllers
{
    [Area("Creator")]
    [Authorize(Roles = SD.Role_Creator)]
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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

            //ArtworkVM artworkVM = new()
            //{
            //    categoryList = _unitOfWork.CategoryObj.GetAll().Select(u => new SelectListItem
            //    {
            //        Text = u.categoryName,
            //        Value = u.categoryId.ToString(),
            //    }),
            //    artwork = new Artwork()
            //};

            if (id == null || id == 0)
            {
                //create
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                Blog blog = new Blog();
                blog.creatorID = userId;
                blog.applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);

                return View(blog);
            }
            else
            {
                //update
                Blog blog = _unitOfWork.BlogObj.Get(u => u.blogID == id, includeProperties: "applicationUser");
                return View(blog);
            }

        }

        [HttpPost]
        public IActionResult Upsert(Blog blog, IFormFile? file)
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
                        string productPath = Path.Combine(wwwRootPath, @"image\blog");

                        if (!string.IsNullOrEmpty(blog.imageUrl))
                        {
                            // Delete the old image
                            var oldImagePath = Path.Combine(wwwRootPath, blog.imageUrl.TrimStart('\\'));

                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        blog.imageUrl = @"\image\blog\" + fileName;
                    }
                    if (blog.blogID == 0)
                    {
                        // Assign current time to createdAt property
                        blog.createAt = DateTime.Now;

                        // Add product
                        _unitOfWork.BlogObj.Add(blog);
                        _unitOfWork.Save();
                 

                        blog.creatorID = userId;
                        var unknownUser = blog.applicationUser;
                        _unitOfWork.BlogObj.Update(blog);
                     
                    
                        _unitOfWork.ApplicationUserObj.Remove(unknownUser);

                        _unitOfWork.Save();


                        TempData["success"] = "Blog created successfully";
                    }
                    else
                    {
                        // Update product
                        _unitOfWork.BlogObj.Update(blog);

                        _unitOfWork.Save();

                        TempData["success"] = "Artwork updated successfully";
                    }
                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                   
                    return View(blog);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                TempData["error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index", "Blog");
            }
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<Blog> blogList = _unitOfWork.BlogObj.GetAll(u => u.creatorID ==  userId, includeProperties: "applicationUser").ToList();
            return Json(new { data = blogList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var blogToBeDeleted = _unitOfWork.BlogObj.Get(u => u.blogID == id);
            if (blogToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error during deleting" });
            }

            if (!string.IsNullOrEmpty(blogToBeDeleted.imageUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, blogToBeDeleted.imageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.BlogObj.Remove(blogToBeDeleted);
            _unitOfWork.Save();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<Blog> blogList = _unitOfWork.BlogObj.GetAll(u => u.creatorID == userId, includeProperties: "applicationUser").ToList();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}