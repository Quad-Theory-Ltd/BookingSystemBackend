using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.WeightView
{
    public class CreateWeigthView
    {
        public int CompanyId { get; set; }
        public string WeightDescription { get; set; }
        public decimal MinimumWeight { get; set; }
        public decimal MaximumWeight { get; set; }
        public int MeasurementUnitId { get; set; }
        public decimal Cost { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
    }
}
