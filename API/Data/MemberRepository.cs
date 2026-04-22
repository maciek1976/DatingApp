using Api.Interfaces;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MemberRepository(AppDbContext context) : IMemberRepository
{
    private readonly AppDbContext _context = context;

    public void Update(Member member)
    {
        _context.Entry(member).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;      
    }

    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
        return await _context.Members.ToListAsync();
    }

    public async Task<Member?> GetMemberByIdAsync(string id)
    {
        return await _context.Members
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(string memberId)
    {
        return await _context.Members
            .Where(m => m.Id == memberId)
            .SelectMany(m => m.Photos)
            .ToListAsync();
    }

    public async Task<Member?> GetMemberFoUpdatesAsync(string id)
    {
        return await _context.Members.FindAsync(id);
    }
}