using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Models
{
    public class Connector
    {
        public int Id { get; set; }
        public int ChargeStationId { get; set; }
        public decimal MaxCurrent { get; set; }
        public ChargeStation ChargeStation { get; set; }
    }
}
