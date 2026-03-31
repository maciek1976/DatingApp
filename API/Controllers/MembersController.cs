using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using API.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //https://localhost:5001/api/members
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();

            return members;
        }

        [HttpGet("{id}")] //https://localhost:5001/api/members/bob-id
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.Users.FindAsync(id);

            if (member == null){
                return NotFound();
            }

            return member;
        }
    }
}