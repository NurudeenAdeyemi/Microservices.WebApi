using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.StockTrading.Models
{
    public class BaseResponse
    {
        public string Message { get; set; }

        public bool Status { get; set; }
    }
}
