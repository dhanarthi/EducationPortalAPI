using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyAchievementsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(AcssshievementEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", entity.SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@eventdate",  entity.eventdate));
            sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", entity.StudentId));
            sqlParameters.Add(new KeyValuePair<string, string>("@EventDetailS", entity.EventDetailS));
            sqlParameters.Add(new KeyValuePair<string, string>("@Place", entity.Place));
            sqlParameters.Add(new KeyValuePair<string, string>("@AchievementStatus", entity.AchievementStatus));
            sqlParameters.Add(new KeyValuePair<string, string>("@filename", entity.filename));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertAchievements", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            ds = manageSQL.GetDataSetValues("GetAchievements", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class AcssshievementEntity
    {
        public Int64 RowId { get; set; }
        public string SchoolID { get; set; }
        public string StudentId { get; set; }
        public string eventdate { get; set; }
        public string EventDetailS { get; set; }
        public string Place { get; set; }
        public string AchievementStatus { get; set; }
        public string filename { get; set; }
        public bool Flag { get; set; }
    }
}
