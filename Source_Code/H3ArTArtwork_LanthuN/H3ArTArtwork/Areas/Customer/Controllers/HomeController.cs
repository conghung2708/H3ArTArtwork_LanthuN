using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models;
using H3ArT.Models.Models;
using H3ArT.Models.ViewModels;
using H3ArT.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;

namespace H3ArTArtwork.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public PackagePaymentVM PackagePaymentVM { get; set; }
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int? categoryId, string search, string? productType)
        {
            IEnumerable<Artwork> artworkList;
            Category category = new();

            // Filter by category 
            if (categoryId.HasValue)
            {
                artworkList = _unitOfWork.ArtworkObj
                    .GetAll(a => a.CategoryId == categoryId && (search == null || (a.Title.ToLower()).Contains(search.ToLower())), includeProperties: "Category,ApplicationUser");
                category = _unitOfWork.CategoryObj
                    .Get(a => a.CategoryId == categoryId);
            }
            else
            {
                artworkList = _unitOfWork.ArtworkObj
                    .GetAll(a => search == null || (a.Title.ToLower()).Contains(search.ToLower()), includeProperties: "Category,ApplicationUser");
            }

            // Filter by product type
            if (!string.IsNullOrEmpty(productType))
            {
                switch (productType.ToLower())
                {
                    case "free":
                        artworkList = artworkList.Where(a => !a.IsPremium);
                        break;
                    case "premium":
                        artworkList = artworkList.Where(a => a.IsPremium);
                        break;
                    // If productType is not "free" or "premium", show all artworks
                    default:
                        break;
                }
            }
            if (productType != null)
            {
                ViewBag.ProductType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(productType);
            }
            ViewBag.Category = category.CategoryName;
            ViewBag.Search = search;

            return View(artworkList);
        }


        public IActionResult Details(int artworkId)
        {
            IEnumerable<Artwork> artworkList;
            if (User.IsInRole(SD.Role_Admin))
            {
                TempData["error"] = "Admin cannot see the artwork detail";
                return RedirectToAction(nameof(Index));
            }
            Artwork artworkFromDb = _unitOfWork.ArtworkObj.Get(u => u.ArtworkId == artworkId, includeProperties: "ApplicationUser");

            if(artworkFromDb == null)
            {
                TempData["error"] = "The artwork does not exist!";
                return RedirectToAction(nameof(Index));
            }

            if (artworkFromDb.ReportedConfirm == true || artworkFromDb.IsBought == true)
            {
                TempData["error"] = "This artwork is bought or reported";
                return RedirectToAction(nameof(Index));
            }
            artworkList = _unitOfWork.ArtworkObj.GetAll(includeProperties: "ApplicationUser");
            artworkList = _unitOfWork.ArtworkObj.GetAll(includeProperties: "Category");
            ShoppingCart shoppingCart = new()
            {
                Artwork = artworkFromDb,
                Count = 1,
                ArtworkId = artworkId,
                //RelatedArtworks = _unitOfWork.ArtworkObj.GetAll(includeProperties: "Category"),
                RelatedArtworks = _unitOfWork.ArtworkObj.GetAll(a => a.CategoryId == artworkFromDb.CategoryId && !a.IsBought),
                ArtistId = artworkFromDb.ArtistId
            };

            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            // Set count to 1 to ensure it's always equal to 1
            shoppingCart.Count = 1;

            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            Artwork artwork = _unitOfWork.ArtworkObj.Get(u => u.ArtworkId == shoppingCart.ArtworkId);
            shoppingCart.BuyerId = userId;
            shoppingCart.ArtistId = artwork.ArtistId;
            shoppingCart.IsNew = true;

            if (userId.Equals(shoppingCart.ArtistId))
            {
                TempData["error"] = "You cannot add your own product to the cart.";
                return RedirectToAction(nameof(Index));
            }

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCartObj.Get(u => u.BuyerId == userId && u.ArtworkId == shoppingCart.ArtworkId);
            if (cartFromDb != null)
            {
                
                //cartFromDb.Count += shoppingCart.Count;

                // Update count to 1 if it's already in the cart
                cartFromDb.Count = 1;
                _unitOfWork.ShoppingCartObj.Update(cartFromDb);
                _unitOfWork.Save();
                TempData["error"] = "You added this artwork before!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //add cart record
                _unitOfWork.ShoppingCartObj.Add(shoppingCart);
                _unitOfWork.Save();
                //add the cart value to session
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCartObj.GetAll(u => u.BuyerId == userId).Count()); //Count the distinct item that user has
            }
            TempData["success"] = "Cart updated successfully";
            return RedirectToAction(nameof(Index));
        }

    
        public IActionResult ArtistProfile(string artistID)
        {
            if (User.IsInRole(SD.Role_Admin))
            {
                TempData["error"] = "Admin cannot see the creator profile";
                return RedirectToAction(nameof(Index));
            }
            UserVM userVM = new()
            {
                User = _unitOfWork.ApplicationUserObj.Get(u => u.Id == artistID),
                ArtworkList = _unitOfWork.ArtworkObj.GetAll(u => u.ArtistId == artistID, includeProperties: "ApplicationUser")
            };
            if(userVM.User == null)
            {
                TempData["error"] = "The creator does not exist!";
                return RedirectToAction(nameof(Index));
            }
            return View(userVM);
        }

      
     
        public IActionResult ViewBlog(string artistID)
        {
            if (User.IsInRole(SD.Role_Admin))
            {
                TempData["error"] = "Admin cannot view blogs";
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<Blog> blogList = _unitOfWork.BlogObj.GetAll(u => u.CreatorId == artistID, includeProperties:"ApplicationUser");

            return View(blogList);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult ReportArtwork(int artworkID)
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var reason = Request.Form["reportReason"];

            Artwork artwork = _unitOfWork.ArtworkObj.Get(u => u.ArtworkId == artworkID);

            if (artwork.ArtistId.Equals(userId))
            {
                TempData["error"] = "Cannot report your artwork";
                return RedirectToAction(nameof(Details), new { artworkID = artworkID });
            }
            ReportArtwork reportArtwork = new ReportArtwork();
            reportArtwork.ArtworkId = artworkID;
            reportArtwork.ReporterUserId = userId;
            reportArtwork.Reason = reason;
            _unitOfWork.ReportArtworkObj.Add(reportArtwork);
            _unitOfWork.Save();
            TempData["success"] = "Report artwork successfully";
            return RedirectToAction(nameof(Details), new { artworkID = artworkID });
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult ReportBlog(int blogID)
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var reason = Request.Form["reportReason"];
            Blog blog = _unitOfWork.BlogObj.Get(u => u.BlogId == blogID);

            if (blog.CreatorId.Equals(userId))
            {
                TempData["error"] = "Cannot report your blog";
                return RedirectToAction(nameof(Blog_Details), new { blogID = blogID });
            }
            ReportBlog reportBlog = new ReportBlog();
            reportBlog.BlogId = blogID;
            reportBlog.Reason = reason;
            reportBlog.ReporterUserId = userId;
            _unitOfWork.ReportBlogObj.Add(reportBlog);
            _unitOfWork.Save();
            TempData["success"] = "Report blog successfully";
            return RedirectToAction(nameof(Blog_Details), new { blogID = blogID });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult ReportArtist(string artistID)
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var reason = Request.Form["reportReason"];
            if (userId == artistID)
            {
                TempData["error"] = "Cannot report yourself";
                return RedirectToAction(nameof(ArtistProfile), new { artistID = artistID });
            }
            ReportArtist reportArtist = new ReportArtist();
            reportArtist.ArtistId = artistID;
            reportArtist.Reason = reason;
            reportArtist.ReporterUserId = userId;
            _unitOfWork.ReportArtistObj.Add(reportArtist);
            _unitOfWork.Save();
            TempData["success"] = "Report artist successfully";

            return RedirectToAction(nameof(ArtistProfile), new { artistID = artistID });
        }

       
 
        public IActionResult Blog()
        {
            if (User.IsInRole(SD.Role_Admin))
            {
                TempData["error"] = "Admin cannot view blogs";
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<Blog> blogList = _unitOfWork.BlogObj.GetAll(includeProperties: "ApplicationUser");
            return View(blogList);
        }

       

        public IActionResult Blog_Details(int blogID)
        {
            if (User.IsInRole(SD.Role_Admin))
            {
                TempData["error"] = "Admin cannot view the blog detail";
                return RedirectToAction(nameof(Index));
            }
            Blog blog = _unitOfWork.BlogObj.Get(u=>u.BlogId == blogID, includeProperties:"ApplicationUser");
            if(blog == null)
            {
                TempData["error"] = "The blog does not exist";
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult MyCollection(string search)
        {
            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Moderator))
            {
                TempData["error"] = "You cannot view blogs";
                return RedirectToAction(nameof(Index));
            }
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            UserVM userVM = new();
            if (!string.IsNullOrWhiteSpace(search))
            {
                userVM = new()
                {
                    User = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId),
                    ArtworkList = _unitOfWork.ArtworkObj.GetAll(u => u.buyerId == userId && u.Title.Contains(search))
                };
            }
            else
            {
                userVM = new()
                {
                    User = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId),
                    ArtworkList = _unitOfWork.ArtworkObj.GetAll(u => u.buyerId == userId)
                };
            }
            
            return View(userVM);
        }

        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public async Task<IActionResult> DownloadArtworkImage(int artworkId)
        {
            var artwork =  _unitOfWork.ArtworkObj.Get(u => u.ArtworkId == artworkId);

            if (artwork == null || string.IsNullOrEmpty(artwork.ImageUrl))
            {
                return NotFound(); // Return 404 Not Found if the artwork or image URL is not found
            }

            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, artwork.ImageUrl.TrimStart('\\'));

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound(); // Return 404 Not Found if the image file is not found
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(imagePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            // Set the Content-Disposition header to prompt the user to save the file as a JPG
            return File(memory, "image/jpeg", Path.GetFileName(imagePath), enableRangeProcessing: true);
        }
    }
}