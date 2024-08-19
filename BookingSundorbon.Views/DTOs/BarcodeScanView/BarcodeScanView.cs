using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.BarcodeScanView
{
    public class BarcodeScanView
    {
        public int Id { get; set; }
        public int ParcelNo { get; set; }
        public string BarcodeNo { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }

        // BarcodeScan Details 
        public int BarcodeScanId { get; set; }
        public int ScanningPointId { get; set; }
        public int ScanningPersonId { get; set; }
        public int ParcelStatusId { get; set; }

    }
}
