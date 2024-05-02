using Microsoft.AspNetCore.Mvc;
using Notas.Models;
using Notas.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/[Notas]")]
[ApiController]

public class NotasController : ControllerBase
{
    private readonly BaseContext _context;

    public NotasController(BaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Nota>>> GetNotas()
    {
        return await _context.Notas.ToListAsync();
    }

}
