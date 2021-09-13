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
            sqlParameters.Add(new KeyValuePair<string, string>("@duedate", Convert.ToString(entity.duedate)));
            sqlParameters.Add(new KeyValuePair<string, string>("@ActualAmount", entity.ActualAmount));
            sqlParameters.Add(new KeyValuePair<string, string>("@PaidAmount", entity.PaidAmount));
            sqlParameters.Add(new KeyValuePair<string, string>("@OutstandingAmount", entity.OutstandingAmount));
            sqlParameters.Add(new KeyValuePair<string, string>("@PayingAmount", entity.PayingAmount));
            sqlParameters.Add(new KeyValuePair<string, string>("@FineAmount", entity.FineAmount));
            sqlParameters.Add(new KeyValuePair<string, string>("@ReceiptBook", entity.ReceiptBook));
            sqlParameters.Add(new KeyValuePair<string, string>("@FeeName", entity.FeeName));
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
        public string ActualAmount { get; set; }
        public string PaidAmount { get; set; }
        public string OutstandingAmount { get; set; }
        public string PayingAmount { get; set; }
        public string FineAmount { get; set; }
        public string ReceiptBook { get; set; }
        public string FeeName { get; set; }
        public bool Flag { get; set; }
    }
}
