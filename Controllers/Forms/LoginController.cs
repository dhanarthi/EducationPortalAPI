using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.Model;
using System.Data;

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("{id}")]
        public Tuple<bool, string, DataTable> Get(int Type, string Value, int RoleId, string Password)
        {
            Security security = new Security();
            string RoleName = string.Empty;
            try
            {
                RoleName = RoleId == 6 ? "Student" : RoleId == 5 ? "Staff" : "Admin";
                ManageSQLConnection manageSQLConnection = new ManageSQLConnection();

                //Check the document Approval
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Type",Convert.ToString(Type)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Value)); //2
                var data = manageSQLConnection.GetDataSetValues("GetRegistration", sqlParameters);
                if (Type == 2) // check the 
                {
                    if(data.Tables.Count>0)
                    {
                        if(data.Tables[0].Rows.Count>0)
                        {
                            if(Convert.ToInt32(data.Tables[0].Rows[0]["RoleId"])  == RoleId)
                            {
                                if(Convert.ToString(data.Tables[0].Rows[0]["EncrptedPwd"]) == security.Encryptword(Password))
                                {
                                    return new Tuple<bool, string, DataTable>(true, "Login Successfully ", data.Tables[0]);

                                }
                                else
                                {
                                    return new Tuple<bool, string, DataTable>(false, "Password Incorrect, Pleas enter correct password" , null);
                                }
                            }
                            else
                            {
                                return new Tuple<bool, string, DataTable>(false, "You are not authorized to login as a  " + RoleName, null);
                            }
                        }
                        else
                        {
                            return new Tuple<bool, string, DataTable>(false, "Invalid User Name", null);
                        }
                    }
                    else
                    {
                        return new Tuple<bool, string, DataTable>(false, "Invalid User Name", null);
                    }
                }
                return new Tuple<bool, string, DataTable> (false," Please try later", null);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return new Tuple<bool, string, DataTable>(false, "Please try later", null);
            }

        }
    }
}
