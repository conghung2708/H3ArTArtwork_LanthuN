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
                    GetAll(a => a.categoryID == categoryId && (search == null || a.title.Contains(search)));
            }
            else
            {
                artworkList = _unitOfWork.ArtworkObj.GetAll(a => search == null || a.title.Contains(search), includeProperties: "category");
            }

            return View(artworkList);
        }

        public IActionResult Details(int artworkId)
        {

            Artwork artworkFromDb = _unitOfWork.ArtworkObj.Get(u => u.artworkId == artworkId, includeProperties: "applicationUser");

            if (artworkFromDb.reportedConfirm == true || artworkFromDb.isBought == true)
            {
        
                TempData["error"] = "This artwork is bought or reported";

                return RedirectToAction(nameof(Index));
            }
            ShoppingCart shoppingCart = new()
            {
                artwork = artworkFromDb,
                count = 1,
                artworkID = artworkId,
                RelatedArtworks = _unitOfWork.ArtworkObj.GetAll(includeProperties: "category"),
                artistID = artworkFromDb.artistID
            };

            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            // Set count to 1 to ensure it's always equal to 1
            shoppingCart.count = 1;

            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;


            Artwork artwork = _unitOfWork.ArtworkObj.Get(u => u.artworkId == shoppingCart.artworkID);
            shoppingCart.buyerID = userId;
            shoppingCart.artistID = artwork.artistID;
            shoppingCart.isNew = true;

            if (userId.Equals(shoppingCart.artistID))
            {
                TempData["error"] = "You cannot add your own product to the cart.";
                return RedirectToAction(nameof(Index));
            }

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCartObj.Get(u => u.buyerID == userId && u.artworkID == shoppingCart.artworkID);
            if (cartFromDb != null)
            {
                //cartFromDb.Count += shoppingCart.Count;

                // Update count to 1 if it's already in the cart
                cartFromDb.count = 1;
                _unitOfWork.ShoppingCartObj.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                //add cart record
                _unitOfWork.ShoppingCartObj.Add(shoppingCart);
                _unitOfWork.Save();
                //add the cart value to session
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCartObj.GetAll(u => u.buyerID == userId).Count()); //Count the distinct item that user has


            }

            TempData["success"] = "Cart updated successfully";


            return RedirectToAction(nameof(Index));
        }


        public IActionResult ArtistProfile(string artistID)
        {
            UserVM userVM = new()
            {
                artist = _unitOfWork.ApplicationUserObj.Get(u => u.Id == artistID),
                artworkList = _unitOfWork.ArtworkObj.GetAll(u => u.artistID == artistID)
            };
            return View(userVM);
        }

        public IActionResult ViewBlog(string artistID)
        {
            IEnumerable<Blog> blogList = _unitOfWork.BlogObj.GetAll(u => u.creatorID == artistID, includeProperties:"applicationUser");
            return View(blogList);
        }

        [Authorize(Roles = SD.Role_Creator + "," + SD.Role_Customer)]
        public IActionResult ReportArtwork(int artworkID)
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            Artwork artwork = _unitOfWork.ArtworkObj.Get(u => u.artworkId == artworkID);

            if (artwork.artistID.Equals(userId))
            {
                TempData["error"] = "Cannot report your artwork";
                return RedirectToAction(nameof(Details), new { artworkID = artworkID });
            }
            ReportArtwork reportArtwork = new ReportArtwork();
            reportArtwork.artworkID = artworkID;
            reportArtwork.reporterUserID = userId;
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

            Blog blog = _unitOfWork.BlogObj.Get(u => u.blogID == blogID);

            if (blog.creatorID.Equals(userId))
            {
                TempData["error"] = "Cannot report your blog";
                return RedirectToAction(nameof(Blog_Details), new { blogID = blogID });
            }
            ReportBlog reportBlog = new ReportBlog();
            reportBlog.blogID = blogID;
            reportBlog.reporterUserID = userId;
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
            reportArtist.artistID = artistID;

            reportArtist.reporterUserID = userId;
            _unitOfWork.ReportArtistObj.Add(reportArtist);
            _unitOfWork.Save();
            TempData["success"] = "Report artist successfully";


            return RedirectToAction(nameof(ArtistProfile), new { artistID = artistID });
        }

        public IActionResult Blog()
        {
            IEnumerable<Blog> blogList = _unitOfWork.BlogObj.GetAll(includeProperties: "applicationUser");
            return View(blogList);
        }

        public IActionResult Blog_Details(int blogID)
        {
            Blog blog = _unitOfWork.BlogObj.Get(u=>u.blogID == blogID, includeProperties:"applicationUser");
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