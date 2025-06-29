using E_Commerce_Project.Models;

namespace E_Commerce_Project.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
    }

}
