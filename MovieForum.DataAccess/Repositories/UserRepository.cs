using Microsoft.EntityFrameworkCore;
using MovieForum.Data;
using MovieForum.Data.Entities;
using MovieForum.Data.Interfaces;

namespace MovieForum.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserEntity> GetByIdAsync(Guid id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        return user;
    }

    public async Task<IEnumerable<UserEntity>> GetByUsernameAsync(string username)
    {
        var users = await _context.Users.Where(u => u.Username.Contains(username)).ToListAsync();
        return users; 
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<Guid> AddAsync(UserEntity user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id; 
    }

    public async Task<bool> UpdateAsync(UserEntity user)
    {
        var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (userToUpdate == null)
        {
            return false;
        }
        
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (userToDelete == null)
        {
            return false;
        }
        
        _context.Users.Remove(userToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}