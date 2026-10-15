
using Domain.Entities;
using Domain.Helpers;

namespace WebAPI103.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Response<List<Usuario>>> ObtenerUsuario();

        Task<Response<Usuario>> ById(int id);

        Task<Response<Usuario>> Create(Usuario request);

        Task<Response<Usuario>> Update(int id, Usuario request);

        Task<Response<Usuario>> Delete(int id);
    }
}
