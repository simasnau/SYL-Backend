using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SYL_Backend.Models;
using System.Linq;

namespace SYL_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        //GET orders of specific user.
        [HttpGet("{bID:int}")]
        public List<NOrder> GetOrders(int bID)
        {
            using SYLContext db = new SYLContext();
            var norders = (from s in db.Sellers
                          join o in db.Orders
                          on s.Id equals o.SellerId
                          where o.BuyerId==bID
                          select new NOrder
                          {
                              name = o.Name,
                              time = o.Time.ToString(@"hh\:mm"),
                              quantity = o.Quantity,
                              adress = s.Adress

                          }).ToList();
            return norders;           
        }

        // Create new order for user which requested it.
        [HttpPost("add")]
        public void addOrder([FromForm]string name,[FromForm] string time,[FromForm] double quantity,[FromForm] int bID,[FromForm] int sID)
        {
            if(Regex.IsMatch(time, @"^[0-2]\d:[0-5]\d$") && quantity > 0 && bID >= 0 && sID >= 0)
            {
                using SYLContext db = new SYLContext();

                var order = new Order
                {
                    Name=name,
                    Time=TimeSpan.Parse(time),
                    Quantity=quantity,
                    BuyerId=bID,
                    SellerId=sID
                };

                db.Orders.Add(order);
                db.SaveChanges();
            }
            
        }

        public class NOrder
        {
            public string name { get; set; }
            public string time { get; set; }
            public double quantity { get; set; }
            public string adress { get; set; }
        }
    }
       
}
