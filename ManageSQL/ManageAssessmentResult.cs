using EducationPortalAPI.Controllers.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortalAPI.ManageSQL
{
    public class ManageAssessmentResult
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();

        public bool InsertData(List<OnlineAssessmentAnswers> entity)
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
                sqlCommand.CommandText = "InsertOnlineAssessmentAnswer";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (var e in entity)
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();
                    sqlCommand.Parameters.AddWithValue("@AnswerId", e.AnswerId);
                    sqlCommand.Parameters.AddWithValue("@OptionId", e.OptionId);
                    sqlCommand.Parameters.AddWithValue("@TestId", e.TestId);
                    sqlCommand.Parameters.AddWithValue("@QuestionId", e.QuestionId);
                    sqlCommand.Parameters.AddWithValue("@isAnswered", e.isAnswered);
                    sqlCommand.Parameters.AddWithValue("@isSelected", e.isSelected);
                    sqlCommand.Parameters.AddWithValue("@StudentId", e.StudentId);
                    sqlCommand.ExecuteNonQuery();
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
  
            }
        }
    }
}
