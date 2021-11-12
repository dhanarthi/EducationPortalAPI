using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentResultController : Controller
    {
        [HttpPost("{id}")]
        public string Post([FromBody]List<OnlineAssessmentAnswers> entity)
        {
           ManageAssessmentResult assessmentResult = new ManageAssessmentResult();
            var result = assessmentResult.InsertData(entity);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string TestID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", TestID));
            var data = manageSQL.GetDataSetValues("CheckOnlineAssessment", sqlParameters);
            return JsonConvert.SerializeObject(data.Tables[0]);
        }
    }
}