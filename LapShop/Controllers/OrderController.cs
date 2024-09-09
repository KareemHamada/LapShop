using Microsoft.AspNetCore.Mvc;
using LapShop.Bl;
using LapShop.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Bl;

namespace LapShop.Controllers
{
    public class OrderController : Controller
    {
        IItems itemService; 
        UserManager<ApplicationUser> _userManager;
        ISalesInvoice salesInvoiceService;
        public OrderController(IItems items, UserManager<ApplicationUser> userManager, ISalesInvoice ssalesInvoiceService)
        {
            itemService = items;
            _userManager = userManager;
            salesInvoiceService = ssalesInvoiceService;
        }

        public IActionResult MyOrders()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> OrderSuccess()
        {
            var cookiesCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                cookiesCart = HttpContext.Request.Cookies["Cart"];

            var cart = JsonConvert.DeserializeObject<ShoppingCart>(cookiesCart);
            await SaveOrder(cart);
            return View();
        }

        // using cookies
        public IActionResult Cart()
        {
            var cookiesCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                cookiesCart = HttpContext.Request.Cookies["Cart"];

            var cart = JsonConvert.DeserializeObject<ShoppingCart>(cookiesCart);
            return View(cart);
        }

        public IActionResult AddToCart(int itemId)
        {
            ShoppingCart cart;
            //HttpContext.Request.Cookies["Cart"]
            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
            else
                cart = new ShoppingCart();

            var item = itemService.GetById(itemId);
            var itemInList = cart.lstItems.Where(a => a.ItemId == itemId).FirstOrDefault();
            if (itemInList != null)
            {
                itemInList.Qty += 1;
                itemInList.Total = itemInList.Qty * itemInList.Price;
            }
            else
            {
                cart.lstItems.Add(
                new ShoppingCartItem
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Price = item.SalesPrice,
                    Qty = 1,
                    Total = item.SalesPrice

                });
            }

            cart.Total = cart.lstItems.Sum(a => a.Total);

            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Cart");
        }

        async Task SaveOrder(ShoppingCart oShopingCart)
        {
            try
            {
                List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();
                foreach (var item in oShopingCart.lstItems)
                {
                    lstInvoiceItems.Add(new TbSalesInvoiceItem()
                    {
                        ItemId = item.ItemId,
                        Qty = item.Qty,
                        InvoicePrice = item.Price
                    });
                }

                var user = await _userManager.GetUserAsync(User);

                TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(5),
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now
                };

                salesInvoiceService.Save(oSalesInvoice, lstInvoiceItems, true);
            }
            catch (Exception ex)
            {

            }
        }


        #region Using Sessions
        //public IActionResult Cart()
        //{
        //    var sessionCart = string.Empty;
        //    if (HttpContext.Session.GetString("Cart") != null)
        //        sessionCart = HttpContext.Session.GetString("Cart");

        //    var cart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);
        //    return View(cart);
        //}

        //public IActionResult AddToCart(int itemId)
        //{
        //    ShoppingCart cart;
        //    if (HttpContext.Session.GetString("Cart") != null)
        //        cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Session.GetString("Cart"));
        //    else
        //        cart = new ShoppingCart();

        //    var item = itemService.GetById(itemId);
        //    var itemInList = cart.lstItems.Where(a=>a.ItemId == itemId).FirstOrDefault();
        //    if (itemInList != null)
        //    {
        //        itemInList.Qty += 1;
        //        itemInList.Total = itemInList.Qty * itemInList.Price;
        //    }
        //    else
        //    {
        //        cart.lstItems.Add(
        //        new ShoppingCartItem
        //        {
        //            ItemId = item.ItemId,
        //            ItemName = item.ItemName,
        //            Price = item.SalesPrice,
        //            Qty = 1,
        //            Total = item.SalesPrice

        //        });
        //    }

        //    cart.Total = cart.lstItems.Sum(a => a.Total);

        //    HttpContext.Session.SetString("Cart",JsonConvert.SerializeObject(cart));
        //    return RedirectToAction("Cart");
        //} 
        #endregion
    }
}
