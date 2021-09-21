using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace EducationPortalAPI.Controllers.Zoom
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoomIntegrationController : ControllerBase
    {

        [HttpPost("{id}")]
        public string Post(ZoomEntity entity)
        {
            try
            {
                // declare the variables
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var now = DateTime.Now;
                var apiSecret = "0yIoVcQKeQX0tG9hZt0qRo9rKXx2sqLeTWjW";
                byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);
                

                ///change it to Token String for the Authorization Header
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "lJXDJ2_mTtWmDHEMAtpW0A",
                    Expires = now.AddSeconds(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                //Create Request

                var client = new RestClient("https://api.zoom.us/v2/users/dulasimca@gmail.com/meetings");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new { topic = "Maths Class", duration = "30", start_time = "2021-09-21T03:00:00", type = "2" });
                request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));


                IRestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;
                var jObject = JObject.Parse(restResponse.Content);
                string startURL = (string)jObject["start_url"];
                string JoinURL = (string)jObject["join_url"];
                string SuccessCode = Convert.ToString(numericStatusCode);

                return JsonConvert.SerializeObject(startURL);

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
    public class ZoomEntity
    {
        public string apiSecret { get; set; }
    }
}
