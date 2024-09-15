using AutoMapper;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public CategoryRepository(ApplicationDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }



        public async Task<Category> CreateAsync(CategoryDto categoryDto)
        {

            Category category=mapper.Map<Category>(categoryDto);

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }


        //public async Task<Category> CreateCategoryAsync(Category category)
        //{
        //    await dbContext.Categories.AddAsync(category);
        //    await dbContext.SaveChangesAsync();
        //    return category;
        //}


        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories=await dbContext.Categories.ToListAsync();
            var categoriesDto=mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }


        public async Task<Category?> GetCategoryById(Guid id)
        {
            Category category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is not null)
            {
                return category;
            }
            return null;
        }

        public async Task<CategoryDto?> GetById(Guid id)
        {
             Category category =await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is not null)
            {
               return mapper.Map<CategoryDto>(category);
            }
            return null;
        }

        public async Task<CategoryDto?> UpdateAsync(CategoryDto categoryDto)
        {
           var existingCategory= await dbContext.Categories.FirstOrDefaultAsync(c=>c.Id==categoryDto.Id);
            if(existingCategory is not null)
            {
                //Update  Category
                dbContext.Entry(existingCategory).CurrentValues
                    .SetValues(mapper.Map<Category>(categoryDto));
                await dbContext.SaveChangesAsync();
                return categoryDto;
            }
            return null;
        }

        public async Task<CategoryDto?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCategory is not null)
            {
                //Delete  Category
                dbContext.Categories.Remove(existingCategory);
                await dbContext.SaveChangesAsync();

                return mapper.Map<CategoryDto>(existingCategory);
            }
            return null;
        }

    }
}
