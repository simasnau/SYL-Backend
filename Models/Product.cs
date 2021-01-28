using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SYL_Backend.Models
{
    public partial class Product
    {
        [Key]
        [Column("Shop ID")]
        public int ShopId { get; set; }
        public double Price { get; set; }
        [Key]
        [StringLength(40)]
        public string Name { get; set; }
        [Column("Product type ID")]
        public int ProductTypeId { get; set; }

        [ForeignKey(nameof(ShopId))]
        [InverseProperty(nameof(Seller.Products))]
        public virtual Seller Shop { get; set; }
    }
}
