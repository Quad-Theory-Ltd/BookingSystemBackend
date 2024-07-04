using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.GetTransitionCostView
{
    public class GetTransitionCostOutputView
    {
      
       
        public decimal TotalCost { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal  DisCountedPercentageAmount { get; set; }
        public decimal AfterDiscountCost { get; set; }
        public decimal CargoCost { get; set; }
        public decimal RouteCost { get; set; }
        public decimal WeightCost { get; set; }
        public decimal ExtraPackagingCost { get; set; }
        public decimal ItemTypeCost {  get; set; }
        public string ShippingServiceName { get; set; }
        public decimal ShippingServiceCost { get; set; }
        public decimal ShippingServicePercentage { get; set; }
        public decimal ShippingServicePercentageAmount { get; set; }
        public decimal DimensionCost { get; set; }
        public decimal PickUpCost { get; set; }
        public decimal VATPercentage { get; set; }
        public decimal VATAmount { get; set; }
        public decimal VATAmountPercentage { get;set; }
        public decimal TotalCostWithVAT { get; set; }
        public decimal TotalCostWithoutVAT { get; set; }
        


    }
    }

