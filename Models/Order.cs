using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SYL_Backend.Models
{
    public partial class Order
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public double Quantity { get; set; }
        [Column("Buyer ID")]
        public int BuyerId { get; set; }
        [Column("Seller ID")]
        public int SellerId { get; set; }
        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }
    }
}
