using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using System.Web;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Controllers
{
    public class BasketController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly Microsoft.Extensions.Logging.ILogger<BasketController> _logger;
        public BasketController(IOrderService orderService, IUserService userService, IProductService productService, ILogger<BasketController> logger)
        {
            _logger = logger;
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult AddToCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
                var product = _productService.GetProductByIdIncludeImage(id);
                _orderService.AddProductToCartItem(product, userId);
                return ViewComponent("CartComponent");
            }
            else
            {
                return PartialView("Product/_LoginModalForm");
            }
        }
        [Authorize]
        public IActionResult RemoveFromCart(int id)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _productService.GetProductByIdIncludeImage(id);
            _orderService.DeleteProductFromCartItem(product, userId);
            return ViewComponent("CartComponent");
        }

        [Authorize]
        public IActionResult AddToOrder(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _productService.GetProductByIdIncludeImage(id);
            _orderService.AddProductToCartItem(product, userId);
            return ViewComponent("OrderComponent");
        }
        [Authorize]
        public IActionResult RemoveFromOrder(int id)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _productService.GetProductByIdIncludeImage(id);
            _orderService.DeleteProductFromCartItem(product, userId);
            return ViewComponent("OrderComponent");
        }
        [Authorize]
        public IActionResult Order(int id)
        {
            if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                var orderViewModel = new OrderViewModel();
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
            }
            return BadRequest();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Order(int cartId, int addressId)
        {
            try
            {
                if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
                {

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
                        var orderViewModel = new OrderViewModel();
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
                return BadRequest();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult UseDiscountCode(string discountCode, int cartId)
        {
            if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                var cart = _orderService.GetExistingOpenCartIncludeProducts(userId, cartId);
                var orderViewModel = new OrderViewModel();
                if (cart != null)
                {
                    ViewBag.showDiscountError = false;
                    var resultType = _orderService.UseDiscountCode(userId, discountCode, cartId);
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
            return BadRequest();
        }




        private List<SessionLineItemOptions> GetLineItems(int cartId)
        {
            var lineItems = new List<SessionLineItemOptions>();

            if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                var cart = _orderService.GetExistingOpenCartIncludeProducts(userId, cartId);
                var order = _orderService.GetOrderByCartId(cart.Id);

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
            }
            return lineItems;
        }

    }
}
