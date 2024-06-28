using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ShipmentProcessRepository
{
    public class ShipmentProcessRepository : IShipmentProcessRepository
    {
        private readonly string _connectionString;

        public ShipmentProcessRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
