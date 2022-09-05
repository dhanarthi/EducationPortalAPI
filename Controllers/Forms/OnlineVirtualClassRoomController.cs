using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.Model;

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineVirtualClassRoomController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string Schoolcode, string ClassId, string SectionId, string dataFormat)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Schoolcode", Schoolcode));
            sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", ClassId));
            sqlParameters.Add(new KeyValuePair<string, string>("@SectionId", SectionId));
            ds = manageSQL.GetDataSetValues("GetDeviceMapping", sqlParameters);
            ManageData _manage = new ManageData();
            if (_manage.CheckDataAvailable(ds))
            {
                string rootPath = GlobalVariable.FolderPath + "ClassRoom//" + Schoolcode + "//" + ClassId + "//" + SectionId + "//" + ds.Tables[0].Rows[0]["Foldername"].ToString() + "//" + dataFormat;

                if (_manage.CheckFolderAvailable(rootPath))
                {
                    string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);
                    List<FolderData> fData = new List<FolderData>();
                    foreach (string dir in dirs)
                    {
                        FolderData _fData = new FolderData();
                        _fData.CamFolder = ds.Tables[0].Rows[0]["foldername"].ToString();
                        _fData.FolderName = new DirectoryInfo(dir).Name;
                        DirectoryInfo di = new DirectoryInfo(rootPath + "//" + _fData.FolderName);
                        FileInfo[] files = di.GetFiles("*.mp4");
                        List<VideoFilenames> filenames = new List<VideoFilenames>();
                        foreach (FileInfo file in files)
                        {
                            VideoFilenames _filenames = new VideoFilenames();
                            _filenames.Filename = file.Name;
                            filenames.Add(_filenames);
                        }
                        _fData.Filenames = filenames;
                        fData.Add(_fData);
                    }
                    return JsonConvert.SerializeObject(fData);
                }
            }
            return null;
        }

    }
    public class FolderData
    {
        public string FolderName { get; set; }
        public string CamFolder { get; set; }
        public List<VideoFilenames> Filenames { get; set; }
    }

    public class VideoFilenames
    {
        public string Filename { get; set; }
    }


    public class OnlineVirtualClassRoomControllerEntity
    {
        public string Id { get; set; }
        public string Districcode { get; set; }
        public int Schoolname { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int Cameraserialno { get; set; }
        public int foldername { get; set; }
        public int Schoolcode { get; set; }
        public int Talukid { get; set; }
        public int Subjectid { get; set; }

    }
}