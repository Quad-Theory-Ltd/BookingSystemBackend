﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelNumbersWithBarcodeView
{
    public class ParcelNumbersWithBarcodeView
    {
        public int PercelNumber { get; set; }
        public string Barcode { get; set; }

        public string RouteName { get; set; }

        public string ReceiverName { get; set; }

        public string ReceiverAddress { get; set; }
        public string RecordSerialNoWithParcelNo { get; set; }

    }
}