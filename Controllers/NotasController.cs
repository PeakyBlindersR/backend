using Microsoft.AspNetCore.Mvc;
using Notas.Models;
using Notas.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/[Controller]")]
[ApiController]

public class NotasController : ControllerBase
{
    private readonly BaseContext _context;

    public NotasController(BaseContext context)
    {
        _context = context;
    }

    //listar notas
    [HttpGet]
    public ActionResult<IEnumerable<Nota>> GetNotas()
    {
        return _context.Notas.ToList();
    }

    //crear nota
    /* [HttpPost]
    public ActionResult<Nota> PostNota(Nota nota)
    {
        _context.Notas.Add(nota);
        _context.SaveChanges();
        return CreatedAtAction("GetNotas", new { id = nota.Id }, nota);
    } */
    [HttpPost]
    public ActionResult<Nota> PostNota(Nota nota)
    {
        try
        {
            _context.Notas.Add(nota);
            _context.SaveChanges();
            return CreatedAtAction("GetNotas", new { id = nota.Id }, nota);
        }
        catch (Exception ex)
        {
            if (ex is DbUpdateException)
            {
                ModelState.AddModelError("", "Error al guardar la nota.");
                return BadRequest(ModelState);
            }
            else
            {
                ModelState.AddModelError("", "Ocurrió un error inesperado.");
                return BadRequest(ModelState);
            }
        }
    }

    //Eliminar nota
    [HttpDelete("{id}")]
    public ActionResult<Nota> DeleteNota(int id)
    {
        var nota = _context.Notas.Find(id);
        if (nota == null)
        {
            return NotFound();
        }
        _context.Notas.Remove(nota);
        _context.SaveChanges();
        return nota;
    }

    private bool NotaExists(int id)
    {
        return _context.Notas.Any(n => n.Id == id);
    }

    //actualizar nota
    [HttpPut("{id}")]
    public async Task<ActionResult<Nota>> UpdateNota(int id, [FromBody] Nota nota)
    {
        // Validate the incoming nota data (optional)

        // Check if the note exists
        var existingNota = await _context.Notas.FindAsync(id);
        if (existingNota == null)
        {
            return NotFound();
        }

        // Update existing note properties
        existingNota.Title = nota.Title; // Update other properties as needed
        _context.Notas.Update(existingNota); // Mark the entity as modified

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NotaExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(existingNota); // Return the updated note
    }


}
