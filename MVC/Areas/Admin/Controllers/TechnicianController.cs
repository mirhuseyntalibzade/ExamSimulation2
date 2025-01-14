using AutoMapper;
using BL.DTOs.TechnicianDTO;
using BL.Exceptions;
using BL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class TechnicianController : Controller
    {
        readonly ITechnicianService _service;
        readonly IMapper _mapper;

        public TechnicianController(ITechnicianService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<GetTechnicianDTO> technicians = await _service.GetAllTechnicianAsync();
                return View(technicians);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddTechnicianDTO technician)
        {
            try
            {
                await _service.AddTechnicianAsync(technician);
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
                GetTechnicianDTO technician = await _service.GetTechnicianByIdAsync(Id);
                UpdateTechnicianDTO updateTechnicianDTO = _mapper.Map<UpdateTechnicianDTO>(technician);

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
                await _service.UpdateTechnician(updateTechnicianDTO);
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
                await _service.DeleteTechnician(Id);
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
