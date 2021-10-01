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
    public class NomineesController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(NomineeEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", entity.SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@ElectionID", Convert.ToString(entity.ElectionID)));
            sqlParameters.Add(new KeyValuePair<string, string>("@NomineeID", entity.NomineeID));
            sqlParameters.Add(new KeyValuePair<string, string>("@ElectionName", entity.ElectionName));
            sqlParameters.Add(new KeyValuePair<string, string>("@ElectionDate", entity.ElectionDate));
            sqlParameters.Add(new KeyValuePair<string, string>("@ElectionDate", entity.ElectionDate));
            sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", entity.ClassId));
            sqlParameters.Add(new KeyValuePair<string, string>("@SectionId", entity.SectionId));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertNominee", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID, string ElectionID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@ElectionID", ElectionID));
            ds = manageSQL.GetDataSetValues("GetNominee", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class NomineeEntity
    {
        public Int64 RowId { get; set; }
        public string SchoolID { get; set; }
        public string ElectionID { get; set; }
        public string NomineeID { get; set; }
        public string ElectionName { get; set; }
        public string ElectionDate { get; set; }
        public string ClassId { get; set; }

        public string SectionId { get; set; }
        public bool Flag { get; set; }
    }
}
