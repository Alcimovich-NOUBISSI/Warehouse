using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Store.Data.Repositories;
using Store.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CategoryDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityContextConnection")
    ));

builder.Services.AddDefaultIdentity<StoreUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

#region Authorization

AddAuthorizationPolicies(builder.Services);

#endregion

AddScoped();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

void AddAuthorizationPolicies(IServiceCollection services)
{
    services.AddAuthorization(options => options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber")));
    services.AddAuthorization(options => options.AddPolicy("RequireManager", policy => policy.RequireClaim("Manager")));
    services.AddAuthorization(options => options.AddPolicy("RequireAdministrator", policy => policy.RequireClaim("Administrator")));
    services.AddAuthorization(options => options.AddPolicy("RequireUser", policy => policy.RequireClaim("User")));
    //services.AddAuthorization(options => options.AddPolicy("RequireName", policy => policy.RequireClaim("def@gmail.com")));
}

void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}