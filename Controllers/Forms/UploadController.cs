    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;
    //using System.Web.Http;
    using Microsoft.AspNetCore.Http;



    namespace EducationPortalAPI.Controllers.Forms
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UploadController : ControllerBase
        {

            [HttpPost]
            [Route("AddFileDetails")]
            public Tuple<bool,string> AddFile()
            {
                try
                {
                    var file = Request.Form.Files[0];
                    //var sPath = Convert.ToString(Request.Form.Keys.Count[0]); //(new System.Collections.Generic.IDictionaryDebugView<string, Microsoft.Extensions.Primitives.StringValues>(((System.Collections.Generic.Dictionary<string, Microsoft.Extensions.Primitives.StringValues>.KeyCollection)((Microsoft.AspNetCore.Http.FormCollection)Request.Form).Keys)._dictionary).Items[0]).Value;


                    if (file.Length > 0)
                    {
                        var files = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var value = files.Split('^');
                    var fileName = value[0];
                    var folderName = value[1];
                    var folder = GlobalVariable.FolderPath + folderName; // Path.Combine("Resources", folderName);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folder);
                    var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);

                        }
                        return new Tuple<bool, string>(true, fileName);
                        // return Ok(new { dbPath });
                    }
                    else
                    {
                        //return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                AuditLog.WriteError(ex.Message);
                    //  return StatusCode(500, $"Internal server error: {ex}");
                }
            return new Tuple<bool, string>(false, "");

                    }
    }
}

