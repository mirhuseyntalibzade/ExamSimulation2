

using BL.DTOs.TechnicianDTO;

namespace BL.Services.Abstractions
{
    public interface ITechnicianService
    {
        public Task<ICollection<GetTechnicianDTO>> GetAllTechnicianAsync();
        public Task<GetTechnicianDTO> GetTechnicianByIdAsync(int Id);
        public Task AddTechnicianAsync(AddTechnicianDTO technician);
        public Task UpdateTechnician(UpdateTechnicianDTO technician);
        public Task DeleteTechnician(int Id);
    }
}
