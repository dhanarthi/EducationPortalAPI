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
        public string Get(string SchoolID, string ClassID, string TestDate)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@TestDate", TestDate));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@Classcode", ClassID));
            var data = manageSQL.GetDataSetValues("GetOnlineAssessmentQuestion", sqlParameters);
            return JsonConvert.SerializeObject(data.Tables[0]);
        }

    }
    public class OnlineAssessmentMasterEntity
    {
        public int  type { get; set; }
        public Int64 RowId { get; set; }
        public string SchoolId { get; set; }
        public string Classcode { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int QuestionType { get; set; }
        public int TotalMarks { get; set; }
        public int TotalDuration { get; set; }
        public int DurationType { get; set; }
        public int SubjectId { get; set; }
        public bool Flag { get; set; }
        public int Medium { get; set; }
        public string AssessmentTime { get; set; }
        public List<OnlineAssessmentQuestionsEntity> Questions { get; set; }
    }
    public class OnlineAssessmentQuestionsEntity
    {
        public Int64 questionId { get; set; }
        public Int64 questionTypeId { get; set; }
        public string questionName { get; set; }
        public List<OnlineAssessmentOptions> options { get;set;}
    }
    public class OnlineAssessmentOptions
    {
       public string optionName { get; set; }
       public Int64 optionId { get; set; }
       public Int64 questionId { get; set; }
       public bool isAnswer { get; set; }
    }
    public class OnlineAssessmentAnswers
    {
        public Int64 TestId { get; set; }
        public Int64 AnswerId { get; set; }
        public Int64 QuestionId { get; set; }
        public Int64 OptionId { get; set; }
        public bool isSelected { get; set; }
        public bool isAnswered { get; set; }
    }
}
