using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LapShop.Bl;
using LapShop.Models;

namespace LapShop.Controllers
{
    public class HomeController : Controller
    {
        IItems oClsItems;
        ICategories oClsCategories;
        ISliders oClsSliders;
        public HomeController(IItems item, ICategories categories,ISliders oSliders)
        {
            oClsItems = item;
            oClsCategories = categories;
            oClsSliders = oSliders;
        }
        public IActionResult Index()
        {
            VmHomePage vm = new VmHomePage();
            vm.lstAllItems = oClsItems.GetAllItemsData(null).Skip(20).Take(20).ToList();
            vm.lstRecommendedItems = oClsItems.GetAllItemsData(null).Skip(60).Take(6).ToList();
            vm.lstNewItems = oClsItems.GetAllItemsData(null).Skip(90).Take(6).ToList();
            vm.lstFreeDelivry = oClsItems.GetAllItemsData(null).Skip(200).Take(4).ToList();
            vm.lstCategories = oClsCategories.GetAll().Take(4).ToList();
            vm.lstSliders = oClsSliders.GetAll();
            return View(vm);
        }

        #region Comments
        //      IItems OclsItems;
        //      public HomeController(IItems items)
        //      {
        //	OclsItems = items;
        //      }
        //      public class VwItemCategories
        //{
        //	public string ItemName { get; set; }
        //	public string CategoryName { get; set; }
        //}
        //      public IActionResult Index()
        //      {
        // //         LapShopContext ctx = new LapShopContext();

        //	//var categories = ctx.TbCategories.Where(a => a.CategoryName.EndsWith("p")).ToList();

        //	var items = OclsItems.GetAllItemsData(null);

        //	return View(items);

        //	#region Comments
        //	//var categories = ctx.TbCategories.OrderByDescending(a=>a.CategoryName).ToList();
        //	//var categories = ctx.TbCategories.Where(a => a.CategoryName == "Apple").ToList();
        //	//var categories = ctx.TbCategories.Where(a => a.CategoryName.Contains("p")).ToList();
        //	//var categories = ctx.TbCategories.Where(a => a.CategoryName.StartsWith("p")).ToList();


        //	//categories.Count();

        //	//categories.Select(s => new TbCategory()
        //	//         {
        //	//             CategoryId = s.CategoryId,
        //	//             CategoryName = s.CategoryName,
        //	//         });


        //	////var lstItems = ctx.TbItems.ToList();
        //	////var lstItems = ctx.TbItems.Sum(a=>a.SalesPrice);
        //	////var lstItems = (from i in ctx.TbItems
        //	////                where i.SalesPrice > 2000
        //	////                select i).ToList();

        //	////var lstItems = (from i in ctx.TbItems
        //	////				where i.SalesPrice > 2000
        //	////				select new TbItem()
        //	////				{
        //	////					ItemId = i.ItemId,
        //	////					ItemName = i.ItemName,
        //	////					SalesPrice = i.SalesPrice,
        //	////				}).OrderBy(s=>s.CreatedDate).ToList();

        //	//var lstItems = (from i in ctx.TbItems join  c in ctx.TbCategories
        //	//				on i.CategoryId equals c.CategoryId
        //	//				where i.SalesPrice > 2000
        //	//				select new VwItemCategories()
        //	//				{
        //	//					ItemName = i.ItemName,
        //	//					CategoryName = c.CategoryName,
        //	//				}).OrderBy(s => s.CategoryName).ToList();


        //	//var Items = ctx.TbItems.FromSqlRaw("Select * from TbItems");
        //	//var ItemCollection = ctx.Database.ExecuteSqlRaw(""); 
        //	#endregion

        //} 
        #endregion
    }
}
