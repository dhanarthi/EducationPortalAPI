using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.Controllers.Forms;

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class AadharCheckController : ControllerBase
    {
        [HttpGet("{id}")]

        public string Get(string AadharNo, string StudentId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AadharNo", AadharNo));
           // sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", StudentId));
            var result = manageSQL.GetDataSetValues("GetCheckAadharNo", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }
}
