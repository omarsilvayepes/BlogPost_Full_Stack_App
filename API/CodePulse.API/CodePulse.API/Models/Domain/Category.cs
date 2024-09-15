using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodePulse.API.Models.Domain
{
    public class Category
    {
        //public Category()
        //{
        //    BlogPostCategories = new HashSet<BlogPostCategory>();
        //}
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        // Navigation property for the many-to-many relationship
        //public ICollection<BlogPostCategory> BlogPostCategories { get; set; }

        //[NotMapped]
        public ICollection<BlogPost> BlogPosts { get; set; }//= new List<BlogPost>();
    }
}
