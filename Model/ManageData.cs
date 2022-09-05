using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace EducationPortalAPI.Model
{
    public class ManageData
    {
        /// <summary>
        /// Check the Data availability
        /// </summary>
        /// <param name="ds">dataset value</param>
        /// <returns></returns>
        public bool CheckDataAvailable(DataSet ds)
        {
            bool isAvailable = false;

            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        isAvailable = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError("CheckData : " + ex.Message + " : " + ex.StackTrace);
            }
            return isAvailable;
        }

        /// <summary>
        /// Create a new folder 
        /// </summary>
        /// <param name="Path">Folder path</param>
        public bool CheckFolderAvailable(string Path)
        {
            bool isAvailable = false;
            if (Directory.Exists(Path))
            {
                isAvailable = true;
            }
            return isAvailable;
        }

        /// <summary>
        /// Check the Data availability
        /// </summary>
        /// <param name="dt">datatable value</param>
        /// <returns></returns>
        public bool CheckDataAvailable(DataTable dt)
        {
            bool isAvailable = false;

            try
            {
                if (dt.Rows.Count > 0)
                {
                    isAvailable = true;
                }

            }
            catch (Exception ex)
            {
                AuditLog.WriteError("CheckData : " + ex.Message + " : " + ex.StackTrace);
            }
            return isAvailable;
        }

    }
}
