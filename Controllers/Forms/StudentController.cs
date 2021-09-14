using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        [HttpGet("{id}")]
        public string Get(String SchoolId, String ClassId, String SectionId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", SchoolId));
            sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", ClassId));
            sqlParameters.Add(new KeyValuePair<string, string>("@SectionId", SectionId));
            ds = manageSQL.GetDataSetValues("GetStudent", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}

