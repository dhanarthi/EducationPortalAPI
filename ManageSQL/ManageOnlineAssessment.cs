using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.Controllers.Forms;

namespace EducationPortalAPI.ManageSQL
{
    public class ManageOnlineAssessment
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();

        SqlDataAdapter dataAdapter;
 
        public bool InsertData(OnlineAssessmentMasterEntity entity)
        {
            SqlTransaction objTrans = null;
            sqlConnection = new SqlConnection(GlobalVariable.ConnectionString);
            DataSet ds = new DataSet();
            sqlCommand = new SqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                objTrans = sqlConnection.BeginTransaction();
                sqlCommand.Transaction = objTrans;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "InsertOnlineAssessmentMaster";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@RowId1", entity.RowId);
                sqlCommand.Parameters.AddWithValue("@TestName", entity.TestName);
                sqlCommand.Parameters.AddWithValue("@TestDescription", entity.TestDescription);
                sqlCommand.Parameters.AddWithValue("@SchoolId", entity.SchoolId);
                sqlCommand.Parameters.AddWithValue("@RowId", entity.RowId);
                sqlCommand.Parameters.AddWithValue("@Flag", entity.Flag);
                sqlCommand.Parameters.AddWithValue("@AssessmentDate", entity.AssessmentDate);
                sqlCommand.Parameters.Add("@RowId", SqlDbType.BigInt, 13);
                sqlCommand.Parameters["@RowId"].Direction = ParameterDirection.Output;
                sqlCommand.ExecuteNonQuery();
                int AssessmentRowid = (int)sqlCommand.Parameters["@RowId"].Value;

                sqlCommand.Parameters.Clear();
                sqlCommand.Dispose();

                foreach (var item in entity.Questions)
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();

                    sqlCommand = new SqlCommand();
                    sqlCommand.Transaction = objTrans;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "InsertOnlineAssessmentQuestions";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@RowId", AssessmentRowid);
                    sqlCommand.Parameters.AddWithValue("@QuestionId", item.QuestionId);
                    sqlCommand.Parameters.AddWithValue("@QuestionDetails", item.QuestionDetails);
                    sqlCommand.Parameters.Add("@QuestionId", SqlDbType.BigInt, 13);
                    sqlCommand.Parameters["@QuestionId"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    int QuestionId= (int)sqlCommand.Parameters["@QuestionId"].Value;

                    foreach (var Option in item.Options)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Dispose();

                        sqlCommand = new SqlCommand();
                        sqlCommand.Transaction = objTrans;
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "InsertOnlineAssessmentOptions";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@AnswerId", Option.AnswerId);
                        sqlCommand.Parameters.AddWithValue("@QuestionId", QuestionId);
                        sqlCommand.Parameters.AddWithValue("@IsAnswer", Option.IsAnswer);  
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                sqlCommand.Parameters.Clear();
                sqlCommand.Dispose();
                objTrans.Commit();
                return true;

            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }
    }
}
