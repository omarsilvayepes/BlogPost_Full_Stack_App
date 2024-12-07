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


        public async Task<IEnumerable<CategoryDto>> GetAllAsync(
            string? query=null,
            string? sortBy=null,
            string? sortDirection=null,
            int? pageNumber=1,
            int? pageSize=100
            )
        {

            var categories =dbContext.Categories.AsQueryable();

            //filtering

            if (!string.IsNullOrWhiteSpace(query))
            {
                categories = categories.Where(c => c.Name.Contains(query));
            }

            //Sorting

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc=string.Equals(sortDirection,"asc", StringComparison.OrdinalIgnoreCase)? true:false;

                    categories=isAsc?categories.OrderBy(c=> c.Name):categories.OrderByDescending(c=>c.Name);
                }

                if (string.Equals(sortBy, "URL", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(c => c.UrlHandle) : categories.OrderByDescending(c => c.UrlHandle);
                }
            }

            //Pagination

            //PageNumber 1 pagesize 5- skip 0, take 5
            //PageNumber 2 pagesize 5- skip 5, take 5
            //PageNumber 3 pagesize 5- skip 10, take 5

            var skipRresults = (pageNumber - 1) * pageSize;
            categories = categories.Skip(skipRresults ?? 0).Take(pageSize ?? 100);

            return mapper.Map<IEnumerable<CategoryDto>>(await categories.ToListAsync());
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
