using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortalAPI.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentListController : Controller
    {
        [HttpGet("{id}")]
        public string Get(int SectionId, int ClassId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SectionId", Convert.ToString(SectionId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", Convert.ToString(ClassId)));
            ds = manageSQL.GetDataSetValues("GetStudentList", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}
