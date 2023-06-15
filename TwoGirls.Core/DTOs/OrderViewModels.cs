using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.DTOs
{
    public class OrderViewModels
    {
        public bool IsPosted { get; set; }
        public int AddressId { get; set; }
        public int CartId { get; set; }
        public decimal OrderPrice { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public List<Address> Addresses { get; set; } = new List<Address>();
        public Address Address { get; set; } = new Address();
    }
}
