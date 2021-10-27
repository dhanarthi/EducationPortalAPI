using Microsoft.AspNetCore.Http;
using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(FeesEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Academic", Convert.ToString(entity.Academic)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", entity.SchoolId));
            sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", entity.StudentId));
            sqlParameters.Add(new KeyValuePair<string, string>("@duedate", entity.duedate));
            sqlParameters.Add(new KeyValuePair<string, string>("@ActualAmount", Convert.ToString(entity.ActualAmount)));
            sqlParameters.Add(new KeyValuePair<string, string>("@PaidAmount", Convert.ToString(entity.PaidAmount)));
            sqlParameters.Add(new KeyValuePair<string, string>("@OutstandingAmount", Convert.ToString(entity.OutstandingAmount)));
            sqlParameters.Add(new KeyValuePair<string, string>("@PayingAmount", Convert.ToString(entity.PayingAmount)));
            sqlParameters.Add(new KeyValuePair<string, string>("@FineAmount", Convert.ToString(entity.FineAmount)));
            sqlParameters.Add(new KeyValuePair<string, string>("@ReceiptBook", entity.ReceiptBook));
            sqlParameters.Add(new KeyValuePair<string, string>("@FeeName", entity.FeeName));
            sqlParameters.Add(new KeyValuePair<string, string>("@PayMethod", "Offline"));
            sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", entity.ClassId));
            sqlParameters.Add(new KeyValuePair<string, string>("@SectionId", entity.SectionId));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            var result = manageSQL.InsertData("InsertFee", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]
        public string Get(string SchoolID)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            ds = manageSQL.GetDataSetValues("GetFee", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class FeesEntity
    {
        public Int64 RowId { get; set; }
        public string Academic { get; set; }
        public string SchoolId { get; set; }
        public string StudentId { get; set; }
        public string duedate { get; set; }
        public decimal ActualAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public decimal PayingAmount { get; set; }
        public decimal FineAmount { get; set; }
        public string ReceiptBook { get; set; }
        public string FeeName { get; set; }

        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public bool Flag { get; set; }
    }
}
