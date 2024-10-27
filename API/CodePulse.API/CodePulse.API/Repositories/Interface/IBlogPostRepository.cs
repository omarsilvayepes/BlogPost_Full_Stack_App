using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        //Task<BlogPost> CreateAsync(BlogPostDto blogPostDto);

        Task<BlogPost> CreateBlogPostAsync(BlogPost blogPostDto);

        Task<BlogPost?> UpdateBlogPostAsync(Guid id, BlogPost blogPost);
        Task<IEnumerable<BlogPostDto>> GetAllAsync();

        Task<BlogPostDto?> GetByIdAsync(Guid id);
        Task<BlogPostDto?> GetByUrlHandleAsync(string urlHandle);
        Task<BlogPostDto?> DeleteByIdAsync(Guid id);
        //Task<BlogPostDto?> UpdateAsync(Guid id, BlogPostDto blogPostDto);
    }
}
