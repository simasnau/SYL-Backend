using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SYL_Backend.Models
{
    [Table("Seller")]
    public partial class Seller
    {
        public Seller()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Seller name")]
        [StringLength(50)]
        public string SellerName { get; set; }
        [StringLength(70)]
        public string Adress { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(50)]
        public string Email { get; set; }

        [InverseProperty(nameof(Product.Shop))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
