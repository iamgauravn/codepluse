using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        ApplicationDbContext context;
        public BlogPostRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await context.BlogPosts.AddAsync(blogPost);
            await context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogPost(Guid Id)
        {
            var result = await context.BlogPosts.FirstOrDefaultAsync(x => x.Id == Id);
            if(result is null)
            {
                return null;
            }

            context.BlogPosts.Remove(result);
            await context.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPost()
        {
            var result = await context.BlogPosts.ToListAsync();
            return result;

        }

        public async Task<BlogPost?> GetBlogPostById(Guid Id)
        {
            var result = await context.BlogPosts.FirstOrDefaultAsync(x => x.Id == Id);
            if(result is null)
            {
                return null;
            }
            return result;
        }

        public async Task<BlogPost?> GetBlogPostByHandle(string urlHandle)
        {
            var result = await context.BlogPosts.FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
            if(result is null)
            {
                return null;
            }
            return result;
        }
 
    }
}
