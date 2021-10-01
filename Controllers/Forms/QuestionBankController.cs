using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionBankController : ControllerBase
    {
        [HttpPost("{id}")]
        public bool Post(QuestionBankEntity entity)
        {
            try
            {
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", entity.SchoolID));
                sqlParameters.Add(new KeyValuePair<string, string>("@QuestionYear", Convert.ToString(entity.QuestionYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Classcode", Convert.ToString(entity.Classcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@subject", entity.subject));
                sqlParameters.Add(new KeyValuePair<string, string>("@FileName", entity.FileName));
                sqlParameters.Add(new KeyValuePair<string, string>("@Description", entity.Description));
                sqlParameters.Add(new KeyValuePair<string, string>("@Medium", Convert.ToString(entity.Medium)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Publishdate", Convert.ToString(entity.Publishdate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                return manageSQL.InsertData("InsertQuestionBank", sqlParameters);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }

        [HttpGet("{id}")]
        public string Get(string SchoolID, int QuestionYear, int Classcode)
        {
            try
            {
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
                sqlParameters.Add(new KeyValuePair<string, string>("@QuestionYear", Convert.ToString(QuestionYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Classcode", Convert.ToString(Classcode)));
                var result = manageSQL.GetDataSetValues("GetQuestionBank", sqlParameters);
                return JsonConvert.SerializeObject(result.Tables[0]);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return "";
            }

        }

        [HttpPut("{id}")]
        public bool Put([FromBody]string Index)
        {
            try
            {
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Index));
                return manageSQL.UpdateValues("DeleteQuestionBank", sqlParameters);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
    }
    public class QuestionBankEntity
    {
        public Int64 RowId { get; set; }
        public string SchoolID { get; set; }
        public int QuestionYear { get; set; }
        public int Classcode { get; set; }
        public string subject { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public int Medium { get; set; }
        public DateTime Publishdate { get; set; }
        public bool Flag { get; set; }
    }
}
