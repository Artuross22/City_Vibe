﻿using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.CacheRepository;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Helpers;
using City_Vibe.Infrastructure.Implement;
using City_Vibe.Infrastructure.Repository;
using City_Vibe.Infrastructure.Services;
using City_Vibe.Services;
using City_Vibe.Services.ApiBulletinBoard;
using City_Vibe.ValidationAttribute.AppUserAttributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WrapperAPI.APIs.CryptoAPI.Interfaces;
using WrapperAPI.APIs.CryptoAPI.Realization;
using WrapperAPI.APIs.WeatherAPI.Interfaces;
using WrapperAPI.APIs.WeatherAPI.Realization;
using WrapperAPI.BaseAPI.Interfaces;

namespace City_Vibe.ExtensionMethod
{
    public static class ServiceCollectionExtensions
    {

       
        public static IServiceCollection AddInfrastructureServices(this
           IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.Configure<AuthMessageSenderOptions>(configuration.GetSection("SendGrid"));  // common pattern

            services.Configure<SettingsKeyAPI>(configuration.GetSection("RapidAPI"));
            
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            return services;
        }
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();

            services.AddScoped<IClubRepository, ClubRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
          
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<ISaveClubRepository, SaveClubRepository>();
            services.AddScoped<ISaveEventRepository, SaveEventRepository>();
            services.AddScoped<IlikeClubRepository, likeClubRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IClubCommentRepository, ClubCommentRepository>();
            return services;
        }

        public static IServiceCollection AddDecoratorPattern(this IServiceCollection services)
        {
            //Scrutor 
            services.Decorate<IAppUserRepository, CacheMemoryUserRepository>();

            services.AddScoped<BulletinBoardService>();

            return services;
        }

        public static IServiceCollection AddAPIDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitedAPIs, UnitedAPIs>();
            services.AddScoped<ICryptoApiAdapter, CryptoApiAdapter>();
            services.AddScoped<IWeatherApiAdapter, WeatherApiAdapter>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICityVibeDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddTransient<ISendGridEmail, SendGridEmail>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPhotoService, PhotoService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;

        }

        public static IServiceCollection AddContractsServices(this IServiceCollection services)
        {

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IClubCommentService, ClubCommentService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;

        }

        public static IServiceCollection AddAttributeServices(this IServiceCollection services)
        {

           services.AddScoped<UserManagerFilterAttribute>();

            return services;
        }
    }
}
