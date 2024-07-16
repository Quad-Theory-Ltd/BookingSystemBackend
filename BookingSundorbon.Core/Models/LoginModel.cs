using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Core.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int ParcelId { get; set; }
        public int AgentId { get; set; }
        public DateTime CreationDate { get; set; }
       
    }
}
