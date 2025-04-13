using System.Text;
using Amazon.Runtime;
using Amazon.S3;
using AutoMapper;
using EmailServices;
using Gym_Community.API.Mapping;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Forum;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Application.Services;
using Gym_Community.Application.Services.E_comm;
using Gym_Community.Application.Services.Forum;
using Gym_Community.Domain.Models;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Gym_Community.Infrastructure.Interfaces.Forum;
using Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise;
using Gym_Community.Infrastructure.Interfaces.Training_Plans;
using Gym_Community.Infrastructure.Repositories;
using Gym_Community.Infrastructure.Repositories.ECommerce;
using Gym_Community.Infrastructure.Repositories.Forum;
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
            });

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(TrainingPlanProfile)); // Option 2: scan where TrainingPlanProfile lives

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
            builder.Services.AddScoped<ISubService, SubService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IVoteService, VoteService>();

            //Email service
            builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
            builder.Services.AddScoped<IEmailService, EmailService>();


            //Ecommerce Repository
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemsRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            // Also make sure your services are registered if not already:
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            

            //Repo
            builder.Services.AddScoped<ISubRepository, SubRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IVoteRepository, VoteRepository>();
            builder.Services.AddScoped<IDailyPlanRepository, DailyPlanRepository>();
            builder.Services.AddScoped<IWeekPlanRepository, WeekPlanRepository>();
            builder.Services.AddScoped<ITrainingPlanRepository, TrainingPlanRepository>();

            builder.Services.AddScoped<IMealRepository, MealRepository>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
            builder.Services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();

            //life time for all tokens (email confirmation , pass reset, etc..)
            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
