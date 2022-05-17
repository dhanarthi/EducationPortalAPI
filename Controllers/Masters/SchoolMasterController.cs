using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Http;
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
    public class SchoolMasterController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(SchoolMasterEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Districcode", Convert.ToString(entity.Districcode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Talukcode", Convert.ToString(entity.Talukcode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Catagorycode", Convert.ToString(entity.Catagorycode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Schoolcode", entity.Schoolcode));
            sqlParameters.Add(new KeyValuePair<string, string>("@Schoolname", Convert.ToString(entity.Schoolname)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Schooladd", Convert.ToString(entity.Schooladd)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Schoolpincode", entity.Schoolpincode));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertSchoolMaster", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string Districcode, string Talukcode, string Catagorycode, string Schoolcode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Districcode", Districcode));
            sqlParameters.Add(new KeyValuePair<string, string>("@Talukcode", Talukcode));
           // sqlParameters.Add(new KeyValuePair<string, string>("@Catagorycode", Catagorycode));
            //sqlParameters.Add(new KeyValuePair<string, string>("@Schoolcode", Schoolcode));
            ds = manageSQL.GetDataSetValues("GetSchoolMaster", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        [HttpPut("{id}")]
        public bool Put([FromBody] string Index)
        {
            try
            {
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                sqlParameters.Add(new KeyValuePair<string, string>("@nslno", Index));
                return manageSQL.UpdateValues("DeleteSchoolMaster", sqlParameters);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
    }
    public class SchoolMasterEntity
    {
        public string Districcode { get; set; }
        public string Talukcode { get; set; }
        public string Catagorycode { get; set; }
        public string Schoolcode { get; set; }
        public string Schoolname { get; set; }
        public string Schooladd { get; set; }
        public bool Flag { get; set; }
        public string Schoolpincode { get; set; }

      
    }
}
