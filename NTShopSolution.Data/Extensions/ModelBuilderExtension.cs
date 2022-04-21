using Microsoft.EntityFrameworkCore;
using NTShopSolution.Data.Enum;
using NTShopSolution.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.Data.Extensions
{
    public static class ModelBuilderExtension 
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle", Value = "This is home title of NTShopSolution" },
                new AppConfig() { Key = "HomeKeyword", Value = "This is home keyword of NTShopSolution" },
                new AppConfig() { Key = "HomeDescription", Value = "This is home description of NTShopSolution" }
                );
            modelBuilder.Entity<Language>().HasData
                (
                    new Language() { Id = "vi-VN", Name = "Tiếng Việt", IsDefault = false },
                    new Language() { Id = "en-US", Name = "English", IsDefault = true }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    SortOrder = 1,
                    ParentId = null,
                    Status = Status.Active,
                });
            modelBuilder.Entity<CategoryTranslation>().HasData
                (
                    new CategoryTranslation() { Id = 1, CategoryId = 1, LanguageId = "vi-VN", Name = "Ao Nam", SeoAlias = "abc", SeoDescription = "abc", SeoTitle = "abc" },
                    new CategoryTranslation() { Id = 2, CategoryId = 1, LanguageId = "en-US", Name = "men T-shirt", SeoAlias = "abc", SeoDescription = "abc", SeoTitle = "abc" }
                );
        }
    }
}
