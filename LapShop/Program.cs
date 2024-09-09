using LapShop.Bl;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Bl;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Xml;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LapShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));





// identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
	options.Password.RequiredLength = 8;
	//options.Password.RequireNonAlphanumeric = true;
	//options.Password.RequireUppercase = true;
	//options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<LapShopContext>();




builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Users/AccessDenied";
    options.Cookie.Name = "Cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
    options.LoginPath = "/Users/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});


//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "Lao Shop Api",
//        Description = "api to access items and related categories",
//        TermsOfService = new Uri("https://itlegend.net/"),
//        Contact = new OpenApiContact
//        {
//            Email = "info@itlegend.net",
//            Name = "Ali Shahin",
//            Url = new Uri("https://itlegend.net/")
//        },
//        License = new OpenApiLicense
//        {
//            Name = "It Legend Licence",
//            Url = new Uri("https://itlegend.net/")
//        }
//    });

//    var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var fullPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
//    options.IncludeXmlComments(fullPath);
//});

builder.Services.AddScoped<ICategories, ClsCategories>();
builder.Services.AddScoped<IItems, ClsItems>();
builder.Services.AddScoped<IItemTypes, ClsItemTypes>();
builder.Services.AddScoped<IOS, ClsOs>();
builder.Services.AddScoped<IItemImages, ClsItemImages>();
builder.Services.AddScoped<ISalesInvoice, ClsSalesInvoice>();
builder.Services.AddScoped<ISalesInvoiceItems, ClsSalesInvoiceItems>();
builder.Services.AddScoped<ISliders, ClsSliders>();


// using sessions in website
builder.Services.AddSession();
// using this action to deal with sessions between logic classes
builder.Services.AddHttpContextAccessor();
// using this action to cache data in the server
builder.Services.AddDistributedMemoryCache();


var app = builder.Build();
 
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//    options.RoutePrefix = string.Empty;
//});




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// using with identity
app.UseAuthentication();
app.UseAuthorization();

// using sessions
app.UseSession();



app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	name: "admin",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
	endpoints.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}"
	);


}
);
app.Run();
