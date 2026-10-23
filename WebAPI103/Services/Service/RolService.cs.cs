using Dapper;
using Domain.dto;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
            var roles = await _context.Database.GetDbConnection()
                .QueryAsync<Rol>("spGetRol", commandType: System.Data.CommandType.StoredProcedure);

            return new Response<List<Rol>>
            {
                Success = true,
                Result = roles.ToList()
            };
        }

        public async Task<Response<Rol>> ById(int id)
        {
            var rol = await _context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<Rol>("spGetRolPorId", new {RolID = id}, commandType: System.Data.CommandType.StoredProcedure);

            if (rol == null)
            {
                return new Response<Rol>(null!, "Rol no encontrado")
                {
                    Success = false,
                };
            }

            return new Response<Rol>(rol, "Rol encontrado");
        }

        public async Task<Response<Rol>> Create(RolRequestDto request)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC spCreateRol {request.Nombre}, {request.Estatus}");

                var nuevo = await _context.rols
                    .FirstOrDefaultAsync(r => r.Nombre == request.Nombre);

                return new Response<Rol>(nuevo!, "Rol creado correctamente");
            }
            catch (Exception ex)
            {
                return new Response<Rol>(null!, ex.Message)
                {
                    Success = false
                };
            }
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

            await _context.Database.GetDbConnection().
                ExecuteAsync("spUpdateRolPorId",
                new
                {
                    RolID = id,
                    Nombre = request.Nombre,
                    Estatus =request.Estatus
                },
                commandType: System.Data.CommandType.StoredProcedure);

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

            bool enUso = await _context.usuarios
                .AnyAsync(u => u.FkRol == id);

            if (enUso)
            {
                return new Response<Rol>(null!, "No se puede eliminar: hay usuarios con este rol")
                {
                    Success = false
                };
            }

            await _context.Database.GetDbConnection()
                .ExecuteAsync(
                    "spDeleteRolPorId",
                    new { RolID = id },
                    commandType: System.Data.CommandType.StoredProcedure
                );

            return new Response<Rol>(rol, "Rol eliminado correctamente")
            {
                Success = true
            };
        }
    }
}