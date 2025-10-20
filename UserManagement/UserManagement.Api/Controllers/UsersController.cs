using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users
                .FromSqlRaw("EXEC sp_Users_CRUD @Action = 'READ'")
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            var paramAction = new SqlParameter("@Action", "READ");

            var user = await _context.Users
                .FromSqlRaw("EXEC sp_Users_CRUD @Action, @Id", paramAction, paramId)
                .FirstOrDefaultAsync();

            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User u)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "CREATE"),
                new SqlParameter("@Name", u.Name ?? (object)DBNull.Value),
                new SqlParameter("@BirthDate", u.BirthDate),
                new SqlParameter("@Gender", u.Gender ?? (object)DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Users_CRUD @Action, @Name, @BirthDate, @Gender",
                parameters);

            return Ok(new { message = "Usuario creado correctamente" });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, User u)
        {
            if (id != u.Id) return BadRequest("El ID no coincide");

            var parameters = new[]
            {
                new SqlParameter("@Action", "UPDATE"),
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", u.Name ?? (object)DBNull.Value),
                new SqlParameter("@BirthDate", u.BirthDate),
                new SqlParameter("@Gender", u.Gender ?? (object)DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Users_CRUD @Action, @Id, @Name, @BirthDate, @Gender",
                parameters);

            return Ok(new { message = "Usuario actualizado correctamente" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "DELETE"),
                new SqlParameter("@Id", id)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Users_CRUD @Action, @Id",
                parameters);

            return Ok(new { message = "Usuario eliminado correctamente" });
        }
    }
}
