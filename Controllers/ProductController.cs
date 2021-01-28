using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SYL_Backend.Models;


namespace SYL_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }


        // GET all Products
        [HttpGet]
        public List<NProduct> Get()   
        {
            using SYLContext db = new SYLContext();

            var products = (from p in db.Products
                            join s in db.Sellers
                            on p.ShopId equals s.Id
                            select new NProduct
                            {
                                name = p.Name,
                                price = p.Price,
                                sellerName = s.SellerName,
                                adress = s.Adress
                            }).ToList();


            return products;

        }


        // Add product to database

        [HttpPost]
        [Route("add")]
        public void addProduct([FromForm]int shopID, [FromForm] double price, [FromForm] string name, [FromForm] int pID)
        {
            if (price > 0&& shopID>=0 &&pID>=0)
            {
                using SYLContext db = new SYLContext();
                var product = new Product
                {
                 ShopId=shopID,
                 Name=name,
                 Price=price,
                 ProductTypeId=pID
                };
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public class NProduct
        {
            public string adress { get; set; }
            public double price { get; set; }
            public string name { get; set; }
            public string sellerName { get; set; }
        }
    }
}
