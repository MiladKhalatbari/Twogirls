using Microsoft.AspNetCore.Mvc;
using Stripe;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IOrderService orderService)
        {
            _logger = logger;
            _userService = userService;
            _orderService = orderService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("Success")]
        public IActionResult Success()
        {
            return View();
        }
        [Route("Failed")]
        public IActionResult Failed()
        {
            return View();
        }
        private string GetRequestBody()
        {
            using var reader = new StreamReader(Request.Body);
            return reader.ReadToEnd();
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
                                if (int.TryParse(userIdString, out var userId) && decimal.TryParse(amountString, out var amount))
                                {
                                    //add transaction
                                    var transaction = new TwoGirls.DataLayer.Entities.Transaction()
                                    {
                                        TypeId = 1,
                                        UserId = userId,
                                        Amount = amount,
                                        Date = DateTime.Now,
                                        Description = "Wallet Deposit",
                                        Finaly = true
                                    };
                                    _userService.AddTransaction(transaction);
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
                                if (int.TryParse(cartIdString, out var cartId) &&
                                    int.TryParse(userIdString, out var userId) &&
                                    int.TryParse(addressIdString, out var addressId))
                                {
                                    var cart = _orderService.GetExistingOpenCartIncludeProducts(userId, cartId);

                                    //add transactions
                                    var transaction = new TwoGirls.DataLayer.Entities.Transaction()
                                    {
                                        TypeId = 1,
                                        UserId = userId,
                                        Amount = cart.TotalOrderPrice(),
                                        Date = DateTime.Now,
                                        Description = "Wallet Deposit",
                                        Finaly = true
                                    };
                                    _userService.AddTransaction(transaction);
                                    var payment = new TwoGirls.DataLayer.Entities.Transaction()
                                    {
                                        TypeId = 2,
                                        UserId = userId,
                                        Amount = cart.TotalOrderPrice(),
                                        Date = DateTime.Now,
                                        Description = "Payment",
                                        Finaly = true
                                    };
                                    _userService.AddTransaction(payment);

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
                                    //return View("/Views/UserPanel/OrderList.cshtml");
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
    }
}