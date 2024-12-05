    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Portafolio.Datos;
    using Portafolio.Modelos;

namespace Portafolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Producto producto)
        {
            var agregado = await _context.RegistrarProducto(producto);
            if (agregado == false)
                return NotFound();
            else 
            return Ok(producto);
        }
        [HttpGet("{id}")] 
        public async Task<IActionResult> GetProduct(int id) 
        { 
            var producto = await _context.GetProductoByIdAsync(id); 
            if (producto == null) 
                return NotFound();
            return Ok(producto); 
        }
    }
}