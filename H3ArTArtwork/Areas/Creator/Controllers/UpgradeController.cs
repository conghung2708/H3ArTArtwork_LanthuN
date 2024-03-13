using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models.Models;
using H3ArT.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using H3ArT.Models;
using H3ArT.Utility;
using Stripe.Checkout;
using H3ArT.DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;

namespace H3ArTArtwork.Areas.Creator.Controllers
{
    [Area("Creator")]
    [Authorize(Roles = "Creator")]
    public class UpgradeController : Controller
    {
        [BindProperty]
        public PackagePaymentVM PackagePaymentVM { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public UpgradeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // trang nay hien ra 3 cai de chon --> chuyen qua Summary
        public IActionResult Index()
        {
            IEnumerable<Package> packageList = _unitOfWork.PackageObj.GetAll();
            return View(packageList);
        }

        // trang view cua Summary
        public IActionResult SummaryPackage(int packageId)
        {
            // nhan data tu trang Index de nap vao PackagePaymentVM
            // ...
            // xong chuyen qua ben Thanh toan

            // lay tu database ra noi dung cua cac goi xong chuyen qua View
            // ...
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);


            PackagePaymentVM = new()
            {
                PackageId = packageId,
                // Package = _unitOfWork.PackageObj.Get(u => u.packageID == packageId),
                // , includeProperties: "artwork"
                OrderHeader = new()
            };
            //foreach (var cart in PackagePaymentVM.Package)
            //{
            //cart.price = cart.artwork.price;
            Package package = _unitOfWork.PackageObj.Get(u => u.PackageId == packageId);
            PackagePaymentVM.OrderHeader.OrderTotal = package.Price;
            PackagePaymentVM.Package = _unitOfWork.PackageObj.Get(u => u.PackageId == packageId);
            PackagePaymentVM.ApplicationUser = applicationUser;
            //}
            return View(PackagePaymentVM);

            // View();
        }

