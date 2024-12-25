using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IdentityDbContext>(c => c.UseInMemoryDatabase("my_db"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(x => {
    x.User.RequireUniqueEmail = false;
    }).AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllers();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Ensure roles exist and create user
    if (!await roleManager.RoleExistsAsync("admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("admin"));
    }

    var user = new IdentityUser { UserName = "zainulabdin", Email = "zainulabdin.uos@gmail.com" };
    var result = await mgr.CreateAsync(user, "Password123!");

    if (result.Succeeded)
    {
        await mgr.AddToRoleAsync(user, "admin");
    }
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapGet("/", () => "Hello World!");

app.Run();
