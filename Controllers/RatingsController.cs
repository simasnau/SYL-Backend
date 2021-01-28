using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SYL_Backend.Models;
using System.Linq;

namespace SYL_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ILogger<RatingsController> _logger;

        public RatingsController(ILogger<RatingsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{name}")]
        public List<Review> GetAllReviews(string name)
        {
            return GetReviewList(name);
        }

        // GET average rating of a seller
        [HttpGet("{name}/avg")]
        public double GetAverageRating(string name)
        {
            var list = GetReviewList(name);
            var sum = 0.0;
            foreach(var review in list) sum += review.Rating;
            var rating = sum / list.Count;

            return rating;
        }

        // POST seller review
        [HttpPost("{name}/add")]
        public IActionResult addReview([FromForm] string username, [FromForm] int rating, [FromForm] string text, string name)
        {
            if (rating>0 && rating<=5)
            {
                try
                {
                    using SYLContext db = new SYLContext();
                    var review = new Review
                    {
                        Username=username,
                        Rating=rating,
                        Text=text,
                        SellerName=name
                    };
                    db.Reviews.Add(review);
                    db.SaveChanges();                    
                    return Ok();

                }catch(SqlException sqlex) {
                    _logger.LogInformation("SqlException happened. Probably primary key collision: " + sqlex);
                    return BadRequest();
                }catch(Exception e)
                {
                    _logger.LogInformation("Exception happened: " + e);
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        // GETs all reviews for a specific seller.
        public List<Review> GetReviewList(string name)
        {
            using SYLContext db = new SYLContext();
            var reviews = db.Reviews.Where(x=>x.SellerName.Equals(name)).ToList();
            return reviews;

        }


    }
}
