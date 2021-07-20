using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CascadasPOS.Models
{
    public class Tax
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Percentage { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
