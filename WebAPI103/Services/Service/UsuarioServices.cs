using Domain.dto;
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
        public async Task<Response<List<UsuarioDto>>> ObtenerUsuario()
        {
            var usuarios = await _context.usuarios
                .Select(u => new UsuarioDto
                {
                    PkUsuario = u.PkUsuario,
                    Nombre = u.Nombre,
                    Username = u.Username,
                    Password = u.Password,
                    NombreRol = u.Roles != null ? u.Roles.Nombre : string.Empty
                })
                .ToListAsync();

            return new Response<List<UsuarioDto>>
            {
                Success = true,
                Result = usuarios
            };
        }

        // Obtener usuario por ID
        public async Task<Response<UsuarioDto>> ById(int id)
        {
            UsuarioDto? usuario = await _context.usuarios
                .Where(u => u.PkUsuario == id)
                .Select(u => new UsuarioDto
                {
                    PkUsuario = u.PkUsuario,
                    Nombre = u.Nombre,
                    Username = u.Username,
                    Password = u.Password,
                    NombreRol = u.Roles != null ? u.Roles.Nombre : string.Empty
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return new Response<UsuarioDto>(null!, "Usuario no encontrado")
                {
                    Success = false
                };
            }

            return new Response<UsuarioDto>(usuario);
        }

        // Crear un nuevo usuario
        public async Task<Response<UsuarioDto>> Create(UsuarioRequestDto request)
        {
            var rol = await _context.rols
                .FirstOrDefaultAsync(r => r.Nombre == request.NombreRol);

            if (rol == null)
            {
                return new Response<UsuarioDto>(null!, "El rol indicado no existe")
                {
                    Success = false
                };
            }

            Usuario newUser = new Usuario
            {
                Nombre = request.Nombre,
                Username = request.Username,
                Password = request.Password,
                FkRol = rol.PkRol
            };

            await _context.usuarios.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return new Response<UsuarioDto>(
                new UsuarioDto
                {
                    PkUsuario = newUser.PkUsuario,
                    Nombre = newUser.Nombre,
                    Username = newUser.Username,
                    Password = newUser.Password,
                    NombreRol = rol.Nombre
                },
                "Se creó correctamente"
            );
        }

        // Actualizar usuario
        public async Task<Response<UsuarioDto>> Update(int id, UsuarioRequestDto request)
        {
            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(x => x.PkUsuario == id);

            if (usuario == null)
            {
                return new Response<UsuarioDto>(null!, "Usuario no encontrado")
                {
                    Success = false
                };
            }

            var rol = await _context.rols
                .FirstOrDefaultAsync(r => r.Nombre == request.NombreRol);

            if (rol == null)
            {
                return new Response<UsuarioDto>(null!, "El rol indicado no existe")
                {
                    Success = false
                };
            }

            usuario.Nombre = request.Nombre;
            usuario.Username = request.Username;
            usuario.Password = request.Password;
            usuario.FkRol = rol.PkRol;

            await _context.SaveChangesAsync();

            return new Response<UsuarioDto>(
                new UsuarioDto
                {
                    PkUsuario = usuario.PkUsuario,
                    Nombre = usuario.Nombre,
                    Username = usuario.Username,
                    Password = usuario.Password,
                    NombreRol = rol.Nombre
                },
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