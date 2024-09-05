using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelDetailsView
{
    public  class ParcelDetailsForPaymentView
    {
        public int ParcelOrderNo { get; set; }
        public decimal OrderAmount { get; set; }
        public string Description { get; set; }
        public int PaymentTypeMethodId { get; set; }
        public string PaymentMethodName { get; set; }

    }
}
