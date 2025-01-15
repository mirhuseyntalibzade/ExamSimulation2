using CORE.Models;
using DAL.Contexts;
using DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Concretes
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<SelectListItem>> SelectServicesAsync()
        {
            ICollection<SelectListItem> services = await _context.Services.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Title
            }).ToListAsync();
            return services;
        }

    }
}
