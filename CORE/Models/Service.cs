using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class Service : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Technician>? Technicians { get; set; }
    }
}
