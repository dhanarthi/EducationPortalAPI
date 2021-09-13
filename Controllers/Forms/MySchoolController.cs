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
    public class MySchoolController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(MySchoolEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Slno", Convert.ToString(entity.Slno)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", entity.SchoolId));
            sqlParameters.Add(new KeyValuePair<string, string>("@Curriculum", entity.Curriculum));
            sqlParameters.Add(new KeyValuePair<string, string>("@HMName", entity.HMName));
            sqlParameters.Add(new KeyValuePair<string, string>("@Emailid", entity.Emailid));
            sqlParameters.Add(new KeyValuePair<string, string>("@Addressinfo", entity.Addressinfo));
            sqlParameters.Add(new KeyValuePair<string, string>("@Phone", entity.Phone));
            sqlParameters.Add(new KeyValuePair<string, string>("@Pincode", entity.Pincode));
            sqlParameters.Add(new KeyValuePair<string, string>("@Landline", entity.Landline));
            sqlParameters.Add(new KeyValuePair<string, string>("@Fax", entity.Fax));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertMySchool", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", SchoolID));
            ds = manageSQL.GetDataSetValues("GetMySchool", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class MySchoolEntity
    {
       public Int64 Slno            {get;set;}
       public string SchoolId       {get;set;}
       public string Curriculum     {get;set;}
       public string HMName         {get;set;}
       public string Emailid        {get;set;}
       public string Addressinfo    {get;set;}
       public string Phone          {get;set;}
       public string Pincode        {get;set;}
       public string Landline       {get;set;}
       public string Fax            {get;set;}
       public bool Flag { get; set; }
    }
}
