using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using EducationPortalAPI.ManageSQL;

namespace EducationPortalAPI.Controllers.Zoom
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoomSettingsController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string SchoolId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", SchoolId));
            ds = manageSQL.GetDataSetValues("GetZoomSettings", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

    }
}
