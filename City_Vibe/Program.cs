using City_Vibe.ExtensionMethod;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationRepositories()
    .AddApplicationServices()
    .AddContractsServices();


//builder pattern 
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
