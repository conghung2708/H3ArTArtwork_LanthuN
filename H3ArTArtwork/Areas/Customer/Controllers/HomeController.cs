using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using H3ArT.Models.ViewModels;
using H3ArT.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using H3ArT.Models;

namespace H3ArTArtwork.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public PackagePaymentVM PackagePaymentVM { get; set; }
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? categoryId, string search)
        {
            IEnumerable<Artwork> artworkList;

            if (categoryId.HasValue)
            {
                artworkList = _unitOfWork.ArtworkObj.
                    GetAll(a => a.CategoryId == categoryId && (search == null || a.Title.Contains(search)));
            }
            else
            {
                artworkList = _unitOfWork.ArtworkObj.GetAll(a => search == null || a.Title.Contains(search), includeProperties: "Category");
            }

            return View(artworkList);
        }

        [Authorize(Roles = "Customer, Creator, Moderator")]
        public IActionResult Details(int artworkId)
        {

            Artwork artworkFromDb = _unitOfWork.ArtworkObj.Get(u => u.ArtworkId == artworkId, includeProperties: "ApplicationUser");

            if (artworkFromDb.ReportedConfirm == true || artworkFromDb.IsBought == true)
            {
        
                TempData["error"] = "This artwork is bought or reported";

                return RedirectToAction(nameof(Index));
            }
            ShoppingCart shoppingCart = new()
            {
                Artwork = artworkFromDb,
                Count = 1,
                ArtworkId = artworkId,
                RelatedArtworks = _unitOfWork.ArtworkObj.GetAll(includeProperties: "Category"),
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

        [Authorize(Roles = "Customer, Creator, Moderator")]
        public IActionResult ArtistProfile(string artistID)
        {
            UserVM userVM = new()
            {
                Artist = _unitOfWork.ApplicationUserObj.Get(u => u.Id == artistID),
                ArtworkList = _unitOfWork.ArtworkObj.GetAll(u => u.ArtistId == artistID)
            };
            return View(userVM);
        }

        [Authorize(Roles = "Customer, Creator, Moderator")]
        public IActionResult ViewBlog(string artistID)
        {
            IEnumerable<Blog> blogList = _unitOfWork.BlogObj.GetAll(u => u.CreatorId == artistID, includeProperties:"ApplicationUser");
            return View(blogList);
        }

        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult ReportArtwork(int artworkID)
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            Artwork artwork = _unitOfWork.ArtworkObj.Get(u => u.ArtworkId == artworkID);

            if (artwork.ArtistId.Equals(userId))
            {
                TempData["error"] = "Cannot report your artwork";
                return RedirectToAction(nameof(Details), new { artworkID = artworkID });
            }
            ReportArtwork reportArtwork = new ReportArtwork();
            reportArtwork.ArtworkId = artworkID;
            reportArtwork.ReporterUserId = userId;
            _unitOfWork.ReportArtworkObj.Add(reportArtwork);
            _unitOfWork.Save();
            TempData["success"] = "Report artwork successfully";
            return RedirectToAction(nameof(Details), new { artworkID = artworkID });
        }

        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult ReportBlog(int blogID)
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            Blog blog = _unitOfWork.BlogObj.Get(u => u.BlogId == blogID);

            if (blog.CreatorId.Equals(userId))
            {
                TempData["error"] = "Cannot report your blog";
                return RedirectToAction(nameof(Blog_Details), new { blogID = blogID });
            }
            ReportBlog reportBlog = new ReportBlog();
            reportBlog.BlogId = blogID;
            reportBlog.ReporterUserId = userId;
            _unitOfWork.ReportBlogObj.Add(reportBlog);
            _unitOfWork.Save();
            TempData["success"] = "Report blog successfully";
            return RedirectToAction(nameof(Blog_Details), new { blogID = blogID });
        }


        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult ReportArtist(string artistID)
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == artistID)
            {
                TempData["error"] = "Cannot report yourself";
                return RedirectToAction(nameof(ArtistProfile), new { artistID = artistID });
            }
            ReportArtist reportArtist = new ReportArtist();
            reportArtist.ArtistId = artistID;

            reportArtist.ReporterUserId = userId;
            _unitOfWork.ReportArtistObj.Add(reportArtist);
            _unitOfWork.Save();
            TempData["success"] = "Report artist successfully";


            return RedirectToAction(nameof(ArtistProfile), new { artistID = artistID });
        }

        [Authorize(Roles = "Customer, Creator, Moderator")]
        public IActionResult Blog()
        {
            IEnumerable<Blog> blogList = _unitOfWork.BlogObj.GetAll(includeProperties: "ApplicationUser");
            return View(blogList);
        }

        [Authorize(Roles = "Customer, Creator, Moderator")]
        public IActionResult Blog_Details(int blogID)
        {
            Blog blog = _unitOfWork.BlogObj.Get(u=>u.BlogId == blogID, includeProperties:"ApplicationUser");
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
    }
}