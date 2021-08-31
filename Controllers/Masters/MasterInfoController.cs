using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.ManageSQL;
using System.Data;
using Newtonsoft.Json;

namespace EducationPortalAPI.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterInfoController : ControllerBase
    {
        
        [HttpGet]
        public string Get()
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            ds = manageSQL.GetDataSetValues("GetMasterDetails");
            return JsonConvert.SerializeObject(ds);
        }
    }
}
