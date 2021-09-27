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
    public class AssignmentsController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(AssignmentEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AssignId", Convert.ToString(entity.AssignId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", entity.SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@Class", Convert.ToString(entity.Class)));
            sqlParameters.Add(new KeyValuePair<string, string>("@AssignmentDate", entity.AssignmentDate));
            sqlParameters.Add(new KeyValuePair<string, string>("@AssignmentDueDate", entity.AssignmentDueDate));
            sqlParameters.Add(new KeyValuePair<string, string>("@AssignmentWork", entity.AssignmentWork));
            sqlParameters.Add(new KeyValuePair<string, string>("@AssignmentType", entity.AssignmentType));
            sqlParameters.Add(new KeyValuePair<string, string>("@Subjectname", entity.Subjectname));
            sqlParameters.Add(new KeyValuePair<string, string>("@Assignmentfilename", entity.Assignmentfilename));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertAssignments", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID, string Class)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@Class", Class));
            ds = manageSQL.GetDataSetValues("GetAssignments", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class AssignmentEntity
    {
        public Int64 AssignId { get; set; }
        public string SchoolID { get; set; }
        public string Class { get; set; }
        public string AssignmentDate { get; set; }
        public string AssignmentDueDate { get; set; }
        public string AssignmentWork { get; set; }
        public string AssignmentType { get; set; }
        public string Subjectname { get; set; }
        public string Assignmentfilename { get; set; }
        public bool Flag { get; set; }
    }
}
