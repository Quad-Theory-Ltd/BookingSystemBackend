using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelView
{
    public class ParcelForBarcodeScanView
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int RouteId { get; set; }
        public int WeightId { get; set; }
        public int DimensionId { get; set; }
        public int CargoTypeId { get; set; }
        public int BranchId { get; set; }
        public string Boxname { get; set; }
        public string  BranchName { get; set; }
        public string WeightDescription { get; set; }
        public string CargoTypeName { get; set; }
    }
}
