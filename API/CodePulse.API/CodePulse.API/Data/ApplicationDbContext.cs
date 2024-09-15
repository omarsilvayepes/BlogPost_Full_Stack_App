using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }

        //public DbSet<BlogPostCategory> BlogPostCategories { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    //modelBuilder.Entity<BlogPost>()
        //    //    .HasMany(c => c.Categories)
        //    //    .WithMany(a => a.BlogPosts)
        //    //    .UsingEntity(t=>t.ToTable("BloggPostCategoriess"));


        //    // Configure the many-to-many relationship
        //    //modelBuilder.Entity<BlogPostCategory>()
        //    //    .HasKey(bc => new { bc.BlogPostId, bc.CategoryId });

        //    //modelBuilder.Entity<BlogPostCategory>()
        //    //    .HasOne(bc => bc.BlogPost)
        //    //    .WithMany(b => b.BlogPostCategories)
        //    //    .HasForeignKey(bc => bc.BlogPostId);

        //    //modelBuilder.Entity<BlogPostCategory>()
        //    //    .HasOne(bc => bc.Category)
        //    //    .WithMany(c => c.BlogPostCategories)
        //    //    .HasForeignKey(bc => bc.CategoryId);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
