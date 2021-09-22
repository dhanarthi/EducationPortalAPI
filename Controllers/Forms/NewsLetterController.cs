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
    public class NewsLetterController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(NewsLetterEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", entity.SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@Topic", Convert.ToString(entity.Topic)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Download", entity.Download));
            sqlParameters.Add(new KeyValuePair<string, string>("@NewsDate",  entity.NewsDate));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertNewsLetter", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            ds = manageSQL.GetDataSetValues("GetNewsLetter", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class NewsLetterEntity
    {
        public Int64 RowId { get; set; }
        public string SchoolID { get; set; }
        public string Topic { get; set; }
        public string Download { get; set; }
        public bool Flag { get; set; }

        public string NewsDate { get; set; }

    }
}
