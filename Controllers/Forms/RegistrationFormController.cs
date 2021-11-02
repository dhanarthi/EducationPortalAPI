using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationPortalAPI.ManageSQL;
using System.Data;
using Newtonsoft.Json;
using EducationPortalAPI.Model;

namespace EducationPortalAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationFormController : ControllerBase
    {
        [HttpPost("{id}")]
        public Tuple<bool, int, string> Post(RegistrationFormEntity registrationForm = null)
        {
            ManageSQLConnection manageSQLConnection = new ManageSQLConnection();
            Security security = new Security();
            //Check the document Approval
            try
            {
                string entryptedPwd = security.Encryptword(registrationForm.Password);
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@ID", registrationForm.ID));
                sqlParameters.Add(new KeyValuePair<string, string>("@slno1", Convert.ToString(registrationForm.slno)));
                sqlParameters.Add(new KeyValuePair<string, string>("@FirstName", registrationForm.FirstName));
                sqlParameters.Add(new KeyValuePair<string, string>("@LastName", Convert.ToString(registrationForm.LastName)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DateofBirth", Convert.ToString(registrationForm.DateofBirth)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DateofJoining", Convert.ToString(registrationForm.DateofJoining)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Gender", Convert.ToString(registrationForm.Gender)));
                sqlParameters.Add(new KeyValuePair<string, string>("@BloodGroup", registrationForm.BloodGroup));
                sqlParameters.Add(new KeyValuePair<string, string>("@Nationality", registrationForm.Nationality));
                sqlParameters.Add(new KeyValuePair<string, string>("@ClassId", Convert.ToString(registrationForm.ClassId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@SectionId", registrationForm.SectionId));
                sqlParameters.Add(new KeyValuePair<string, string>("@Medium", registrationForm.Medium));
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentPhotoFileName", registrationForm.StudentPhotoFileName));
                sqlParameters.Add(new KeyValuePair<string, string>("@Caste", Convert.ToString(registrationForm.Caste)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Addressinfo", registrationForm.CurrentAddrress));
                sqlParameters.Add(new KeyValuePair<string, string>("@PermanentAddress", Convert.ToString(registrationForm.PermanentAddress)));
                sqlParameters.Add(new KeyValuePair<string, string>("@SchoolId", Convert.ToString(registrationForm.SchoolId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@PhoneNumber", registrationForm.PhoneNumber));
                sqlParameters.Add(new KeyValuePair<string, string>("@PhoneNumber", registrationForm.PhoneNumber));
                sqlParameters.Add(new KeyValuePair<string, string>("@AltNumber", registrationForm.AltNumber));
                sqlParameters.Add(new KeyValuePair<string, string>("@Nameoflastschool", registrationForm.Nameoflastschool));
                sqlParameters.Add(new KeyValuePair<string, string>("@LastchoolTelephone", Convert.ToString(registrationForm.LastchoolTelephone)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DistrictId", Convert.ToString(registrationForm.District)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Postalcode", Convert.ToString(registrationForm.Postalcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@RoleId", Convert.ToString(registrationForm.RoleId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@City", Convert.ToString(registrationForm.Taluk)));
                sqlParameters.Add(new KeyValuePair<string, string>("@State", Convert.ToString(registrationForm.State)));
                sqlParameters.Add(new KeyValuePair<string, string>("@UserId", "-"));
                sqlParameters.Add(new KeyValuePair<string, string>("@Password", registrationForm.Password));
                sqlParameters.Add(new KeyValuePair<string, string>("@Religion", registrationForm.Religion));
                sqlParameters.Add(new KeyValuePair<string, string>("@IncomeFilename", registrationForm.IncomeFilename));
                sqlParameters.Add(new KeyValuePair<string, string>("@NativityFilename", registrationForm.NativityFilename));
                sqlParameters.Add(new KeyValuePair<string, string>("@CommunityFilename", registrationForm.CommunityFilename));
                sqlParameters.Add(new KeyValuePair<string, string>("@Disability", registrationForm.Disability));
                sqlParameters.Add(new KeyValuePair<string, string>("@AadharNo", registrationForm.AadharNo));

                sqlParameters.Add(new KeyValuePair<string, string>("@FatherName", registrationForm.FatherName));
                sqlParameters.Add(new KeyValuePair<string, string>("@FatherOccupation", registrationForm.FatherOccupation));
                sqlParameters.Add(new KeyValuePair<string, string>("@FatherMobileNo", registrationForm.FatherMobileNo));
                sqlParameters.Add(new KeyValuePair<string, string>("@FatherEmailid", registrationForm.FatherEmailid));
                sqlParameters.Add(new KeyValuePair<string, string>("@FatherDesignation", registrationForm.FatherDesignation));
                sqlParameters.Add(new KeyValuePair<string, string>("@FatherQualification", registrationForm.FatherQualification));
                sqlParameters.Add(new KeyValuePair<string, string>("@FYearlyIncome", registrationForm.FatherYearlyIncome));
                sqlParameters.Add(new KeyValuePair<string, string>("@FatherPhotoFileName", registrationForm.FatherPhotoFileName));

                sqlParameters.Add(new KeyValuePair<string, string>("@MotherName", registrationForm.MotherName));
                sqlParameters.Add(new KeyValuePair<string, string>("@MotherOccupation", registrationForm.MotherOccupation));
                sqlParameters.Add(new KeyValuePair<string, string>("@MotherQualification", registrationForm.MotherQualification));
                sqlParameters.Add(new KeyValuePair<string, string>("@MotherDesignation", registrationForm.MotherDesignation));
                sqlParameters.Add(new KeyValuePair<string, string>("@MotherMobileNo", registrationForm.MotherMobileNo));
                sqlParameters.Add(new KeyValuePair<string, string>("@MotherEmailid", registrationForm.MotherEmailid));
                sqlParameters.Add(new KeyValuePair<string, string>("@MYearlyIncome", registrationForm.MotherYearlyIncome));
                sqlParameters.Add(new KeyValuePair<string, string>("@MotherPhotoFilName", registrationForm.MotherPhotoFilName));
               
                sqlParameters.Add(new KeyValuePair<string, string>("@GaurdianName", registrationForm.GaurdianName));
                sqlParameters.Add(new KeyValuePair<string, string>("@GaurdianEmailid", registrationForm.GaurdianEmailid));
                sqlParameters.Add(new KeyValuePair<string, string>("@GaurdianOccupation", registrationForm.GaurdianOccupation));
                sqlParameters.Add(new KeyValuePair<string, string>("@GaurdianDesignation", registrationForm.GaurdianDesignation));
                sqlParameters.Add(new KeyValuePair<string, string>("@GuardianQualification", registrationForm.GuardianQualification));
                sqlParameters.Add(new KeyValuePair<string, string>("@GaurdianMobileNo", registrationForm.GaurdianMobileNo));
                sqlParameters.Add(new KeyValuePair<string, string>("@GaurdianPhotoFileName", registrationForm.GaurdianPhotoFileName));

                sqlParameters.Add(new KeyValuePair<string, string>("@EncrptedPwd", entryptedPwd));

                var resut = manageSQLConnection.InsertData("InsertRegistration", sqlParameters, "slno");
                return resut;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("ex", ex);
                return new Tuple<bool, int, string>(false, 1, "");
            }
            finally
            {
                Console.WriteLine("finally!");
            }

        }

        [HttpGet("{id}")]
        public string Get(string Type,string Value)
        {
            ManageSQLConnection manageSQLConnection = new ManageSQLConnection();

            //Check the document Approval
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Type", Type));
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Value));
            var data = manageSQLConnection.GetDataSetValues("GetRegistration", sqlParameters);
            return JsonConvert.SerializeObject(data.Tables[0]);
        }
    }
    public class RegistrationFormEntity
    {

        public int slno { get; set; }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime DateofJoining { get; set; }
        public string Gender { get; set; }
        public string Medium { get; set; }
        public string Nationality { get; set; }
        public string BloodGroup { get; set; }
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public string StudentPhotoFileName { get; set; }
        public string Caste { get; set; }
        public string CurrentAddrress { get; set; }
        public string PermanentAddress { get; set; }
        public int SchoolId { get; set; }
        public string PhoneNumber { get; set; }
        public string AltNumber { get; set; }
        public string EmailId { get; set; }
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherQualification { get; set; }
        public string FatherDesignation { get; set; }
        public string FatherMobileNo { get; set; }
        public string FatherEmailid { get; set; }
        public string FatherYearlyIncome { get; set; }
        public string FatherPhotoFileName { get; set; }
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherDesignation { get; set; }
        public string MotherQualification { get; set; }
        public string MotherMobileNo { get; set; }
        public string MotherEmailid { get; set; }
        public string MotherYearlyIncome { get; set; }
        public string MotherPhotoFilName { get; set; }
        public string Nameoflastschool { get; set; }
        public string LastchoolTelephone { get; set; }
        public int District { get; set; }
        public string Postalcode { get; set; }
        public string LanguageSpoken { get; set; }
        public string GaurdianName { get; set; }
        public string GaurdianOccupation { get; set; }
        public string GaurdianDesignation { get; set; }
        public string GuardianQualification { get; set; }
        public string GaurdianMobileNo { get; set; }
        public string GaurdianEmailid { get; set; }
        public string GaurdianPhotoFileName { get; set; }
        public int RoleId { get; set; }
        public string UserId { get; set; }
        public string Taluk { get; set; }
        public string State { get; set; }
        public string Password { get; set; }
        public string Religion { get; set; }
        public string IncomeFilename { get; set; }
        public string NativityFilename { get; set; }
        public string CommunityFilename { get; set; }
        public string Disability { get; set; }
        public string AadharNo { get; set; }
    }
}
