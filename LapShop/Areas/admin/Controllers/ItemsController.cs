using LapShop.Bl;
using Microsoft.AspNetCore.Mvc;
using LapShop.Utlities;
using Microsoft.AspNetCore.Authorization;

namespace LapShop.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin, data entry")]
    [Area("admin")]

    public class ItemsController : Controller
    {
        IItems oClsItems;
        ICategories oClsCategories;
        IItemTypes oClsItemTypes;
        IOS oClsOs;
        public ItemsController(IItems item,ICategories category,IItemTypes itemTypes,IOS ios)
        {
            oClsItems = item;
            oClsCategories = category;
            oClsItemTypes = itemTypes;
            oClsOs = ios;
        }


        //[AllowAnonymous]
        public IActionResult List()
        {
            ViewBag.lstCategories = new List<TbCategory>()
            {
                new TbCategory{
                    CategoryId = 0,
                    CategoryName = ""
                }
            };
            ViewBag.lstCategories.AddRange(oClsCategories.GetAll());
            var items = oClsItems.GetAllItemsData(null);
            return View(items);
        }

        public IActionResult Search(int id)
        {
            ViewBag.lstCategories = oClsCategories.GetAll();


            var items = oClsItems.GetAllItemsData(id);
            return View("List", items);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? itemId)
        {
            var item = new TbItem();
            ViewBag.lstCategories = oClsCategories.GetAll();
            ViewBag.lstItemTypes = oClsItemTypes.GetAll();
            ViewBag.lstOs = oClsOs.GetAll();
            if (itemId != null)
            {
                item = oClsItems.GetById(Convert.ToInt32(itemId));
            }
            return View(item);
        }
        public IActionResult Delete(int itemId)
        {
            oClsItems.Delete(itemId);
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", item);

            item.ImageName = await Helper.UploadImage(Files, "Items");

            oClsItems.Save(item);

            return RedirectToAction("List");
        }

    }
}
