using E_Commerce_Project.Context;
using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.Repositories
{
    public class CategoryService :ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Category category)
        {
            await _context.Categories.AddAsync(category);
          await  _context.SaveChangesAsync(); 
        }

        public async Task Delete(int id)
        {
         // so what we can do here is that we can find with this id, Is there is any respective category is available??
         var IsCategoryPresent = await _context.Categories.FindAsync(id);

            if (IsCategoryPresent != null)
            {
                 _context.Categories.Remove(IsCategoryPresent);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
          return  await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
           var allCategories =  await _context.Categories.FirstOrDefaultAsync(a=>a.Id == id);
            //_context.SaveChangesAsync();
            return allCategories;
            
        }

        public async Task Update(Category category)
        {
            // Step 1: Find the existing category by Id
            var existingCategory = await _context.Categories.FindAsync(category.Id);

            // Step 2: Check if it exists
            if (existingCategory != null)
            {
                // Step 3: Update its properties
                existingCategory.Name = category.Name;
                existingCategory.Products = category.Products; // if you have this field

                // Step 4: Save changes to database
                await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine($"Category with ID {category.Id} not found.");
                // Optionally throw an exception or return a failure result
            }
        }

    }
}
