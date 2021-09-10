using System;
using System.Collections.Generic;
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
    }
}