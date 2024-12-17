using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(CategoryDto categoryDto);

        //Task<Category> CreateCategoryAsync(Category category);
        Task<IEnumerable<CategoryDto>> GetAllAsync(
            string? query,
            string? sortBy,
            string? sortDirection,
            int? pageNumber,
            int? pageSize
            );

        Task<CategoryDto?> GetById(Guid id);

        Task<Category?> GetCategoryById(Guid id);

        Task<CategoryDto?> UpdateAsync(CategoryDto categoryDto);

        Task<CategoryDto?> DeleteAsync(Guid id);

        Task<int> GetCount();
    }
}
