using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NTShopSolution.Data.Configuration;
using NTShopSolution.Data.Extensions;
using NTShopSolution.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.Data.EF
{
    public class NTShopDbContext : IdentityDbContext
    {
        public NTShopDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInCartConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
            //Identity Configuration AppUser,AppRole
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            //Identity other model:
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x=>new {x.RoleId,x.UserId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x=>x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x=>x.UserId);

            modelBuilder.Seed();



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cart> carts { set; get; }
        public DbSet<Category> categories { set; get; }
        public DbSet<CategoryTranslation> categoryTranslations { set; get; }
        public DbSet<Contact> contacts { set; get; }
        public DbSet<Language> languages { set; get; }
        public DbSet<Order> orders { set; get; }
        public DbSet<OrderDetail> orderDetails { set; get; }
        public DbSet<Product> products { set; get; }
        public DbSet<ProductInCategory> productInCategories { set; get; }
        public DbSet<ProductTranslation> productTranslations { set; get; }
        public DbSet<Promotion> promotions { set; get; }
        public DbSet<Transaction> transactions { set; get; }
        public DbSet<ProductInCart> productInCarts { set; get; }
        public DbSet<ProductImage> productImages { set; get; }

    }
}
