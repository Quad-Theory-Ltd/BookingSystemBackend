using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelView
{
    public class ParcelView
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal VAT_TaxPercentage { get; set; }
        public decimal VAT_TaxAmount { get; set; }
        public decimal OrderPayableAmount { get; set; }
        public string Barcode { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int BranchId { get; set; }



    }
}
