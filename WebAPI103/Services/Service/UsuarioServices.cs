using Domain.Entities;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using WebAPI103.Context;
using WebAPI103.Services.Interfaces;

namespace WebAPI103.Services
{
    // Funcionalidades para la base de datos
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los usuarios
        public async Task<Response<List<Usuario>>> ObtenerUsuario()
        {
            var usuarios = await _context.usuarios.ToListAsync();

            return new Response<List<Usuario>>
            {
                Success = true,
                Result = usuarios
            };
        }

        // Obtener usuario por ID
        public async Task<Response<Usuario>> ById(int id)
        {
            Usuario? usuario = await _context.usuarios
                .FirstOrDefaultAsync(x => x.PkUsuario == id);

            return new Response<Usuario>(usuario);
        }

        // Crear un nuevo usuario
        public async Task<Response<Usuario>> Create(Usuario request)
        {
            Usuario newUser = new Usuario
            {
                Nombre = request.Nombre,
                Username = request.Username,
                Password = request.Password,
                FkRol = request.FkRol
            };

            await _context.usuarios.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return new Response<Usuario>(
                newUser,
                "Se creó correctamente"
            );
        }

        public async Task<Response<Usuario>> Update(int id, Usuario request)
        {
            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(x => x.PkUsuario == id);

            if (usuario == null)
            {
                return new Response<Usuario>(
                    null!,
                    "Usuario no encontrado"
                )
                {
                    Success = false
                };
            }

            usuario.Nombre = request.Nombre;
            usuario.Username = request.Username;
            usuario.Password = request.Password;
            usuario.FkRol = request.FkRol;

            await _context.SaveChangesAsync();

            return new Response<Usuario>(
                usuario,
                "Usuario actualizado correctamente"
            );
        }
        public async Task<Response<Usuario>> Delete(int id)
        {
            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(x => x.PkUsuario == id);

            if (usuario == null)
            {
                return new Response<Usuario>(
                    null!,
                    "Usuario no encontrado"
                )
                {
                    Success = false
                };
            }

            _context.usuarios.Remove(usuario);

            await _context.SaveChangesAsync();

            return new Response<Usuario>(
                usuario,
                "Usuario eliminado correctamente"
            );
        }
    }
}