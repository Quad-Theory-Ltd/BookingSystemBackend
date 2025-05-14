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
        public string Boxname { get; set; }
        public string  BranchName { get; set; }
        public string CargoTypeName { get; set; }
        public string RouteName { get; set; }
        public string RecordSerialNo { get; set; }
    }
}
