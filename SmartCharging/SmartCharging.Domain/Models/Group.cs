using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Models
{
    public class Group : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Capacity { get; set; }
        public IEnumerable<ChargeStation> ChargeStations { get; set; }
    }
}
