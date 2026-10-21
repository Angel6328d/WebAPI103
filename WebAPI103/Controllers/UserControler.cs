using Domain.dto;
using WebAPI103.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI103.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _usuarioService.ObtenerUsuario();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ById(int id)
        {
            var response = await _usuarioService.ById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioRequestDto usuario)
        {
            var result = await _usuarioService.Create(usuario);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Actualizar usuario
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UsuarioRequestDto usuario)
        {
            var response = await _usuarioService.Update(id, usuario);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // Eliminar usuario
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _usuarioService.Delete(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
    }
}