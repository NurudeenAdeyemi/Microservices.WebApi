using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.StockTrading.Models
{
    public class UserStock
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid StockId { get; set; }

        public Stock Stock { get; set; }

        public decimal Price { get; set; }

        public int Share { get; set; }

        public decimal Total { get; set; }

    }
}
