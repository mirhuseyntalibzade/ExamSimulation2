using AutoMapper;
using BL.DTOs.ServiceDTO;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Admin.Controllers;

[Area("Admin")]

public class ServiceController : Controller
{
    readonly IServiceModelService _service;
    readonly IMapper _mapper;

    public ServiceController(IServiceModelService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        try
        {
            ICollection<GetServiceDTO> services = await _service.GetAllServiceAsync();
            return View(services);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddServiceDTO service)
    {
        try
        {
            await _service.AddServiceAsync(service);
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
            GetServiceDTO service = await _service.GetServiceByIdAsync(Id);
            UpdateServiceDTO updateServiceDTO = _mapper.Map<UpdateServiceDTO>(service);

            return View(updateServiceDTO);
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
    public async Task<IActionResult> Update(UpdateServiceDTO updateServiceDTO)
    {
        try
        {
            await _service.UpdateService(updateServiceDTO);
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
            await _service.DeleteService(Id);
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
