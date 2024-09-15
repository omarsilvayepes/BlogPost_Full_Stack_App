using AutoMapper;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper Mapper;
        private readonly ICategoryRepository CategoryRepository;

        public BlogPostRepository(ApplicationDbContext dbContext,
            IMapper mapper,
            ICategoryRepository categoryRepository
            )
        {
            DbContext = dbContext;
            Mapper = mapper;
            CategoryRepository = categoryRepository;
        }


        public async Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost)
        {

            await DbContext.BlogPosts.AddAsync(blogPost);
            await DbContext.SaveChangesAsync();
            return blogPost;
        }




        //public async Task<BlogPost> CreateAsync(BlogPostDto blogPostDto)
        //{
        //    //Add The Categories to BlogPost

        //    foreach (var categoryId in blogPostDto.Categories)
        //    {
        //        var existingCategory = await CategoryRepository.GetById(categoryId);
        //        if (existingCategory is not null)
        //        {
        //            blogPostDto.CategoriesList.Add(existingCategory);
        //        }
        //    }

        //    BlogPost blogPost = Mapper.Map<BlogPost>(blogPostDto);

        //    await DbContext.BlogPosts.AddAsync(blogPost);
        //    await DbContext.SaveChangesAsync();
        //    return blogPost;
        //}



        public async Task<IEnumerable<BlogPostDto>> GetAllAsync()
        {
            var blogPosts = await DbContext.BlogPosts.Include(c=>c.Categories).ToListAsync(); //Include it is Join Functinality
            var blogPostDtos = Mapper.Map<IEnumerable<BlogPostDto>>(blogPosts);
            return blogPostDtos;
        }

        public async Task<BlogPostDto?> GetByIdAsync(Guid id)
        {
            var blogPost = await DbContext.BlogPosts.Include(c => c.Categories)//Include it is Join Functinality
                .FirstOrDefaultAsync(b=> b.Id==id); 

            var blogPostDto = Mapper.Map<BlogPostDto>(blogPost);
            return blogPostDto;
        }
    }
}
