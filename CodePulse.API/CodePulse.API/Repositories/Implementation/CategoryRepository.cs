using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var result = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(result is null)
            {
                return null;
            }

            context.Categories.Remove(result);
            await context.SaveChangesAsync();

            return result;
             
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var response =  await context.Categories.ToListAsync();
            return response;

        }

        public async Task<Category?> GetById(Guid id)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var result = await context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if(result != null)
            {
                //this will update all the values 
                context.Entry(result).CurrentValues.SetValues(category);
                await context.SaveChangesAsync();
                return category;
            }

            return null;

        }
    }
}
