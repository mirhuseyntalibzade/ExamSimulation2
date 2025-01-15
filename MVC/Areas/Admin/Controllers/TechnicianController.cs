using AutoMapper;
using BL.DTOs.TechnicianDTO;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]


    public class TechnicianController : Controller
    {
        readonly ITechnicianService _technicianService;
        readonly IServiceModelService _serviceModelService;
        readonly IMapper _mapper;

        public TechnicianController(ITechnicianService technicianService, IMapper mapper, IServiceModelService serviceModelService)
        {
            _technicianService = technicianService;
            _mapper = mapper;
            _serviceModelService = serviceModelService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<GetTechnicianDTO> technicians = await _technicianService.GetAllTechnicianAsync();
                return View(technicians);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> Create()
        {
            ICollection<SelectListItem> services = await _serviceModelService.SelectAllServices();
            AddTechnicianDTO addTechnicianDTO = new AddTechnicianDTO
            {
                Services = services
            };

            return View(addTechnicianDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddTechnicianDTO technician)
        {
            try
            {
                await _technicianService.AddTechnicianAsync(technician);
                return RedirectToAction("Index");
            }
            catch (OperationNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> UpdateAsync(int Id)
        {
            try
            {
                GetTechnicianDTO technician = await _technicianService.GetTechnicianByIdAsync(Id);
                UpdateTechnicianDTO updateTechnicianDTO = _mapper.Map<UpdateTechnicianDTO>(technician);
                ICollection<SelectListItem> services = await _serviceModelService.SelectAllServices();
                updateTechnicianDTO.Services = services;

                return View(updateTechnicianDTO);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTechnicianDTO updateTechnicianDTO)
        {
            try
            {
                await _technicianService.UpdateTechnician(updateTechnicianDTO);
                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (OperationNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _technicianService.DeleteTechnician(Id);
                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (OperationNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
