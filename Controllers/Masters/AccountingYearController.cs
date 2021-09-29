using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPortalAPI.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingYearController : Controller
    {
        [HttpGet]
        public string Get()
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            ds = manageSQL.GetDataSetValues("GetAccountYear");
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
}