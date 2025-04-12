using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Domain.Data.Models.E_comms;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.Domain.Data.Models.System_Plans;
using Gym_Community.Domain.Models;
using Gym_Community.Domain.Models.ClientStuff;
using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Domain.Models.Forum;
using Gym_Community.Domain.Models.Gym;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gym_Community.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        //Client Stuff
        public DbSet<ClientInfo> ClientInfo { get; set; }
        //Coach Plans
        public DbSet<ClientPlan> ClientPlans { get; set; }
      
        //Coach Stuff
        public DbSet<CoachCertificate> CoachCertificates { get; set; }
        public DbSet<CoachPortfolio> CoachPortfolios { get; set; }
        public DbSet<CoachRating> CoachRatings { get; set; }
        public DbSet<WorkSample> WorkSamples { get; set; }
        //E-Commerce
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Wishlist> WhishLists { get; set; }
        //Forum
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Sub> Subs { get; set; }
        public DbSet<Vote> Votes { get; set; }
        //Meals and Exercise
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        //Payment and shipping
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        //System Plans
        public DbSet<StaticDailyExercise> StaticDailyExercises { get; set; }
        public DbSet<StaticDailyMeal> StaticDailyMeals { get; set; }
        //public DbSet<StaticDailyPlan> StaticDailyPlans { get; set; }
        public DbSet<StaticPlan> StaticPlans { get; set; }
        public DbSet<StaticWorkoutDay> StaticWorkoutDays { get; set; }
        //Gym
        public DbSet<Gym> Gym { get; set; }
        public DbSet<GymPlan> GymPlans { get; set; }
        public DbSet<GymCoach> GymCoaches { get; set; }
        public DbSet<GymImgs> GymImgs { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Ignore<CultureInfo>(); // Ignore CultureInfo globally

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", ConcurrencyStamp = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", ConcurrencyStamp = "2", Name = "Coach", NormalizedName = "COACH" },
                new IdentityRole { Id = "3", ConcurrencyStamp = "3", Name = "Client", NormalizedName = "CLIENT" },
                new IdentityRole { Id = "4", ConcurrencyStamp = "4", Name = "GymOwner", NormalizedName = "GYMOWNER" }
            );
        }
    }
}   
