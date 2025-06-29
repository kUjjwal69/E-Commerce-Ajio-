using E_Commerce_Project.Context;
using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.Repositories
{
    public class UserService:IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _context.Users
                 .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);

            // Optional: if using hashing:
            // var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            // if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password)) return null;

            return user; // will return null if not found
        }

        public async Task<IEnumerable<User>> GetAll()
        {
           var n = await _context.Users.ToListAsync();
            return n;
        }

        public async Task<User> GetById(int id)
        {
            var a = await _context.Users.FindAsync(id);
            return a;
        }

        public async Task<User> Register(User user)
        {
            // Optional: check if email already exists
            var isExist = await _context.Users.AnyAsync(e=>e.Email == user.Email);
            if (isExist)
                throw new Exception("Email already registered.");

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
