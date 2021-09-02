
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
    public class StudentAssignmentDetilsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(StudentAssignmenEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AssignId", Convert.ToString(entity.AssignId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@AssignFileName", entity.AssignFileName));
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@StudentID", Convert.ToString(entity.StudentID)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertStudentAssignmentStaus", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string Get(string SchoolID, string Class, int StudentID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@Class", Class));
            sqlParameters.Add(new KeyValuePair<string, string>("@StudentID", Convert.ToString(StudentID)));
            ds = manageSQL.GetDataSetValues("GetStudentAssignmentDetails", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class StudentAssignmenEntity
    {
        public Int64 RowId { get; set; }
        public Int64 AssignId { get; set; }
        public string AssignFileName { get; set; }
        public bool Flag { get; set; }
        public int StudentID { get; set; }
    }
}
