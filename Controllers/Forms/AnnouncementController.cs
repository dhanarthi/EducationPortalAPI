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
    public class AnnouncementController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(AnnouncementsEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", entity.SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@Announcementdate", entity.Announcementdate));
            sqlParameters.Add(new KeyValuePair<string, string>("@AnnouncementTag", entity.AnnouncementTag));
            sqlParameters.Add(new KeyValuePair<string, string>("@Announcement", entity.Announcement));
            sqlParameters.Add(new KeyValuePair<string, string>("@Announcementfilename", entity.Announcementfilename));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertAnnouncements", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            ds = manageSQL.GetDataSetValues("GetAnnouncements", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class AnnouncementsEntity
    {
        public Int64 RowId { get; set; }
        public string SchoolID { get; set; }
        public string Announcementdate { get; set; }
        public string AnnouncementTag { get; set; }
        public string Announcement { get; set; }
        public string Announcementfilename { get; set; }
        public bool Flag { get; set; }
    }
}
