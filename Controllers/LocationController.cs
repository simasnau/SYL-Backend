using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SYL_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILogger<LocationController> logger)
        {
            _logger = logger;
        }



        // Converts address to coordinates and returns them in a list.
        [HttpGet]
        public async Task<IEnumerable<double>> getCoordinates(string adress)
        {
            string requestUri;
            WebRequest request;
            WebResponse response;

            requestUri = string.Format("https://api.opencagedata.com/geocode/v1/xml?key={1}&q={0}", Uri.EscapeDataString(adress), Secrets.GEOapi_key);
            request = WebRequest.Create(requestUri);
            response = request.GetResponse();

            XDocument xdoc = XDocument.Load(response.GetResponseStream());
            XElement locationElement = xdoc.Element("response").Element("results").Element("result").Element("geometry");
            XElement lat = locationElement.Element("lat");
            XElement lng = locationElement.Element("lng");

            
            List<double> list = new List<double>();
            
            list.Add(Double.Parse(lat.Value));
            list.Add(Double.Parse(lng.Value));

            return list;
            
           
        }


    }

}
