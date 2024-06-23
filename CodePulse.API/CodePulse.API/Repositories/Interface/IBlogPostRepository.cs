using CodePulse.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPost();
        Task<BlogPost?> GetBlogPostById(Guid Id);
        Task<BlogPost?> GetBlogPostByHandle(string urlHandle);
        Task<BlogPost?> DeleteBlogPost(Guid Id);
    }
}
