using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SYL_Backend.Models;
using System.Linq;

namespace SYL_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SellerController : ControllerBase
    {
        
        private readonly ILogger<SellerController> _logger;

        public SellerController(ILogger<SellerController> logger)
        {
            _logger = logger;
        }



        // GETs all sellers
        [HttpGet("all")]
        public List<string> GetAllSellers()
        {
            using (SYLContext db = new SYLContext())
            {
                return db.Sellers.Select(x => x.SellerName).Distinct().ToList();
            }
            
        }


        // GET seller id for login purposes
        [HttpGet]
        public string GetSellerID([FromForm]string email = null, [FromForm]string password = null, string name = null)
        {
              using SYLContext db = new SYLContext();
              if (email == null && password == null && name != null)
              {
                  try
                  {
                    var value = db.Sellers.Where(x => x.SellerName.Equals(name)).FirstOrDefault().Id.ToString();                     
                    return value;
                  }
                  catch (Exception e)
                  {
                      return "Failed to get id from name." + e;
                  }
              }
              else if (name == null && email != null && password != null)
              {
                  try
                  {
                    var sellerID = db.Sellers.Where(x => x.Email.Equals(email) && x.Password.Equals(password)).FirstOrDefault().Id.ToString();                      
                      return sellerID;
                  }
                  catch (Exception e)
                  {
                      return "Failed to get id from email and password." + e;
                  }

              }
              else {
                  return "All parameters are null";

              } 
        }


        // Create new seller
        [HttpPost("add")]
        public void createSeller([FromForm] string sellerName, [FromForm] string adress, [FromForm] string pass, [FromForm] string email)
        {

            using SYLContext db = new SYLContext();
            var seller = new Seller
            {
                SellerName=sellerName,
                Adress=adress,
                Password=pass,
                Email=email
            };

            db.Sellers.Add(seller);
            db.SaveChanges();
            
        }
        


    }
}
