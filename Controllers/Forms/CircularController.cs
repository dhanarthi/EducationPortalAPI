using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using EducationPortalAPI.ManageSQL;

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircularController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(CircularEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", entity.SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@CircularDate", Convert.ToString(entity.CircularDate)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Subject", entity.Subject));
            sqlParameters.Add(new KeyValuePair<string, string>("@Details", entity.Details));
            sqlParameters.Add(new KeyValuePair<string, string>("@Download", entity.Download));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertCirculars", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            ds = manageSQL.GetDataSetValues("GetCirculars", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }


    public class CircularEntity
    {
        public Int64 RowId { get; set; }
        public string SchoolID { get; set; }
        public DateTime CircularDate { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string Download { get; set; }
        public bool Flag { get; set; }
    }
}
