using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository iCategoryRepo;

        public CategoriesController(ICategoryRepository iCategoryRepo)
        {
            this.iCategoryRepo = iCategoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await this.iCategoryRepo.GetAllCategories();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await iCategoryRepo.CreateAsync(category);

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var response = await iCategoryRepo.GetById(id);

            if(response is null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryDto
            {
                Id = response.Id,
                Name = response.Name,
                UrlHandle = response.UrlHandle
            };
              
            return Ok(categoryDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDTO updateCategoryRequestDTO)
        {
            var category = new Category
            {
                Id = id,
                Name = updateCategoryRequestDTO.Name,
                UrlHandle = updateCategoryRequestDTO.UrlHandle
            };

            category = await iCategoryRepo.UpdateAsync(category);  

            if(category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(response);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var response = await iCategoryRepo.DeleteAsync(id);

            if(response is null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryDto
            {
                Id = response.Id,
                Name = response.Name,
                UrlHandle = response.UrlHandle,
            };

            return Ok(categoryDTO);

        }

    }
}
