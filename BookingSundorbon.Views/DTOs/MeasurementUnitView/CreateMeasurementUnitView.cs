using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.MeasurementUnitView
{
    public class CreateMeasurementUnitView
    {
        public string UnitDescription { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
    }
}
