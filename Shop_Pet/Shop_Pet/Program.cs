using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Shop_Pet.Models.DataModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ShopPetDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Sqlconnect")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();//giỏ hàng
builder.Services.AddSession(
    options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(10);
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = "Shop_Pet.session";
      
        options.Cookie.IsEssential = true;
    }

    );

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication("ShopPet").AddCookie("ShopPet", Option =>
{
    //đường dẫn cấm truy Cập Admin

    Option.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = "Shop_Pet.cookie",
        Path = "/",
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.SameAsRequest
    };
    Option.LoginPath = "/Admin/Home/Login";
    Option.ReturnUrlParameter = "requestUrl";
    Option.SlidingExpiration = true;
});
//can co khi tao tai khoan(tat ca builder)

/*// Add services to the container.
builder.Services.AddControllersWithViews();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();//can co khi tao tai khoan
}



app.UseHttpsRedirection();//can co khi tao tai khoan
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();//can co khi tao tai khoan

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
