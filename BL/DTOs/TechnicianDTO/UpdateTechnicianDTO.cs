using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.TechnicianDTO
{
    public class UpdateTechnicianDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ServiceId { get; set; }
        public ICollection<SelectListItem> Services { get; set; }
        public IFormFile Image { get; set; }
    }
}
