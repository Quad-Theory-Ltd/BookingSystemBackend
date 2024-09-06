using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelView
{
    public class ParcelInfoForPaymentView
    {

        public int ParcelNo { get; set; }
        public string RecordSerialNo { get; set; }
        public decimal OrderPayableAmount { get; set; }
        public int PaymentTypeMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public string UniqItemDescription { get; set; }
        public string recordSerialNoWithParcelNo { get; set; }
        public string Barcode { get; set; }
        public string RouteName { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
    }
}
