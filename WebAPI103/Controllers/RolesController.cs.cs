using Domain.dto;
using Microsoft.AspNetCore.Mvc;
using WebAPI103.Services.Interfaces;

namespace WebAPI103.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolesController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _rolService.ObtenerRoles());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _rolService.ById(id));

        [HttpPost]
        public async Task<IActionResult> Create(RolRequestDto request) =>
            Ok(await _rolService.Create(request));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RolRequestDto request) =>
            Ok(await _rolService.Update(id, request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            Ok(await _rolService.Delete(id));
    }
}