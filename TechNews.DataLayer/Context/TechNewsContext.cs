using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechNews.DataLayer.Entities;
using TechNews.Utility.Generators;
using TechNews.Utility.Security;

namespace TechNews.DataLayer.Context
{
    public class TechNewsContext : DbContext
    {
        public TechNewsContext(DbContextOptions<TechNewsContext> options) : base(options)
        {

        }

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Browser> Browsers { get; set; }
        public DbSet<BlackList> BlackList { get; set; }


        #endregion

        #region Ticket

        public DbSet<Ticket> Tickets { get; set; }

        #endregion

        #region Category

        public DbSet<Category> Categories { get; set; }
        public DbSet<SelectedCategory> SelectedCategories { get; set; }

        #endregion

        #region Tag

        public DbSet<Tag> Tags { get; set; }

        #endregion

        #region Review

        public DbSet<Review> Reviews { get; set; }

        #endregion

        #region Items

        public DbSet<Item> Items { get; set; }

        #endregion

        #region Newsletter

        public DbSet<Newsletter> Newsletter { get; set; }

        #endregion

        #region Slider

        public DbSet<Slider> Slider { get; set; }

        #endregion

        #region Email

        public DbSet<SentEmails> Emails { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region SeedData

            //Seeding User Data
            modelBuilder.Entity<User>().HasData(new User
            {
                EmailAddress = "pzix886@gmail.com",
                IdentifierCode = StringGenerator.GenerateUniqueString(),
                IpAddress = "192.168.0.1",
                IsActive = true,
                PhoneNumber = "09120000000",
                ProfilePhoto = "Default.png",
                RegisterDate = DateTime.Now,
                UserName = "محمد ساجدی",
                RoleId = 3,
                UserId = 1,
                Password = PasswordHelper.EncodePasswordMd5("123")
            });

            //Seeding Roles Data
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                RoleId = 1,
                RoleName = "User"
            }, new Role()
            {
                RoleId = 2,
                RoleName = "Author"
            }, new Role()
            {
                RoleId = 3,
                RoleName = "Admin"
            });

            //Seeding Category Data
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                CategoryId = 1,
                CategoryTitle = "اخبار ویدیو گیم"
            }, new Category()
            {
                CategoryId = 2,
                CategoryTitle = "اخبار فیلم و سریال"
            }, new Category()
            {
                CategoryId = 3,
                CategoryTitle = "اخبار تکنولوژی"
            }, new Category()
            {
                CategoryId = 4,
                CategoryTitle = "رویداد ها"
            }, new Category()
            {
                CategoryId = 5,
                CategoryTitle = "بررسی بازی"
            }, new Category()
            {
                CategoryId = 6,
                CategoryTitle = "بررسی سخت افزار"
            }, new Category()
            {
                CategoryId = 7,
                CategoryTitle = "بررسی فیلم و سریال"
            }, new Category()
            {
                CategoryId = 8,
                CategoryTitle = "مقالات"
            }, new Category()
            {
                CategoryId = 9,
                CategoryTitle = "پیشنهادی ها"
            });

            #endregion

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
