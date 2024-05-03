namespace Notas.Models;

public class Nota
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateOnly Date { get; set; }
}