﻿using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models;
using H3ArT.Models.Models;
using H3ArT.Models.ViewModels;
using H3ArT.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace H3ArTArtwork.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer, Creator")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCartObj.GetAll(u => u.BuyerId == userId, includeProperties: "Artwork"),
                OrderHeader = new()
            };
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = cart.Artwork.Price;
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Price;
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCartObj.Get(u => u.ShoppingCartId == cartId, tracked: true);

            // Fetch the associated orderHeaderId using userId or any other relevant information
            var userId = cartFromDb.BuyerId;
            var orderHeader = _unitOfWork.OrderHeaderObj.Get(o => o.ApplicationUserId == userId && o.OrderStatus == SD.StatusPending);

            if (orderHeader != null)
            {
                // Remove associated OrderDetail record
                var orderDetail = _unitOfWork.OrderDetailObj.Get(od => od.ArtworkId == cartFromDb.ArtworkId && od.OrderHeaderId == orderHeader.Id);
                if (orderDetail != null)
                {
                    _unitOfWork.OrderDetailObj.Remove(orderDetail);
                }
            }

            _unitOfWork.ShoppingCartObj.Remove(cartFromDb);
            //Remove from session
            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCartObj.GetAll(u => u.BuyerId == cartFromDb.BuyerId).Count() - 1);

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCartObj.GetAll(u => u.BuyerId == userId, includeProperties: "Artwork"),
                OrderHeader = new()
            };
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);

            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.FullName;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = cart.Artwork.Price;
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Price;
            }
            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            //get the id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //ShoppingCartVM will automatically be populated


            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCartObj.GetAll(u => u.BuyerId == userId, includeProperties: "Artwork");


            ApplicationUser applicationUser = _unitOfWork.ApplicationUserObj.Get(u => u.Id == userId);

            ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;


            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {

                cart.Price = cart.Artwork.Price;
                ShoppingCartVM.OrderHeader.PhoneNumber += cart.Price;
                if (cart.Artwork.IsBought)
                {
                    // Add model error if artwork is already bought
                    ModelState.AddModelError("", $"The artwork '{cart.Artwork.Title}' has already been purchased.");
                    return View(ShoppingCartVM); // or any other suitable action result
                }
            }
            if (!ModelState.IsValid)
            {
                // If model state is not valid, return the view with validation errors
                return View(ShoppingCartVM); // or any other suitable action result
            }
            var existingOrder = _unitOfWork.OrderHeaderObj.Get(o => o.ApplicationUserId == userId && o.PaymentStatus == SD.PaymentStatusPending);

            if (existingOrder != null)
            {
                ShoppingCartVM.OrderHeader = existingOrder;
                _unitOfWork.OrderHeaderObj.Update(existingOrder);
                _unitOfWork.Save();
                var newShoppingCartItems = ShoppingCartVM.ShoppingCartList.Where(cart => cart.IsNew);

                foreach (var cart in newShoppingCartItems)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        ArtworkId = cart.ArtworkId,
                        OrderHeaderId = existingOrder.Id,
                        Price = cart.Price,
                        Count = cart.Count
                    };

                    _unitOfWork.OrderDetailObj.Add(orderDetail);
                    cart.IsNew = false; // Reset isNew flag
                    _unitOfWork.ShoppingCartObj.Update(cart);
                }
            }
            else
            {
                //it is a regular customer account
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;

                _unitOfWork.OrderHeaderObj.Add(ShoppingCartVM.OrderHeader);
                _unitOfWork.Save();
                foreach (var cart in ShoppingCartVM.ShoppingCartList)
                {
                    OrderDetail orderDetail = new()
                    {
                        ArtworkId = cart.ArtworkId,
                        OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                        Price = cart.Price,
                        Count = cart.Count
                    };
                    _unitOfWork.OrderDetailObj.Add(orderDetail);
                    _unitOfWork.Save();
                    cart.IsNew = false; // Reset isNew flag
                    _unitOfWork.ShoppingCartObj.Update(cart);
                }

            }

            //stripe logic
            var domain = "https://localhost:44358/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                CancelUrl = domain + "customer/cart/index",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };
            foreach (var item in ShoppingCartVM.ShoppingCartList)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Artwork.Title
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }
            var service = new SessionService();
            //create sessionId and paymentIntentId
            Session session = service.Create(options);
            _unitOfWork.OrderHeaderObj.UpdateStripePaymentId(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeaderObj.Get(u => u.Id == id, includeProperties: "ApplicationUser");
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCartObj.GetAll(u => u.BuyerId == orderHeader.ApplicationUserId, includeProperties: "Artwork").ToList();
            //this is an order by customer
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork.OrderHeaderObj.UpdateStripePaymentId(id, session.Id, session.PaymentIntentId);
                _unitOfWork.OrderHeaderObj.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                _unitOfWork.Save();

                foreach (var cartItem in shoppingCarts)
                {
                    cartItem.Artwork.IsBought = true;
                    _unitOfWork.ArtworkObj.Update(cartItem.Artwork);
                }
                _unitOfWork.Save();
            }

            HttpContext.Session.Clear();

            _unitOfWork.ShoppingCartObj.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            return View(id);
        }

    }
}