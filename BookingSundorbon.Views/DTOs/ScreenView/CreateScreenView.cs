using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ScreenView
{
    public class CreateScreenView
    {
        public string Id { get; set; }
        public string UIName { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
    }
}
