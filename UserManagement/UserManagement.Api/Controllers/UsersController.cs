using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Entities;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Users.ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User u)
        {
            _context.Users.Add(u);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = u.Id }, u);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, User u)
        {
            if (id != u.Id) return BadRequest();
            _context.Entry(u).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _context.Users.FindAsync(id);
            if (u == null) return NotFound();
            _context.Users.Remove(u);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
