using Microsoft.AspNetCore.Mvc;
using Notas.Models;
using Notas.Data;

[Route("api/[Controller]")]
[ApiController]

public class NotasController : ControllerBase
{
    private readonly BaseContext _context;

    public NotasController(BaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Nota>> GetNotas()
    {
        return _context.Notas.ToList();
    }

}
