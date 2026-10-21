using Domain.dto;
using Domain.Entities;
using Domain.Helpers;

namespace WebAPI103.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Response<List<UsuarioDto>>> ObtenerUsuario();

        Task<Response<UsuarioDto>> ById(int id);

        Task<Response<UsuarioDto>> Create(UsuarioRequestDto request);
        Task<Response<UsuarioDto>> Update(int id, UsuarioRequestDto request);

        Task<Response<Usuario>> Delete(int id);
    }
}