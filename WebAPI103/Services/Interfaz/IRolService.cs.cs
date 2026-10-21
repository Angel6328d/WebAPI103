using Domain.dto;
using Domain.Entities;
using Domain.Helpers;

namespace WebAPI103.Services.Interfaces
{
    public interface IRolService
    {
        Task<Response<List<Rol>>> ObtenerRoles();
        Task<Response<Rol>> ById(int id);
        Task<Response<Rol>> Create(RolRequestDto request);
        Task<Response<Rol>> Update(int id, RolRequestDto request);
        Task<Response<Rol>> Delete(int id);
    }
}