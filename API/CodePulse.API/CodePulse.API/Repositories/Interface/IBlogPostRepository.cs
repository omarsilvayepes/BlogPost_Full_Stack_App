using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        //Task<BlogPost> CreateAsync(BlogPostDto blogPostDto);

        Task<BlogPost> CreateBlogPostAsync(BlogPost blogPostDto);
        Task<IEnumerable<BlogPostDto>> GetAllAsync();

        Task<BlogPostDto?> GetByIdAsync(Guid id);
    }
}
