using E_Commerce_Project.Context;
using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.Repositories
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Product product)
        {
            await _context.Products.AddAsync(product);
           await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var isPresent = await _context.Products.FindAsync(id);
            if (isPresent != null)
            {
                _context.Products.Remove(isPresent);
              await  _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
           var prod =  await _context.Products.ToListAsync();
            return prod;
        }

        public async Task<Product> GetById(int id)
        {
            var orderById = await _context.Products.FindAsync(id);

            return orderById;
            
        }

        public async Task Update(Product product)
        {
            var value = await _context.Products.FindAsync(product.Id);

            if(value!= null)
            {
                value.Name = product.Name;
                value.Description = product.Description;
                await _context.SaveChangesAsync();
            }
        }

    }
}
