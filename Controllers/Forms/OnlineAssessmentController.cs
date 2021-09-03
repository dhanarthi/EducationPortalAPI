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
    public class OnlineAssessmentController : ControllerBase
    {
         
        [HttpPost("{id}")]
        public string Post(OnlineAssessmentMasterEntity entity)
        {
            ManageOnlineAssessment onlineAssessment = new ManageOnlineAssessment();           
            var result = onlineAssessment.InsertData(entity);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
           
            return "";
        }

    }
    public class OnlineAssessmentMasterEntity
    {
        public Int64 RowId { get; set; }
        public string SchoolId { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public DateTime AssessmentDate { get; set; }
        public bool Flag { get; set; }
        public List<OnlineAssessmentQuestionsEntity> Questions { get; set; }
    }
    public class OnlineAssessmentQuestionsEntity
    {
        public Int64 QuestionId { get; set; }
        public Int64 RowId { get; set; }
        public string QuestionDetails { get; set; }
        public List<OnlineAssessmentOptions> Options { get;set;}
    }
    public class OnlineAssessmentOptions
    {
       public Int64  AnswerId { get; set; }
       public Int64  QuestionId { get; set; }
       public bool IsAnswer { get; set; }
    }
}
