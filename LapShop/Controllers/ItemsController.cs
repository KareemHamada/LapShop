using LapShop.Bl;
using Microsoft.AspNetCore.Mvc;
using LapShop.Models;

namespace LapShop.Controllers
{
    public class ItemsController : Controller
    {
        IItems OclsItems;
        IItemImages OclsItemImages;
        public ItemsController(IItems item,IItemImages itemImages)
        {
            OclsItems = item;
            OclsItemImages = itemImages;
        }
        public IActionResult ItemDetails(int itemId)
        {
            VmItemDetails vmItemDetails = new VmItemDetails();
            vmItemDetails.Item = OclsItems.GetItemById(itemId);
            vmItemDetails.lstRecommendedItems = OclsItems.GetRecommendedItems(itemId); 
            vmItemDetails.lstItemImages = OclsItemImages.GetByItemId(itemId);
            return View(vmItemDetails);
        }

        public IActionResult ItemList()
        {
            return View();
        }

    }
}
