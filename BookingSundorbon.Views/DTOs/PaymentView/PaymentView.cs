using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.PaymentView
{
    public class PaymentView
    {
        public int Id { get; set; }
        public DateTime? PaymentDate { get; set; } = null;
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public int ParcelOderNo { get; set; }
        public string PaymentInvoiceNo { get; set; }
        public decimal OrderAmount { get; set; }
        public int CurrencyTypeId { get; set; }
        public string? CurrencyType { get; set; }
        public string? CurrencySymbol { get; set; }
        public string Description { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
        public string RecordSerialNo { get; set; }
        public string ParcelNoWithRecordSerialNo { get; set; }
        public int PaymentStatusId { get; set; }
        public string? StatusName { get; set; }

        public string StripePaymentIntentId { get; set; } = null;
        public string StripeChargeId { get; set; } = null;
        public string CardLast4 { get; set; } = null;
        public string CardBrand { get; set; } = null;
        public string StripePaymentMethod { get; set; } = null;

    }

    public class StripePaymentDetails
    {
        public string StripePaymentIntentId { get; set; }
        public string StripeChargeId { get; set; }
        public string CardLast4 { get; set; }
        public string CardBrand { get; set; }
        public string StripePaymentMethod { get; set; }
        public int PaymentStatusId { get; set; }

    }

    public class PaymentIntentView
    {
        public int parcelId { get; set; }
        public string currency { get; set; }
    }
}
