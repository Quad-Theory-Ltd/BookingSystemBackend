using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.CurrentStockCurierServiceView
{
    public class CurrentStockCurierServiceView
    {
        public int Id { get; set; }
        public int DimentionId { get; set; }
        public string BoxName { get; set; }
        public  DateTime TransactionDate { get; set; }
        public decimal ReceiveQty { get; set; }
        public decimal IssueQty { get; set; }
        public decimal AdjustmentQty { get; set; }
        public decimal BalanceQty { get; set; }
    }
}
