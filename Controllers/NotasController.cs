using Microsoft.AspNetCore.Mvc;
using Notas.Models;
using Notas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Route("api/[Controller]")]
[ApiController]

public class NotasController : ControllerBase
{
    private readonly BaseContext _context;

    public NotasController(BaseContext context)
    {
        _context = context;
    }

    //LISTAR NOTAS
    [HttpGet]
    public ActionResult<IEnumerable<Nota>> GetNotas()
    {
        return _context.Notas.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Nota> GetNota(int? Id)
    {
        var nota = _context.Notas.FirstOrDefault(m => m.Id == Id);
        return nota;
    }

    //-----------------------------------------------------------------------


    //CREAR NOTA
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

    //-------------------------------------------------------------------------------------------------------

    //ELIMINAR NOTA
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

    //------------------------------------------------------------------------------

    //ACTUALIZAR NOTA
    [HttpPut("{id}")]
    public ActionResult<Nota> PutNota(int id, Nota nota)
    {
        var existingNota = _context.Notas.FirstOrDefault(n => n.Id == id);
        if (existingNota == null)
        {
            return NotFound();
        }

        existingNota.Title = nota.Title;
        existingNota.Content = nota.Content;
        existingNota.Date = nota.Date;

        _context.SaveChanges();

        return nota;
    }

    //BUSCAR NOTA
    /* [HttpGet("{id}")]
    public ActionResult<Nota> Search(string word, DateOnly date)
    {
        var notaEncontrada = _context.Notas.Where(i => i.Title.Contains(word) || i.Content.Contains(word) || i.Date.Contains(date)).ToList();
        return notaEncontrada;
    } */

    [HttpGet("{id}")] // Adjust route if needed
    public ActionResult<IEnumerable<Nota>> Search(string word, DateOnly? date) // Make date nullable
    {
        var query = _context.Notas.AsQueryable(); // Start with a queryable

        // Filter by word (optional)
        if (!string.IsNullOrEmpty(word))
        {
            query = query.Where(i => i.Title.Contains(word) || i.Content.Contains(word));
        }

        // Filter by date (if provided)
        if (date.HasValue)
        {
            query = query.Where(i => i.Date == date.Value);
        }

        return query.ToList(); // Execute the query and return notes
    }


}