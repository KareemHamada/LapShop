using LapShop.Bl;

namespace LapShop.Bl
{
    public interface IItems
    {
        public List<TbItem> GetAll();
        public TbItem GetById(int id);
        public VwItem GetItemById(int id);

        public List<VwItem> GetAllItemsData(int? categoryId);
        public List<VwItem> GetRecommendedItems(int itemId);

        public bool Save(TbItem item);
        public bool Delete(int id);
    }
    public class ClsItems : IItems
    {
        LapShopContext context;
        public ClsItems(LapShopContext ctx)
        {
            context = ctx;
        }

        public List<TbItem> GetAll()
        {
            try
            {
                var lstItems = context.TbItems.Where(a=>a.CurrentState == 1).OrderByDescending(a=>a.CreatedDate).ToList();
                return lstItems;
            }
            catch (Exception ex)
            {
                return new List<TbItem>();
            }

        }

        public List<VwItem> GetAllItemsData(int? categoryId)
        {
            try
            {
                var items = context.VwItems.Where(a => (a.CategoryId == categoryId || categoryId == null || categoryId == 0 ) && a.CurrentState == 1 && !string.IsNullOrEmpty(a.ItemName)).OrderByDescending(x=>x.CreatedDate).ToList();
                return items;
            }catch(Exception ex)
            { 
                return new List<VwItem>();
            }
        }

        public List<VwItem> GetRecommendedItems(int itemId)
        {
            try
            {
                var itemPrice = GetById(itemId).SalesPrice;

                var items = context.VwItems.Where(a => a.SalesPrice >= itemPrice - 50 && a.SalesPrice <= itemPrice +50 && a.CurrentState == 1).Take(10).ToList();
                return items;
            }
            catch (Exception ex)
            {
                return new List<VwItem>();
            }
        }

            public TbItem GetById(int id)
        {
            try
            {
                var item = context.TbItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
                return item;
            }
            catch
            {
                return new TbItem();
            }
        }
        public VwItem GetItemById(int id)
        {
            try
            {
                var item = context.VwItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
                return item;
            }
            catch
            {
                return new VwItem();
            }
        }

        public bool Save(TbItem item)
        {
            try
            {
                if (item.ItemId == 0)
                {
                    item.CurrentState = 1;
                    item.CreatedBy = "1";
                    item.CreatedDate = DateTime.Now;
                    context.TbItems.Add(item);
                }
                else
                {
                    item.UpdatedBy = "1";
                    item.UpdatedDate = DateTime.Now;
                    context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var item = GetById(id);
                //context.TbItems.Remove(item);
                item.CurrentState = 0;
                context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }


}
