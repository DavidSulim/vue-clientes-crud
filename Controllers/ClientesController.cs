using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientesApi.Models;

namespace ClientesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ClientesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            var list = await _db.Clientes.AsNoTracking().ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            var cliente = await _db.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Create([FromBody] Cliente model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Clientes.Add(model);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException != null)
            {
                return Conflict(new { error = "Correo ya registrado" });
            }

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Cliente model)
        {
            if (id != model.Id) return BadRequest(new { error = "Id en ruta y body no coinciden" });
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exists = await _db.Clientes.AsNoTracking().AnyAsync(c => c.Id == id);
            if (!exists) return NotFound();

            _db.Entry(model).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException != null)
            {
                return Conflict(new { error = "Correo ya registrado" });
            }

            return NoContent();
        }

        [HttpDelete("{id:int}"]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _db.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            _db.Clientes.Remove(cliente);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
