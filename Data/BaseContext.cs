using Microsoft.EntityFrameworkCore;
using Notas.Models;

namespace Notas.Data;

public class BaseContext : DbContext
{
    public BaseContext(DbContextOptions<BaseContext> options) : base(options){

    }
    public DbSet<Nota> Notas { get; set; }
}