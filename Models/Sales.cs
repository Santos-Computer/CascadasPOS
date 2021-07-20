using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CascadasPOS.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountPercentage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public int UserId { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
