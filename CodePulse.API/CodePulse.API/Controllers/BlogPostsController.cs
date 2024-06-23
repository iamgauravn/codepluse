using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {

        IBlogPostRepository iBlogPostRepo;
        ICategoryRepository iCategoryRepo;

        public BlogPostsController(IBlogPostRepository iBlogPostRepo, ICategoryRepository iCategoryRepo)
        {
            this.iBlogPostRepo = iBlogPostRepo;
            this.iCategoryRepo = iCategoryRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDTO createBlogPostRequestDTO)
        {
            var blogPost = new BlogPost
            {
                Title = createBlogPostRequestDTO.Title,
                Author = createBlogPostRequestDTO.Author,
                Content = createBlogPostRequestDTO.Content,
                FeaturedImageUrl = createBlogPostRequestDTO.FeaturedImageUrl,
                IsVisible = createBlogPostRequestDTO.IsVisible,
                PublishedDate = createBlogPostRequestDTO.PublishedDate,
                ShortDescription = createBlogPostRequestDTO.ShortDescription,
                UrlHandle = createBlogPostRequestDTO.UrlHandle,
                Categories = new List<Category>()
            };

            foreach(var item in createBlogPostRequestDTO.Categories)
            {
                var existingCategory = await iCategoryRepo.GetById(item);
                if(existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            blogPost = await iBlogPostRepo.CreateAsync(blogPost);

            var blogPostDTO = new
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                CategoryDtos = blogPost.Categories.Select(x=> new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(blogPostDTO);

        }

        [HttpGet]
        public async Task<IActionResult> GetBlogPost()
        {
            var result = await iBlogPostRepo.GetAllBlogPost();
            return Ok(result);
        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            var result = await iBlogPostRepo.GetBlogPostByHandle(urlHandle);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid Id)
        {
            var result = await iBlogPostRepo.GetBlogPostById(Id);
            if(result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost(Guid Id)
        {
            var result = await iBlogPostRepo.DeleteBlogPost(Id);
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
