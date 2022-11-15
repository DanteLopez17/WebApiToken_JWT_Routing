using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiTokenJWTRouting21122.Data;
using WebApiTokenJWTRouting21122.Models;

namespace WebApiTokenJWTRouting21122.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //Instanciar Context
        protected readonly ConexionDbContext _contexto;
        //Constructor
        public LoginController(ConexionDbContext contexto)
        {
            _contexto = contexto;
        }
        //Metodo POST Login con Route
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            //si login viene vacio, devuelve un BadRequest
            if(login is null)
            {
                return BadRequest();
            }

            var listaUsuarios = _contexto.Login.ToList<Login>();
            var listaUsuarios2 = _contexto.Login.ToList();

            Console.WriteLine("Lista 1: " + listaUsuarios);
            Console.WriteLine("Lista 2: " + listaUsuarios2);

            //Si login esta en la lista genera un JWT y devuelve un 200
            foreach(var user in listaUsuarios)
            {
                if (login.Usuario == user.Usuario && login.Constrasenia == user.Constrasenia)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfiguracionManager.AppSetting["JWT:SigningKey"]));

                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOptions = new JwtSecurityToken(issuer: ConfiguracionManager.AppSetting["JWT:Issuer"],
                        audience: ConfiguracionManager.AppSetting["JWT:Audience"],
                        claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(7200), signingCredentials: signinCredentials);

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    return Ok(new
                    {
                        Token = tokenString
                    });
                }
            }
            //Si login no se encuentra en la BD devuelve un Unauthorized
            return Unauthorized();
        }
    }
}
