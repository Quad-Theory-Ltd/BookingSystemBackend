﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelStatusView
{
    public class ParcelStatusView
    {
        public int Id { get; set; }
        public string ParcelStatusName { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}