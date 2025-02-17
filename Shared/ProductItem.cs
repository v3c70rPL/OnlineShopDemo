using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderItem
    {
        string Id { get; set; } // ProductService -  Product - Id - stored here for reference
        string Name { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
