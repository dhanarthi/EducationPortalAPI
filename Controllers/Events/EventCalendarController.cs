using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPortalAPI.Controllers.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCalendarController : Controller
    {
        [HttpGet("{id}")]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            ds = manageSQL.GetDataSetValues("GetEventDetails", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}