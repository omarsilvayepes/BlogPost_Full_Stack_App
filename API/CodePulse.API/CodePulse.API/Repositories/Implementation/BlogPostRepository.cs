using AutoMapper;
using Azure.Core;
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
       // private readonly ICategoryRepository CategoryRepository;

        public BlogPostRepository(ApplicationDbContext dbContext,
            IMapper mapper
            //ICategoryRepository categoryRepository
            )
        {
            DbContext = dbContext;
            Mapper = mapper;
            //CategoryRepository = categoryRepository;
        }


        public async Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost)
        {

            await DbContext.BlogPosts.AddAsync(blogPost);
            await DbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPostDto?> DeleteByIdAsync(Guid id)
        {
            var blogPost = await DbContext.BlogPosts
                  .FirstOrDefaultAsync(b => b.Id == id);

            if(blogPost is not null)
            {
                DbContext.BlogPosts.Remove(blogPost);
                await DbContext.SaveChangesAsync();
                return Mapper.Map<BlogPostDto>(blogPost);
            }

            return null;
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

        public async Task<BlogPost?> UpdateBlogPostAsync(Guid id, BlogPost blogPost)
        {
            var existingBlog = await DbContext.BlogPosts.Include(c => c.Categories)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (existingBlog is not null)
            {

                //Update  BlogPost
                DbContext.Entry(existingBlog).CurrentValues
                    .SetValues(blogPost);

                //Update Categories

                existingBlog.Categories = blogPost.Categories.ToList();

                await DbContext.SaveChangesAsync();

                return blogPost;
            }
            return null;
        }

        //public async Task<BlogPostDto?> UpdateAsync(Guid id,BlogPostDto blogPostDto)
        //{
        //    var existingBlog = await DbContext.BlogPosts.Include(c=>c.Categories)
        //        //.AsNoTracking()
        //        .FirstOrDefaultAsync(b => b.Id == id);
        //    if (existingBlog is not null)
        //    {

        //        //Update  BlogPost
        //        DbContext.Entry(existingBlog).CurrentValues
        //            .SetValues(Mapper.Map<BlogPost>(blogPostDto));

        //        //Update Categories

        //        existingBlog.Categories = Mapper.Map<IEnumerable<Category>>(blogPostDto.Categories).ToList();

        //        //DbContext.BlogPosts.Update(existingBlog);
        //        await DbContext.SaveChangesAsync();

        //        return blogPostDto;
        //    }
        //    return null;
        //}
    }
}
