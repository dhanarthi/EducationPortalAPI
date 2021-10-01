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
    public class BooksController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(BooksEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", entity.SchoolId));
            sqlParameters.Add(new KeyValuePair<string, string>("@Pdffilename", Convert.ToString(entity.Pdffilename)));
            sqlParameters.Add(new KeyValuePair<string, string>("@authorReference", entity.authorReference));
            sqlParameters.Add(new KeyValuePair<string, string>("@subjects", entity.subjects));
            sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", Convert.ToString(entity.ClassId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Years",  entity.Years));
            sqlParameters.Add(new KeyValuePair<string, string>("@medium", Convert.ToString(entity.medium)));
            var result = manageSQL.InsertData("InsertBooks", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            ds = manageSQL.GetDataSetValues("GetBooks", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class BooksEntity
    {
        public Int64 RowId { get; set; }
        public string SchoolId { get; set; }
        public int ClassId { get; set; }
        public string subjects { get; set; }
        public string authorReference { get; set; }
        public string Pdffilename { get; set; }
        public bool Flag { get; set; }
        
        public string Years { get; set; }

        public string medium { get; set; }
    }
}
