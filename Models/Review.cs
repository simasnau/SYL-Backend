using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SYL_Backend.Models
{
    public partial class Review
    {
        [Key]
        [StringLength(40)]
        public string Username { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        [Key]
        [StringLength(50)]
        public string SellerName { get; set; }
    }
}
