using AutoMapper;
using BL.DTOs.TechnicianDTO;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;

namespace BL.Services.Concretes
{
    public class TechnicianService : ITechnicianService
    {
        readonly ITechnicianRepository _repository;
        readonly IMapper _mapper;
        readonly IWebHostEnvironment _webHostEnvironment;
        public TechnicianService(ITechnicianRepository repository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task AddTechnicianAsync(AddTechnicianDTO technicianDTO)
        {
            Technician technician = _mapper.Map<Technician>(technicianDTO);
            
            string rootPath = _webHostEnvironment.WebRootPath;
            string fileName = technicianDTO.Image.FileName;
            string folderPath = rootPath + "/uploads/technicians/";
            string filePath = Path.Combine(folderPath, fileName);

            bool exists = Directory.Exists(folderPath);

            if (!exists)
            {
                Directory.CreateDirectory(folderPath);
            }

            string[] allowedExtensions = [".png", ".jpg", ".jpeg"];
            bool isAllowed = false;
            foreach (string extention in allowedExtensions)
            {
                if (Path.GetExtension(fileName) == extention)
                {
                    isAllowed = true;
                    break;
                }
            }
            if (!isAllowed)
            {
                throw new OperationNotValidException("file is not supported");
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await technicianDTO.Image.CopyToAsync(stream);
            }
            technician.ImageURL = fileName;

            await _repository.AddAsync(technician);
            int result = await _repository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationNotValidException("Couldnt added technician.");
            }
        }

        public async Task DeleteTechnician(int Id)
        {
            Technician technician = await _repository.GetByIdAsync(Id);
            _repository.Delete(technician);
            int result = await _repository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationNotValidException("Couldnt deleted technician.");
            }
        }

        public async Task<ICollection<GetTechnicianDTO>> GetAllTechnicianAsync()
        {
            ICollection<Technician> technicians= await _repository.GetAllAsync();
            return _mapper.Map<ICollection<GetTechnicianDTO>> (technicians);
        }

        public async Task<GetTechnicianDTO> GetTechnicianByIdAsync(int Id)
        {
            Technician technician = await _repository.GetByIdAsync(Id);
            if (technician is null)
            {
                throw new NotFoundException("Item is not found.");
            }
            return _mapper.Map<GetTechnicianDTO>(technician);
        }

        public async Task UpdateTechnician(UpdateTechnicianDTO technicianDTO)
        {
            Technician technician = await _repository.GetByConditionAsync(t=>t.Id==technicianDTO.Id);
            if (technician is null)
            {
                throw new NotFoundException("Item is not found.");
            }
            Technician updatedTechnician = _mapper.Map<Technician>(technicianDTO);

            string rootPath = _webHostEnvironment.WebRootPath;
            string fileName = technicianDTO.Image.FileName;
            string folderPath = rootPath + "/uploads/technicians/" + fileName;

            string[] allowedExtensions = [".png", ".jpg", ".jpeg"];
            bool isAllowed = false;
            foreach (string extention in allowedExtensions)
            {
                if (Path.GetExtension(fileName) == extention)
                {
                    isAllowed = true;
                    break;
                }
            }
            if (!isAllowed)
            {
                throw new OperationNotValidException("file is not supported");
            }

            using (var stream = new FileStream(folderPath, FileMode.Create))
            {
                await technicianDTO.Image.CopyToAsync(stream);
            }
            updatedTechnician.ImageURL = fileName;

            _repository.Update(updatedTechnician);
            int result = await _repository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationNotValidException("Couldn't save changes");
            }
        }
    }
}
