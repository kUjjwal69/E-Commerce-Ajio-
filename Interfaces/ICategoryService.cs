using E_Commerce_Project.Models;

namespace E_Commerce_Project.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(int id);
    }

}
