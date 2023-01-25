using City_Vibe.Data;
using City_Vibe.Helpers;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Add services to the container
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(e => e.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<ISaveClubRepository, SaveClubRepository>();
builder.Services.AddScoped<ISaveEventRepository, SaveEventRepository>();

builder.Services.AddTransient<ISendGridEmail, SendGridEmail>();

builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("SendGrid"));  // common pattern
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// builder pattern 
builder.Services.AddAuthentication()

.AddFacebook(options =>
{

    options.AppId = "1153555458865780";
    options.AppSecret = "780def90e529535402b302fdcf9aaa1d";
})
.AddGoogle(options =>
{
    options.ClientId = "584621299652-geanup19loi4anoj4udsfq09dijhs5tu.apps.googleusercontent.com";
    options.ClientSecret = "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";
});


builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 5;
    opt.Password.RequireLowercase = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
    opt.Lockout.MaxFailedAccessAttempts = 5;
    //opt.SignIn.RequireConfirmedAccount = true;
});

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
app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
