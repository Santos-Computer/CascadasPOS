using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadasPOS.Models
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
    }
}
