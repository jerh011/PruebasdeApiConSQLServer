using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Portafolio.Modelos;

namespace Portafolio.Datos
{
 

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            var idParam = new SqlParameter("@Id", id);
            var productos = await Productos
                .FromSqlRaw("EXEC ProductoByID @Id", idParam)
                .AsNoTracking() // Añade AsNoTracking para mejorar el rendimiento
                .ToListAsync(); // Convierte los resultados a una lista en el lado del cliente

            var producto = productos.FirstOrDefault(); // Obtiene el primer elemento de la lista
            return producto;
        }

        public async Task<bool> RegistrarProducto(Producto producto)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC InsertarProducto @Nombre, @Descripcion, @Precio",
                new SqlParameter("@Nombre", producto.Nombre),
                new SqlParameter("@Descripcion", producto.Descripcion),
                new SqlParameter("@Precio", producto.Precio)
            );

            // Verifica si el resultado es mayor a 0, lo que indica que una fila fue afectada
            return result > 0;
        }


    }

}