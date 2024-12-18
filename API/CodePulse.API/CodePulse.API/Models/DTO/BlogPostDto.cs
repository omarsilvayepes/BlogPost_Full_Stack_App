﻿namespace CodePulse.API.Models.DTO
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime publishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
        //public Guid[] Categories { get; set; }

        //public ICollection<CategoryDto> CategoriesList { get; set; } = new List<CategoryDto>();
        public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

    }

}
