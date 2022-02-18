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
using EducationPortalAPI.ManageSQL;
using System.Data;

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
                    Expires = now.AddSeconds(6000),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                //Create Request
                try
                {
                    string[] timeSplit = entity.MeetingTime.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    TimeSpan time = new TimeSpan(0, Convert.ToInt32(timeSplit[0]), Convert.ToInt32(timeSplit[1].Substring(0, 1)), 0, 0);
                    entity.MeetingDate = entity.MeetingDate.Add(time);
                }
                catch (Exception ex)
                {
                    AuditLog.WriteError(ex.Message);
                }
               
                var client = new RestClient("https://api.zoom.us/v2/users/dulasimca@gmail.com/meetings");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new { topic = entity.Topics, duration = entity.Duration, start_time = entity.MeetingDate, type = "2" });
                request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));


                IRestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;
                JObject MyjObject = JObject.Parse(restResponse.Content);
                string startURL = (string)MyjObject["start_url"];
                string JoinURL = (string)MyjObject["join_url"];
                string SuccessCode = Convert.ToString(numericStatusCode);

                if (numericStatusCode == 201)
                {
                    MeettingEntity meetting = new MeettingEntity();
                    meetting.MeetingId = (string)MyjObject["id"];
                    meetting.MeetingURL = (string)MyjObject["join_url"]; ;
                    meetting.Passcode = (string)MyjObject["password"]; ;
                    meetting.Topics = (string)MyjObject["topic"]; ;
                    meetting.Duration = (int)MyjObject["duration"]; ;
                    meetting.MeetingDate = (DateTime)MyjObject["start_time"];
                    meetting.HostEmail = (string)MyjObject["host_email"];
                    meetting.ClassId = entity.ClassId;
                    meetting.SchoolId = entity.SchoolId;
                    meetting.MeetingTime = entity.MeetingTime;
                    meetting.SectionCode = entity.SectionCode;
                    meetting.RowId = entity.RowId;
                    meetting.Flag = true;
                    meetting.StartURL = startURL;
                    meetting.CreatedBy = entity.CreatedBy;
                    ManageZoomSql _manageSQL = new ManageZoomSql();
                    _manageSQL.InsertData(meetting);

                }
                //Insert record into Database.
                return JsonConvert.SerializeObject(MyjObject);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("{id}")]
        public string Get(string SchoolId, string ClassId, string SectionCode, string Date)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            var procedure = string.Empty;
            sqlParameters.Add(new KeyValuePair<string, string>("@Date", Date));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", SchoolId));
            if (ClassId == "0")
            {
                sqlParameters.Add(new KeyValuePair<string, string>("@CreatedBy", SectionCode));
                procedure = "GetMeetingInfoCreatedBy";

            } else
            {
                sqlParameters.Add(new KeyValuePair<string, string>("@SectionCode", SectionCode));
                sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", ClassId));
                procedure = "GetMeetingInfo";
            }
            var data = manageSQL.GetDataSetValues(procedure, sqlParameters);
            return JsonConvert.SerializeObject(data.Tables[0]);
        }
    }
    public class ZoomEntity
    {
        public Int64 RowId { get; set; }
        public int ClassId { get; set; }
        public int SectionCode { get; set; }
        public string SchoolId { get; set; }
        public DateTime MeetingDate { get; set; }
        public string MeetingTime { get; set; }
        public int Duration { get; set; }
        public string Topics { get; set; }
        public string CreatedBy { get; set; }
    }

    public class MeettingEntity
    {
        public Int64 RowId { get; set; }
        public int ClassId { get; set; }
        public int SectionCode { get; set; }
        public string SchoolId { get; set; }
        public DateTime MeetingDate { get; set; }
        public string MeetingTime { get; set; }
        public int Duration { get; set; }
        public string MeetingId { get; set; }
        public string MeetingURL { get; set; }
        public string Topics { get; set; }
        public string Passcode { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Flag { get; set; }
        public string HostEmail { get; set; }
        public string StartURL { get; set; }
        public string CreatedBy { get; set; }
    }
}
