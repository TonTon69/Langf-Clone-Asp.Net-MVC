using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace langfvn.Models
{
    public partial class LangfvnContext : DbContext
    {
        public LangfvnContext()
            : base("name=LangfvnContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccPer> AccPers { get; set; }
        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<CategoryFood> CategoryFoods { get; set; }
        public virtual DbSet<CategoryNew> CategoryNews { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<KindOfFood> KindOfFoods { get; set; }
        public virtual DbSet<KindOfNew> KindOfNews { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionDetail> PermissionDetails { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.AccPers)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.News)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Advertisement>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryFood>()
                .HasMany(e => e.KindOfFoods)
                .WithRequired(e => e.CategoryFood)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoryNew>()
                .HasMany(e => e.KindOfNews)
                .WithRequired(e => e.CategoryNew)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .Property(e => e.FoodPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Food>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<KindOfFood>()
                .HasMany(e => e.Foods)
                .WithRequired(e => e.KindOfFood)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KindOfNew>()
                .HasMany(e => e.News)
                .WithRequired(e => e.KindOfNew)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.AccPers)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.PermissionDetails)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PermissionDetail>()
                .Property(e => e.Code_Action)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .HasMany(e => e.Stores)
                .WithRequired(e => e.Place)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.KindOfFoods)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);
        }
    }
}