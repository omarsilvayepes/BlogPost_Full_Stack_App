using Azure.Core;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;
        public BlogPostsController(IBlogPostRepository blogPostRepository,ICategoryRepository categoryRepository)
        {
            this.blogPostRepository=blogPostRepository;
            this.categoryRepository=categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostRequestDto request)
        {
            /*Important :sometimes Automapper can cause issues with EF Core ,i.e tracker issue or duplicated key value
             * so ,for those problems in this functionality it doesn't use Automapper
             * */



            //var blogPostDto = new BlogPostDto
            //{
            //    Author = request.Author,
            //    Content = request.Content,
            //    FeaturedImageUrl = request.FeaturedImageUrl,
            //    IsVisible = request.IsVisible,
            //    PublishDate = request.PublishDate,
            //    ShortDescription = request.ShortDescription,
            //    Title = request.Title,
            //    UrlHandle = request.UrlHandle,
            //    Categories = request.Categories
            //};

            //await blogPostRepository.CreateAsync(blogPostDto);
            //return Ok(blogPostDto);

            //Convert Dto to Domain

            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishDate = request.PublishDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetCategoryById(categoryGuid);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }



            blogPost = await blogPostRepository.CreateBlogPostAsync(blogPost);

            //Convert Domain Model to DTO

            var response = new BlogPostDto
            {
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPostDtos = await blogPostRepository.GetAllAsync();
            if (blogPostDtos is null)
            {
                return NotFound();
            }
            return Ok(blogPostDtos);
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPostDto = await blogPostRepository.GetByIdAsync(id);

            if (blogPostDto is null)
            {
                return NotFound();
            }
            return Ok(blogPostDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBlogPostById([FromRoute] Guid id)
        {
            var blogPostDto = await blogPostRepository.DeleteByIdAsync(id);

            if (blogPostDto is null)
            {
                return NotFound();
            }
            return Ok(blogPostDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id,UpdateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Id= id,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishDate = request.PublishDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            //Add from DB categoreies

            foreach (var categoryId in request.Categories)
            {
                var existingCategory = await categoryRepository.GetCategoryById(categoryId);
                if(existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }


            blogPost = await blogPostRepository.UpdateBlogPostAsync(id,blogPost);

            if (blogPost is null)
            {
                return NotFound();
            }

            //Convert Domain Model to DTO

            var blogPostDto = new BlogPostDto
            {
                Id= blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(blogPostDto);
        }
    }
}
