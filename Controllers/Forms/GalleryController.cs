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
    public class GalleryController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(GalleryEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@school", entity.school));
            sqlParameters.Add(new KeyValuePair<string, string>("@Date", entity.Date));
            sqlParameters.Add(new KeyValuePair<string, string>("@imagefilename", Convert.ToString(entity.imagefilename)));
            sqlParameters.Add(new KeyValuePair<string, string>("@title", Convert.ToString(entity.title)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertGallery", sqlParameters);
            return JsonConvert.SerializeObject(result);



        }
        [HttpGet("{id}")]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@school", SchoolID));
            ds = manageSQL.GetDataSetValues("GetGallery", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class GalleryEntity
    {
        public Int64 RowId { get; set; }
        public string school { get; set; }
        public string Date { get; set; }
        public string imagefilename { get; set; }
        public string title { get; set; }
        public bool Flag { get; set; }
    }
}