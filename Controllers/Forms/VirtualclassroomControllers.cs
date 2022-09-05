﻿using EducationPortalAPI.ManageSQL;
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
    public class VirtualclassroomController : ControllerBase
    {

        [HttpPost("{id}")]
        public string Post(VirtualclassroomEntity entity)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", entity.SchoolId));
            sqlParameters.Add(new KeyValuePair<string, string>("@vdofilename", Convert.ToString(entity.vdofilename)));
            sqlParameters.Add(new KeyValuePair<string, string>("@authorReference", entity.authorReference));
            sqlParameters.Add(new KeyValuePair<string, string>("@SubjectId", entity.SubjectId));
            sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", Convert.ToString(entity.ClassId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
            //  sqlParameters.Add(new KeyValuePair<string, string>("@medium", Convert.ToString(entity.medium)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Years", entity.Years));
            sqlParameters.Add(new KeyValuePair<string, string>("@medium", Convert.ToString(entity.medium)));
            var result = manageSQL.InsertData("Insertvirtualclassrooms", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet("{id}")]       
        public string Get(string SchoolID, string ClassId, string Medium)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolID));
            sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", ClassId));
            sqlParameters.Add(new KeyValuePair<string, string>("@MediumId", Medium));
            ds = manageSQL.GetDataSetValues("GetVirtualclassrooms", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
    }
    public class VirtualclassroomEntity
    {
        public string RowId { get; set; }
        public string SchoolId { get; set; }
        public int ClassId { get; set; }
        public string SubjectId { get; set; }
        public string authorReference { get; set; }
        public string vdofilename { get; set; }
        public bool Flag { get; set; }
        // public int medium { get; set; }
        public string Years { get; set; }

        public string medium { get; set; }
    }
}
