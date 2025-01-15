using AutoMapper;
using BL.DTOs.ServiceDTO;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BL.Services.Concretes
{
    public class ServiceModelService : IServiceModelService
    {
        readonly IServiceRepository _repository;
        readonly IMapper _mapper;
        public ServiceModelService(IServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddServiceAsync(AddServiceDTO ServiceDTO)
        {
            Service Service = _mapper.Map<Service>(ServiceDTO);
            await _repository.AddAsync(Service);
            int result = await _repository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationNotValidException("Couldn't add service.");
            }
        }

        public async Task DeleteService(int Id)
        {
            Service Service = await _repository.GetByIdAsync(Id);
            _repository.Delete(Service);
            int result = await _repository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationNotValidException("Couldn't deleted service.");
            }
        }

        public async Task<ICollection<GetServiceDTO>> GetAllServiceAsync()
        {
            ICollection<Service> Services = await _repository.GetAllAsync();
            return _mapper.Map<ICollection<GetServiceDTO>>(Services);
        }

        public async Task<GetServiceDTO> GetServiceByIdAsync(int Id)
        {
            Service service = await _repository.GetByIdAsync(Id);
            if (service is null)
            {
                throw new NotFoundException("Item is not found.");
            }
            return _mapper.Map<GetServiceDTO>(service);
        }

        public async Task<ICollection<SelectListItem>> SelectAllServices()
        {
            return await _repository.SelectServicesAsync();
        }

        public async Task UpdateService(UpdateServiceDTO ServiceDTO)
        {
            Service service = await _repository.GetByConditionAsync(t => t.Id == ServiceDTO.Id);
            if (service is null)
            {
                throw new NotFoundException("Item is not found.");
            }
            Service updatedService = _mapper.Map<Service>(ServiceDTO);

            _repository.Update(updatedService);
            int result = await _repository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationNotValidException("Couldn't save changes");
            }
        }
    }
}
