using EducationPortalAPI.Controllers.Zoom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace EducationPortalAPI.ManageSQL
{
    public class ManageZoomSql
    {
        public bool InsertData(MeettingEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@RowId", Convert.ToString(entity.RowId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@MeetingId", entity.MeetingId));
                sqlParameters.Add(new KeyValuePair<string, string>("@MeetingURL", entity.MeetingURL));
                sqlParameters.Add(new KeyValuePair<string, string>("@Passcode", entity.Passcode));
                sqlParameters.Add(new KeyValuePair<string, string>("@Topics", entity.Topics));
                sqlParameters.Add(new KeyValuePair<string, string>("@Duration", Convert.ToString(entity.Duration)));
                sqlParameters.Add(new KeyValuePair<string, string>("@MeetingTime", Convert.ToString(entity.MeetingTime)));
                sqlParameters.Add(new KeyValuePair<string, string>("@MeetingDate", Convert.ToString(entity.MeetingDate)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", Convert.ToString(entity.ClassId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", entity.SchoolId));
                sqlParameters.Add(new KeyValuePair<string, string>("@SectionCode", Convert.ToString(entity.SectionCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostEmail", Convert.ToString(entity.HostEmail)));
                sqlParameters.Add(new KeyValuePair<string, string>("@StartURL", "")); //entity.StartURL
                sqlParameters.Add(new KeyValuePair<string, string>("@CreatedBy", entity.CreatedBy));
               AuditLog.WriteError(entity.MeetingId  + " : " + entity.StartURL);
                var result = manageSQL.InsertData("InsertMeetingInfo", sqlParameters);
              //  AuditLog.WriteError(result.ToString());
                return true;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }
            finally
            {
            }
        }
    }
}
