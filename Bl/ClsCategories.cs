namespace LapShop.Bl
{
	public interface ICategories
	{
		public List<TbCategory> GetAll();
		public TbCategory GetById(int id);
		public bool Save(TbCategory category);
		public bool Delete(int id);
    }
    public class ClsCategories: ICategories
    {
        LapShopContext context;
        public ClsCategories(LapShopContext ctx)
        {
            context = ctx;
        }
        // history tables
        public List<TbCategory>GetAll()
		{
			try
			{
				var lstCategories = context.TbCategories.Where(a=>a.CurrentState == 1).ToList();
				return lstCategories;
			}
			catch (Exception ex) {
				return new List<TbCategory>();
			}
			
		}

		public TbCategory GetById(int id)
		{
			try
			{
				var category = context.TbCategories.FirstOrDefault(a => a.CategoryId == id && a.CurrentState == 1);
				return category;
			}
			catch
			{
				return new TbCategory();
			}
		}

		public bool Save(TbCategory category)
		{
			try
			{
				if (category.CategoryId == 0)
				{
					category.CreatedBy = "1";
					category.CreatedDate = DateTime.Now;
					context.TbCategories.Add(category);
				}
				else
				{
					category.UpdatedBy = "1";
					category.UpdatedDate = DateTime.Now;
					context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
				var category = GetById(id);
				//context.TbCategories.Remove(category);
				category.CurrentState = 0;
				context.SaveChanges();
				return true;
			}catch(Exception ex)
			{
				return false;
			}

		}
	}
}
