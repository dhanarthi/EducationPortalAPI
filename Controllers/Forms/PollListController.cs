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
    public class PollListController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(PollListsEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", entity.SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@StudentID", entity.StudentID));
            sqlParameters.Add(new KeyValuePair<string, string>("@NomineeID", entity.NomineeID));
            sqlParameters.Add(new KeyValuePair<string, string>("@ElectionID", entity.ElectionID));
            sqlParameters.Add(new KeyValuePair<string, string>("@VoteStatus", entity.VoteStatus));
            sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", entity.ClassId));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertPollLists", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID, string Class, string StudentID, string ElectionID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@StudentID", StudentID));
            sqlParameters.Add(new KeyValuePair<string, string>("@ElectionID", ElectionID));
            ds = manageSQL.GetDataSetValues("GetPollLists", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class PollListsEntity
    {
        public string SchoolID { get; set; }
        public string StudentID { get; set; }
        public string NomineeID { get; set; }
        public string ElectionID { get; set; }
        public string VoteStatus { get; set; }        
        public string ClassId { get; set; }
        public bool Flag { get; set; }
    }
}