        // nay la khi nhan cai nut thanh toan trong trang Summary --> POST
        [HttpPost]
        [ActionName("SummaryPackage")]
        public IActionResult SummaryPackagePOST()
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);
            if (applicationUser.AvaiblePost <= 0 || applicationUser.AvaiblePost == null)
            {
                //ShoppingCartVM will automatically be populated
                var packageId = PackagePaymentVM.PackageId;
                PackagePaymentVM.Package = _unitOfWork.PackageObj.Get(u => u.PackageId == packageId);
                Package package = PackagePaymentVM.Package;
                //Package package = _unitOfWork.PackageObj.Get(u => u.packageID == packageId);

                PackagePaymentVM.OrderHeader.OrderDate = System.DateTime.Now;
                PackagePaymentVM.OrderHeader.ApplicationUserId = userId;
                PackagePaymentVM.OrderHeader.OrderTotal = package.Price;
                PackagePaymentVM.OrderHeader.IsPackageOrder = true;


                if (!ModelState.IsValid)
                {
                    // If model state is not valid, return the view with validation errors
                    return View(PackagePaymentVM); // or any other suitable action result
                }
                var existingOrder = _unitOfWork.OrderHeaderObj.Get(o => o.ApplicationUserId == userId && o.PaymentStatus == SD.PaymentStatusPending);
                /*
                //if (existingOrder != null)
                //{
                //    PackagePaymentVM.orderHeader = existingOrder;
                //    _unitOfWork.OrderHeaderObj.Remove(existingOrder);
                //    _unitOfWork.Save();
                //    // var newShoppingCartItems = PackagePaymentVM.ShoppingCartList.Where(cart => cart.isNew);

                //    //foreach (var cart in newShoppingCartItems)
                //    //{
                //    OrderDetailPackage orderDetail = new OrderDetailPackage
                //    {
                //        //artworkId = cart.artworkID,
                //        //orderHeaderId = existingOrder.Id,
                //        //price = cart.price,
                //        //count = cart.count


                //        orderHeaderId = existingOrder.Id,
                //        packageId = package.packageID,
                //        price = package.price
                //    };
                //}
                //    _unitOfWork.OrderDetailObj.Add(orderDetail);
                //    cart.isNew = false; // Reset isNew flag
                //    _unitOfWork.ShoppingCartObj.Update(cart);
                //}
                //}
                //else
                //{
                //it is a regular customer account
                PackagePaymentVM.orderHeader.paymentStatus = SD.PaymentStatusPending;
                PackagePaymentVM.orderHeader.orderStatus = SD.StatusPending;

                _unitOfWork.OrderHeaderObj.Add(PackagePaymentVM.orderHeader);
                _unitOfWork.Save();
                //foreach (var cart in ShoppingCartVM.ShoppingCartList)
                //{
                */
                PackagePaymentVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                PackagePaymentVM.OrderHeader.OrderStatus = SD.StatusPending;

                _unitOfWork.OrderHeaderObj.Add(PackagePaymentVM.OrderHeader);
                _unitOfWork.Save();
                OrderDetailPackage orderDetailPackage = new OrderDetailPackage
                {
                    //artworkId = cart.artworkID,
                    //orderHeaderId = existingOrder.Id,
                    //price = cart.price,
                    //count = cart.count
                    orderHeaderId = PackagePaymentVM.OrderHeader.Id,
                    packageId = package.PackageId,
                    price = package.Price
                };
                _unitOfWork.OrderDetailPackageObj.Add(orderDetailPackage);
                _unitOfWork.Save();
                // cart.isNew = false; // Reset isNew flag
                // _unitOfWork.ShoppingCartObj.Update(cart);
                //}

                //}
                //stripe logic
                var domain = "https://localhost:7034/";

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + $"creator/upgrade/PackageOrderConfirmation?id={PackagePaymentVM.OrderHeader.Id}&packageID={PackagePaymentVM.PackageId}",

                    CancelUrl = domain + "customer/cart/index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };
                //foreach (var item in ShoppingCartVM.ShoppingCartList)
                //{
                var packageItem = PackagePaymentVM.Package;
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(packageItem.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = packageItem.PackageName
                        }
                    },
                    Quantity = 1
                };
                options.LineItems.Add(sessionLineItem);
                //}
                var service = new SessionService();
                //create sessionId and paymentIntentId
                Session session = service.Create(options);
                _unitOfWork.OrderHeaderObj.UpdateStripePaymentId(PackagePaymentVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                //applicationUser.AvaiblePost = package.amountPost;
                //_unitOfWork.ApplicationUserObj.Update(applicationUser);
                //_unitOfWork.Save();

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            return View(PackagePaymentVM);
        }

        public IActionResult PackageOrderConfirmation(int id, int packageID)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);
            Package package = _unitOfWork.PackageObj.Get(u => u.PackageId == packageID);
            OrderHeader orderHeader = _unitOfWork.OrderHeaderObj.Get(u => u.Id == id, includeProperties: "applicationUser");

            //this is an order by customer
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork.OrderHeaderObj.UpdateStripePaymentId(id, session.Id, session.PaymentIntentId);
                _unitOfWork.OrderHeaderObj.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                _unitOfWork.Save();
            }
            // update amountPost
            //applicationUser.AvaiblePost = package.amountPost;
            //_unitOfWork.ApplicationUserObj.Update(applicationUser);
            //_unitOfWork.Save();

            //List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCartObj.GetAll(u => u.buyerID == orderHeader.applicationUserId).ToList();
            //_unitOfWork.ShoppingCartObj.RemoveRange(shoppingCarts);
            //_unitOfWork.Save();
            return View(id);
        }

        public IActionResult Reset()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);
            applicationUser.AvaiblePost = 0;
            _unitOfWork.ApplicationUserObj.Update(applicationUser);
            _unitOfWork.Save();
            IEnumerable<Package> packageList = _unitOfWork.PackageObj.GetAll();
            return RedirectToAction("Index");
        }
    }
}
