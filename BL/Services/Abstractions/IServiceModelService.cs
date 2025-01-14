using BL.DTOs;
using BL.DTOs.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Abstractions
{
    public interface IServiceModelService
    {
        public Task<ICollection<GetServiceDTO>> GetAllServiceAsync();
        public Task<GetServiceDTO> GetServiceByIdAsync(int Id);
        public Task AddServiceAsync(AddServiceDTO technician);
        public Task UpdateService(UpdateServiceDTO technician);
        public Task DeleteService(int Id);
    }
}
