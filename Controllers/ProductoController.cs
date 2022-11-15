using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTokenJWTRouting21122.Data;
using WebApiTokenJWTRouting21122.Models;

namespace WebApiTokenJWTRouting21122.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize] // No se puede acceder sin autenticacion
    public class ProductoController : Controller
    {
        //Instanciar contexto
        protected readonly ConexionDbContext _contexto;
        //Constructor
        public ProductoController(ConexionDbContext contexto)
        {
            _contexto = contexto;
        }
        //Read Async
        [HttpGet]
        [Route("ListadoProductos")]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            var listaProductos = await _contexto.Producto.ToListAsync();
            return listaProductos;
        }
        //ReadByAsync
        [HttpGet]
        [Route("VerProducto")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var producto = await _contexto.Producto.FindAsync(id);
            return producto;
        }
        //Create Async
        [HttpPost]
        [Route("CrearProducto")]
        public async Task<ActionResult<Producto>> CreateProductAsync(Producto producto)
        {
            //add POSIBLE ERROR EN EL ADDASYNC
            _contexto.Producto.Add(producto);
            //savechanges
            await _contexto.SaveChangesAsync();
            //retornar producto creado
            return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
        }
        //Update Async
        [HttpPut]
        [Route("ActualizarProducto")]
        public async Task<ActionResult<Producto?>> ActualizarProductoAsync(Producto producto)
        {
            if(producto != null)
            {
                //update
                _contexto.Update(producto);
                //savechanges
                await _contexto.SaveChangesAsync();
            }
            
            //retorna producto actualizado
            return await _contexto.Producto.FindAsync(producto.Id);
        }
        //Delete Async: retorna un booleano
        [HttpDelete]
        [Route("BorrarProducto")]
        public async Task<ActionResult<bool>> BorrarProductoAsync(int id)
        {
            //busca el producto
            var producto = await _contexto.Producto.FindAsync(id);
            //Crea una variable para dar una respuesta 200 pero true en caso de borrado y false en caso de no borrado
            bool respuesta;
            //si no es nulo elimina y  retorna true
            if(producto != null)
            {
                _contexto.Producto.Remove(producto);
                await _contexto.SaveChangesAsync();
                respuesta = true;
            }
            //si es nulo retorna false
            else
            {
                respuesta =  false;
            }

            return Ok(new { Respuesta = respuesta });
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
