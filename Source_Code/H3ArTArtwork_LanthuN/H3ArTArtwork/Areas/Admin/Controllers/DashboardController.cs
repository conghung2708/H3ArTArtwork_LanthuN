using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models;
using H3ArT.Models.Models;
using H3ArT.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace H3ArTArtwork.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public DashboardVM DashboardVM { get; set; }
        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> users = _unitOfWork.ApplicationUserObj.GetAll();
            IEnumerable<Artwork> artworks = _unitOfWork.ArtworkObj.GetAll(u => u.IsBought == false && u.ReportedConfirm == falseb);
            IEnumerable<OrderHeader> orderHeaders = _unitOfWork.OrderHeaderObj.GetAll();
            double totalPrice = 0;
            foreach (var order in orderHeaders)
            {
                totalPrice += order.OrderTotal;
            }

            DashboardVM = new()
            {
                NumberOfUsers = users.Count(),
                NumberOfArtworks = artworks.Count(),
                NumberOfOrders = orderHeaders.Count(),
                NumberOfSales = totalPrice,
            };
            return View(DashboardVM);
        }
    }
}
