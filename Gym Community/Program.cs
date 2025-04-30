using System.Text;
using Amazon.Runtime;
using Amazon.S3;
using AutoMapper;
using EmailServices;
using Gym_Community.API.Mapping;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Client;
using Gym_Community.Application.Interfaces.CoachStuff;
using Gym_Community.Application.Interfaces.Forum;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Application.Services;
using Gym_Community.Application.Services.Client;
using Gym_Community.Application.Services.CoachStuff;
using Gym_Community.Application.Services.E_comm;
using Gym_Community.Application.Services.Forum;
using Gym_Community.Application.Services.Gym;
using Gym_Community.Domain.Models;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces;
using Gym_Community.Infrastructure.Interfaces.Client;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Gym_Community.Infrastructure.Interfaces.Forum;
using Gym_Community.Infrastructure.Interfaces.Gym;
using Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Gym_Community.Infrastructure.Repositories;
using Gym_Community.Infrastructure.Repositories.Client;
using Gym_Community.Infrastructure.Repositories.CoachStuff;
using Gym_Community.Infrastructure.Repositories.ECommerce;
using Gym_Community.Infrastructure.Repositories.Forum;
using Gym_Community.Infrastructure.Repositories.Gym;
using Gym_Community.Infrastructure.Repositories.Meals_and_Exercise;
using Gym_Community.Infrastructure.Repositories.Training_Plans;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace Gym_Community
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            //add dbcontext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //add identity
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };

                // Add Google and Facebook authentication
            }).AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "542482302983-oeddeor9j8rirdjnf99oe2um6sucgi58.apps.googleusercontent.com";
                googleOptions.ClientSecret = "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";
                googleOptions.CallbackPath = new PathString("/signin-google");
            })
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "FACEBOOK_APP_ID";
                facebookOptions.AppSecret = "FACEBOOK_APP_SECRET";
            });






            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(TrainingPlanProfile)); // Option 2: scan where TrainingPlanProfile lives
            builder.Services.AddAutoMapper(typeof(ClientProfileMapper)); 
   


            // Aws s3
            builder.Services.AddSingleton<AWSCredentials>(sp =>
            new BasicAWSCredentials(
            builder.Configuration["AWS:AccessKey"],
            builder.Configuration["AWS:SecretKey"]
            ));

            builder.Services.AddSingleton<IAmazonS3>(sp =>
                new AmazonS3Client(
                    sp.GetRequiredService<AWSCredentials>(),
                    Amazon.RegionEndpoint.GetBySystemName(builder.Configuration["AWS:Region"])
                )
            );

            // Add services to the container.

            //Services  
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAwsService, AwsService>();

            //client service
            builder.Services.AddScoped<IClientInfoService, ClientInfoService>();
            builder.Services.AddScoped<IClientProfileService, ClientProfileService>();

            //Email service
            builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
            builder.Services.AddScoped<IEmailService, EmailService>();

            //Fourm Service
            builder.Services.AddScoped<ISubService, SubService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IVoteService, VoteService>();

            //Gym service
            builder.Services.AddScoped<IGymService, GymService>();
            builder.Services.AddScoped<IGymCoachService, GymCoachService>();
            builder.Services.AddScoped<IGymImgService, GymImgService>();
            builder.Services.AddScoped<IGymPlanService, GymPlanService>();
            builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();


            //Ecommerce Repository
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemsRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
            builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IShippingRepository, ShippingRepository>();


            //Client repository
            builder.Services.AddScoped<IClientInfoRepository, ClientInfoRepository>();

            // Ecommerce Service
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IWishlistService, WishlistService>();
            builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
            builder.Services.AddScoped<IShoppingCartItemService, ShoppingCartItemService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IShippingService, ShippingService>();

            //Forum Repository
            builder.Services.AddScoped<ISubRepository, SubRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IVoteRepository, VoteRepository>();

            //Gym Repository
            builder.Services.AddScoped<IGymRepository, GymRepository>();
            builder.Services.AddScoped<IGymCoachRepository, GymCoachRepository>();
            builder.Services.AddScoped<IGymImgRepository, GymImgRepository>();
            builder.Services.AddScoped<IGymPlanRepository, GymPlanRepository>();
            builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
            builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();


            builder.Services.AddScoped<IDailyPlanRepository, DailyPlanRepository>();
            builder.Services.AddScoped<IWeekPlanRepository, WeekPlanRepository>();
            builder.Services.AddScoped<ITrainingPlanRepository, TrainingPlanRepository>();

            builder.Services.AddScoped<IMealRepository, MealRepository>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
            builder.Services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();




            // CoachstuffRepo
            builder.Services.AddScoped<IWorkSampleRepository, WorkSampleRepository>();
            builder.Services.AddScoped<ICoachRatingRepository, CoachRatingRepository>();
            builder.Services.AddScoped<ICoachCertificateRepository, CoachCertificateRepository>();
            builder.Services.AddScoped<ICoachPortfolioRepository, CoachPortfolioRepository>();

            // CoachStuffServices
            builder.Services.AddScoped<IWorkSampleService, WorkSampleService>();
            builder.Services.AddScoped<ICoachRatingService, CoachRatingService>();
            builder.Services.AddScoped<ICoachCertificateService, CoachCertificateService>();
            builder.Services.AddScoped<ICoachPortfolioService, CoachPortfolioService>();



            //life time for all tokens (email confirmation , pass reset, etc..)
            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();


            //serialization 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
