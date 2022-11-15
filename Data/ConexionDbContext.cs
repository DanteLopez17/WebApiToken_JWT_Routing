using Microsoft.EntityFrameworkCore;
using WebApiTokenJWTRouting21122.Models;

namespace WebApiTokenJWTRouting21122.Data
{
    public class ConexionDbContext : DbContext
    {
        //Instanciar IConfiguration
        protected readonly IConfiguration _configuracion;
        //Constructor
        public ConexionDbContext(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        //sobreescribir metodo OnConfiguring paara hacer la conexion a la BD
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuracion.GetConnectionString("MiConexion"));
        }
        //DbSet
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Login> Login { get; set; }
    }
}
