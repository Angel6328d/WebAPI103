using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using WebAPI103.Context;
using WebAPI103.Services.Interfaces;

namespace WebAPI103.Services
{
    public class RolService : IRolService
    {
        private readonly ApplicationDbContext _context;

        public RolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Rol>>> ObtenerRoles()
        {
            var roles = await _context.rols.ToListAsync();

            return new Response<List<Rol>>
            {
                Success = true,
                Result = roles
            };
        }

        public async Task<Response<Rol>> ById(int id)
        {
            var rol = await _context.rols
                .FirstOrDefaultAsync(r => r.PkRol == id);

            if (rol == null)
            {
                return new Response<Rol>(null!, "Rol no encontrado")
                {
                    Success = false
                };
            }

            return new Response<Rol>(rol);
        }

        public async Task<Response<Rol>> Create(RolRequestDto request)
        {
            bool existe = await _context.rols
                .AnyAsync(r => r.Nombre == request.Nombre);

            if (existe)
            {
                return new Response<Rol>(null!, "Ya existe un rol con ese nombre")
                {
                    Success = false
                };
            }

            Rol nuevo = new Rol
            {
                Nombre = request.Nombre,
                Estatus = request.Estatus
            };

            await _context.rols.AddAsync(nuevo);
            await _context.SaveChangesAsync();

            return new Response<Rol>(nuevo, "Rol creado correctamente");
        }

        public async Task<Response<Rol>> Update(int id, RolRequestDto request)
        {
            var rol = await _context.rols
                .FirstOrDefaultAsync(r => r.PkRol == id);

            if (rol == null)
            {
                return new Response<Rol>(null!, "Rol no encontrado")
                {
                    Success = false
                };
            }

            bool nombreEnUso = await _context.rols
                .AnyAsync(r => r.Nombre == request.Nombre && r.PkRol != id);

            if (nombreEnUso)
            {
                return new Response<Rol>(null!, "Ya existe otro rol con ese nombre")
                {
                    Success = false
                };
            }

            rol.Nombre = request.Nombre;
            rol.Estatus = request.Estatus;

            await _context.SaveChangesAsync();

            return new Response<Rol>(rol, "Rol actualizado correctamente");
        }

        public async Task<Response<Rol>> Delete(int id)
        {
            var rol = await _context.rols
                .FirstOrDefaultAsync(r => r.PkRol == id);

            if (rol == null)
            {
                return new Response<Rol>(null!, "Rol no encontrado")
                {
                    Success = false
                };
            }

            bool enUso = await _context.usuarios.AnyAsync(u => u.FkRol == id);

            if (enUso)
            {
                return new Response<Rol>(null!, "No se puede eliminar: hay usuarios con este rol")
                {
                    Success = false
                };
            }

            _context.rols.Remove(rol);
            await _context.SaveChangesAsync();

            return new Response<Rol>(rol, "Rol eliminado correctamente");
        }
    }
}