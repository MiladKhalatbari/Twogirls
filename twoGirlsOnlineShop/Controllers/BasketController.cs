using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using Stripe.Issuing;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Security.Claims;
using System.Web;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;
using twoGirlsOnlineShop.Models;

namespace twoGirlsOnlineShop.Controllers
{
    public class BasketController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly ILogger<BasketController> _logger;
        public BasketController(IOrderService orderService, IUserService userService, IProductService productService, ILogger<BasketController> logger)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult AddToCart(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _productService.GetProductByIdIncludeImage(id);
            _orderService.AddProductToCartItem(product, userId);
            return ViewComponent("CartComponent");
        }
        [Authorize]
        public IActionResult RemoveFromCart(int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _productService.GetProductByIdIncludeImage(id);
            _orderService.DeleteProductFromCartItem(product, userId);
            return ViewComponent("CartComponent");
        }

        [Authorize]
        public IActionResult AddToOrder(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _productService.GetProductByIdIncludeImage(id);
            _orderService.AddProductToCartItem(product, userId);
            return ViewComponent("OrderComponent");
        }
        [Authorize]
        public IActionResult RemoveFromOrder(int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _productService.GetProductByIdIncludeImage(id);
            _orderService.DeleteProductFromCartItem(product, userId);
            return ViewComponent("OrderComponent");
        }
        [Authorize]
        public IActionResult Order(int id)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            OrderViewModels orderViewModel = new OrderViewModels();
            var cart = _orderService.GetCartByIdIncludeProducts(userId, id);
            if (cart != null)
            {
                cart.Order = _orderService.GetOrderByCartId(cart.Id);
                orderViewModel.Cart = cart;
                orderViewModel.Address = new TwoGirls.DataLayer.Entities.Address()
                {
                    UserId = userId
                };
                if (orderViewModel.Cart.IsClose)
                {
                    orderViewModel.Addresses = new List<TwoGirls.DataLayer.Entities.Address>();
                    var orderAddress = _orderService.GetOrderAddressByCartId(cart.Id);
                    orderViewModel.Addresses.Add(orderAddress);

                }
                else
                {
                    orderViewModel.Addresses = _userService.GetUserAddresses(userId);
                }

                return View(orderViewModel);

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Order(int cartId, int addressId)
        {
            try
            {
                int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (_userService.CheckAddressId(userId, addressId))
                {
                    var options = new Stripe.Checkout.SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string> { "card" },
                        LineItems = GetLineItems(cartId),
                        Mode = "payment",
                        SuccessUrl = "https://Twogirls.somee.com/Success",
                        CancelUrl = "https://Twogirls.somee.com/Failed",
                        PaymentIntentData = new SessionPaymentIntentDataOptions()
                        {
                            Metadata = new Dictionary<string, string>
                    {
                        { "cartId" , cartId.ToString()},
                        {"addressId",addressId.ToString()},
                        {"userId",userId.ToString()},
                        {"gatewayType","order" },
                    }
                        }
                    };

                    var service = new Stripe.Checkout.SessionService();
                    var session = await service.CreateAsync(options);
                    return Redirect(session.Url);
                }
                else
                {
                    ViewBag.showAddressError = true;
                    var cart = _orderService.GetCartByIdIncludeProducts(userId, cartId);
                    OrderViewModels orderViewModel = new OrderViewModels();
                    if (cart != null)
                    {
                        orderViewModel.Cart = cart;
                        orderViewModel.Address = new TwoGirls.DataLayer.Entities.Address()
                        {
                            UserId = userId
                        };
                        if (orderViewModel.Cart.IsClose)
                        {
                            orderViewModel.Addresses = new List<TwoGirls.DataLayer.Entities.Address>()
                    {
                        _orderService.GetOrderAddressByCartId(cart.Id)
                    };
                        }
                        else
                        {
                            orderViewModel.Addresses = _userService.GetUserAddresses(userId);
                        }
                        return View(orderViewModel);
                    }
                    return NotFound();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }
        private string GetRequestBody()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                return reader.ReadToEnd();
            }
        }
        [HttpPost]
        public IActionResult UpdatePaymentStatus()
        {
            try
            {
                var json = GetRequestBody();

                _logger.LogInformation("Stripe webhook received: " + json);

                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], "your_webhook_secret");

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    var metadata = paymentIntent.Metadata;
                    if (metadata.TryGetValue("gatewayType", out var gatewayType))
                    {
                        if (gatewayType == "wallet")
                        {
                            if (metadata.TryGetValue("amount", out var amountString) &&
                                 metadata.TryGetValue("userId", out var userIdString))
                            {
                                if (int.TryParse(userIdString, out int userId) && decimal.TryParse(amountString, out decimal amount))
                                {                                    
                                    //add transaction
                                    var Transaction = new TwoGirls.DataLayer.Entities.Transaction()
                                    {
                                        TypeId = 1,
                                        UserId = userId,
                                        Amount = amount,
                                        Date = DateTime.Now,
                                        Description = "Wallet Deposit",
                                        Finaly = true
                                    };
                                    _userService.AddTransaction(Transaction);
                                    return Ok();
                                }
                            }
                        }
                        else if (gatewayType == "order")
                        {

                            if (metadata.TryGetValue("cartId", out var cartIdString) &&
                                 metadata.TryGetValue("userId", out var userIdString) &&
                                 metadata.TryGetValue("addressId", out var addressIdString))
                            {
                                if (int.TryParse(cartIdString, out int cartId) &&
                                    int.TryParse(userIdString, out int userId) &&
                                    int.TryParse(addressIdString, out int addressId))
                                {
                                    var cart = _orderService.GetExistingOpenCartIncludeProducts(userId, cartId);

                                    //add transactions
                                    var Transaction = new TwoGirls.DataLayer.Entities.Transaction()
                                    {
                                        TypeId = 1,
                                        UserId = userId,
                                        Amount = cart.TotalOrderPrice(),
                                        Date = DateTime.Now,
                                        Description = "Wallet Deposit",
                                        Finaly = true
                                    };
                                    _userService.AddTransaction(Transaction);
                                    var Payment = new TwoGirls.DataLayer.Entities.Transaction()
                                    {
                                        TypeId = 2,
                                        UserId = userId,
                                        Amount = cart.TotalOrderPrice(),
                                        Date = DateTime.Now,
                                        Description = "Payment",
                                        Finaly = true
                                    };
                                    _userService.AddTransaction(Payment);

                                    // cart is finaly
                                    cart.IsClose = true;
                                    _orderService.UpdateCart(cart);

                                    //update order
                                    var order = _orderService.GetOrderByCartId(cartId);
                                    order.OrderDate = DateTime.Now;
                                    order.IsFinally = true;
                                    order.AddressId = addressId;
                                    _orderService.UpdateOrder(order);

                                    _logger.LogInformation($"Payment succeeded for CartId: {cartId}, UserId: {userId}, AddressId: {addressId}");

                                    // open a new cart and order
                                    _orderService.GetOpenCartIncludeProductsOrCreateNew(userId);
                                    //return orderlist view
                                    var user = _userService.GetUserById(userId);
                                    ViewData["UserAvatarViewModel"] = new UserAvatarViewModel()
                                    {
                                        ImagePath = user.ImagePath,
                                        FullName = user.FirstName + " " + user.LastName,
                                        Email = user.Email
                                    };
                                    var orders = _orderService.GetAllOrdersByUserId(userId);
                                    ViewData["OrderList"] = orders;
                                    return View("/Views/UserPanel/OrderList.cshtml");
                                    return Ok();
                                }
                            }
                        }

                    }
                }

                _logger.LogWarning($"Unexpected webhook event type: {stripeEvent.Type}");

                return BadRequest();

            }

            catch (StripeException ex)
            {
                _logger.LogError($"Stripe webhook error: {ex.Message}");
                return StatusCode((int)ex.HttpStatusCode);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Webhook processing error: {ex.Message}");
                return StatusCode(500);
            }
        }

    

        [HttpPost]
        public IActionResult UseDiscountCode(string discountCode, int cartId)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cart = _orderService.GetExistingOpenCartIncludeProducts(userId, cartId);
            OrderViewModels orderViewModel = new OrderViewModels();
            if (cart != null)
            {
                ViewBag.showDiscountError = false;
                DiscountResultType resultType = _orderService.UseDiscountCode(userId, discountCode, cartId);
                switch (resultType)
                {
                    case DiscountResultType.Success:
                        break;
                    case DiscountResultType.NotFound:
                        ViewBag.showDiscountError = true;
                        ViewBag.DiscountError = "Not Found!";
                        break;
                    case DiscountResultType.Expired:
                        ViewBag.showDiscountError = true;
                        ViewBag.DiscountError = "Discount Expired!";
                        break;
                    case DiscountResultType.Finished:
                        ViewBag.showDiscountError = true;
                        ViewBag.DiscountError = "Discount Finished!";
                        break;
                    case DiscountResultType.UseTwice:
                        ViewBag.showDiscountError = true;
                        ViewBag.DiscountError = "You Have used this code!";
                        break;
                    case DiscountResultType.NotStarted:
                        ViewBag.showDiscountError = true;
                        ViewBag.DiscountError = "Not Started yet!";
                        break;
                }
                cart.Order = _orderService.GetOrderByCartId(cart.Id);
                orderViewModel.Cart = cart;
                orderViewModel.Address = new TwoGirls.DataLayer.Entities.Address()
                {
                    UserId = userId
                };
                if (orderViewModel.Cart.IsClose)
                {
                    orderViewModel.Addresses = new List<TwoGirls.DataLayer.Entities.Address>()
                    {
                        _orderService.GetOrderAddressByCartId(cart.Id)
                    };
                }
                else
                {
                    orderViewModel.Addresses = _userService.GetUserAddresses(userId);
                }

                return View("Order", orderViewModel);

            }
            return NotFound();
        }




        private List<SessionLineItemOptions> GetLineItems(int cartId)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cart = _orderService.GetExistingOpenCartIncludeProducts(userId, cartId);
            var order = _orderService.GetOrderByCartId(cart.Id);
            var lineItems = new List<SessionLineItemOptions>();

            foreach (var cartItem in cart.CartItems)
            {
                var lineItem = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(cartItem.Product.DiscountedPrice * 100 * (100 - order.DiscountPercent) / 100), // Price is in USD cents.
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = cartItem.Product.Title,
                            Images = new List<string>() { HttpUtility.UrlPathEncode(cartItem.Product.ImagePaths.Select(i => "https://Twogirls.somee.com/" + i.Url).First()) }
                        }
                    },
                    Quantity = cartItem.Quantity
                };

                lineItems.Add(lineItem);
            }

            return lineItems;
        }

    }
}
