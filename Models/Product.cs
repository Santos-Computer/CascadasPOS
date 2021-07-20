using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CascadasPOS.Models
{
    public class Product
    {
        private string _image;
        public int Id { get; set; }
        public int Code { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public int Barcode { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Cost { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Sales price")]
        public decimal SalesPrice { get; set; }
        public bool Active { get; set; } = true;
        public string Image
        {
            get => string.IsNullOrEmpty(_image) ? "~/Images/Uploads/noimage.jpg" : _image;
            set => _image = value;
        }

        [NotMapped]
        public IFormFile FormFile { get; set; }
        public bool PriceIncludeTax { get; set; }
        [Display(Name = "Unit of measure")]
        public int UnitOfMeasure { get; set; }
        [Display(Name = "Date created")]
        public DateTime? DateCreated { get; set; } = DateTime.Now.ToLocalTime();
        [Display(Name = "Date updated")]
        public DateTime? DateUpdated { get; set; }
        [Display(Name = "Tax")]
        public int TaxId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Tax Tax { get; set; }
        public Category Category { get; set; }

        public override string ToString() => Name;
    }
}
