using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.S_UserDetailView
{
    public class S_UserDetailView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string UserAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
