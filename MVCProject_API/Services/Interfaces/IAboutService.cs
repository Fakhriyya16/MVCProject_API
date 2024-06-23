using AutoMapper;
using MVCProject_API.Data;
using MVCProject_API.DTOs.AboutDto;
using MVCProject_API.Models;

namespace MVCProject_API.Services.Interfaces
{
    public interface IAboutService
    {
        Task<List<AboutDto>> GetAll();
        Task<AboutDetailDto> GetById(int id);
        Task Create(AboutCreateDto request);
        Task Delete(About request);
        Task Edit(About about,AboutEditDto request);
    }
}
