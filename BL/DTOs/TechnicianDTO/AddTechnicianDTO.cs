using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.TechnicianDTO
{
    public class AddTechnicianDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ServiceId { get; set; }
        public IFormFile Image { get; set; }
    }
}
