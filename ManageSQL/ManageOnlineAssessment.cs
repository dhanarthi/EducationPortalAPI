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
                sqlCommand.Parameters.AddWithValue("@Classcode", entity.Classcode);
                sqlCommand.Parameters.AddWithValue("@totalmarks", entity.TotalMarks);
                sqlCommand.Parameters.AddWithValue("@totalduration", entity.TotalDuration);
                sqlCommand.Parameters.AddWithValue("@durationtype", entity.DurationType);
                sqlCommand.Parameters.AddWithValue("@SubjectId", entity.SubjectId);
                sqlCommand.Parameters.AddWithValue("@questiontype", entity.QuestionType);
                sqlCommand.Parameters.AddWithValue("@Flag", entity.Flag);
                sqlCommand.Parameters.AddWithValue("@AssessmentDate", entity.AssessmentDate);
                sqlCommand.Parameters.Add("@RowId", SqlDbType.BigInt, 13);
                sqlCommand.Parameters["@RowId"].Direction = ParameterDirection.Output;
                sqlCommand.ExecuteNonQuery();
                int AssessmentRowid = (int)(long)sqlCommand.Parameters["@RowId"].Value;

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
                    sqlCommand.Parameters.AddWithValue("@QuestionId1", item.questionId);
                    sqlCommand.Parameters.AddWithValue("@QuestionDetails", item.questionName);
                    sqlCommand.Parameters.Add("@QuestionId", SqlDbType.BigInt, 13);
                    sqlCommand.Parameters["@QuestionId"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    int QuestionId= (int)(long)sqlCommand.Parameters["@QuestionId"].Value;

                    foreach (var Option in item.options)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Dispose();

                        sqlCommand = new SqlCommand();
                        sqlCommand.Transaction = objTrans;
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "InsertOnlineAssessmentOptions";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@AnswerId", Option.optionId);
                        sqlCommand.Parameters.AddWithValue("@QuestionId", QuestionId);
                        sqlCommand.Parameters.AddWithValue("@OptionName", Option.optionName);
                        sqlCommand.Parameters.AddWithValue("@IsAnswer", Option.isAnswer);  
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
