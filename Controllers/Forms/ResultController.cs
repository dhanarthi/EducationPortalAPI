using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.ManageSQL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();
        [HttpPost("{id}")]
        public bool Post([FromBody]List<ResultEntity> entity)
        {
            SqlTransaction objTrans = null;
            using (sqlConnection = new SqlConnection(GlobalVariable.ConnectionString))
            {
                sqlCommand = new SqlCommand();
                try
                {
                    if (sqlConnection.State == 0)
                    {
                        sqlConnection.Open();
                    }
                    objTrans = sqlConnection.BeginTransaction();

                    foreach (var item in entity)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Dispose();
                        sqlCommand = new SqlCommand();
                        sqlCommand.Transaction = objTrans;
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "InsertResult";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@RowId", item.RowId);
                        sqlCommand.Parameters.AddWithValue("@ExamTypeId", item.ExamTypeId);
                        sqlCommand.Parameters.AddWithValue("@ClassId", item.ClassId);
                        sqlCommand.Parameters.AddWithValue("@SectionId", item.SectionId);
                        sqlCommand.Parameters.AddWithValue("@SubjectId", item.SubjectId);
                        sqlCommand.Parameters.AddWithValue("@Date", item.ExamDate);
                        sqlCommand.Parameters.AddWithValue("@StudentId", item.StudentId);
                        sqlCommand.Parameters.AddWithValue("@TotalMarks", item.TotalMarks);
                        sqlCommand.Parameters.AddWithValue("@MarksScored", item.MarksScored);
                        sqlCommand.Parameters.AddWithValue("@Topic", item.Topic);
                        sqlCommand.Parameters.AddWithValue("@UserId", item.UserId);
                        sqlCommand.Parameters.AddWithValue("@SchoolId", item.SchoolId);
                        sqlCommand.Parameters.AddWithValue("@ShortYear", item.ShortYear);
                        sqlCommand.ExecuteNonQuery();
                    }
                    objTrans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    AuditLog.WriteError(ex.Message);
                    objTrans.Rollback();
                    return false;
                }
                finally
                {
                    sqlConnection.Close();
                    sqlCommand.Dispose();
                }

            }
        }

        [HttpGet("{id}")]
        public string Get(string SchoolId, string UserId, string Date)
        {
            try
            {
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                sqlParameters.Add(new KeyValuePair<string, string>("@SchoolID", SchoolId));
                sqlParameters.Add(new KeyValuePair<string, string>("@UserId", UserId));
                sqlParameters.Add(new KeyValuePair<string, string>("@Date", Date));
                var result = manageSQL.GetDataSetValues("GetResultDetails", sqlParameters);
                return JsonConvert.SerializeObject(result.Tables[0]);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return "";
            }

        }

        [HttpPut("{id}")]
        public bool Put([FromBody]string Index)
        {
            try
            {
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Index));
                return manageSQL.UpdateValues("DeleteExamResult", sqlParameters);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }

    }

    public class ResultEntity
    {
        public Int64 RowId { get; set; }
        public int ExamTypeId { get; set; }
        public string ExamDate { get; set; }
        public int SectionId { get; set; }
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string Topic { get; set; }
        public int TotalMarks { get; set; }
        public int MarksScored { get; set; }
        public int UserId { get; set; }
        public int SchoolId { get; set; }
        public int ShortYear { get; set; }
    }
    }
