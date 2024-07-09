using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ScreenView
{
    public class CreateScreenFunctionView
    {
        public string Id { get; set; }
        public string ScreenId { get; set; }
        public string FunctionId { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
    }
}
