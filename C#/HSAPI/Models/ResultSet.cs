using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HSAPI.Models
{
    public enum ResultCode
    {
        Success, Duplicate, Failure, NotFound, ConflictedData, Required, RecordExist
    }

    /// <summary>
    /// 
    /// </summary>
    public class Messages
    {
        public static string RECORD_ADDED = "Added successfully!";
        public static string RECORD_UPDATED = "Updated successfully!";
        public static string RECORD_DELETED = "Deleted successfully!";
        public static string FIELD_VALUE_REQUIRED = "Field values required.";
        public static string RECORD_NOT_FOUND = "Record not found.";
        public static string DUPLICATE_RECORD = "Duplicate record.";
        public static string USER_ALREADY_EXIST = "User already exist.";
        public static string INVALID_USER_NAME_OR_PASSWORD = "Username or password is invalid.";

        public static string CAN_NOT_DELETE = "Can not delete record, as its is being used in other records.";

        public static string SMS_VERIFIED = "SMS code verified";

        public static string SMS_GATEWAY_USER_NAME = "sharpthinker";

        public static string SMS_GATEWAY_PASSWORD = "IdCfVYdIMCRGMc";

        public static string REJECT_REASON = "Please provide rejection reason.";
    }

    /// <summary>
    /// 
    /// </summary>

    public class Utility
    {
        public static int EMPLOYEE_STATUS_INCOMPLETE_DATA = 1;
        public static int EMPLOYEE_STATUS_READY_FOR_APPROVAL = 2;
        public static int EMPLOYEE_STATUS_APPROVAL_REQUESTED = 3;

        public static int EMPLOYEE_STATUS_APPROVAL_REJECTED = 5;
        public static int EMPLOYEE_STATUS_RELEASE_REJECTED = 7;
        public static int EMPLOYEE_STATUS_BLOCKED = 8;

        public static int USER_ROLE_ADMIN = 1;
        public static int USER_ROLE_CLERK = 2;
        public static int USER_ROLE_MANAGER = 3;
        public static int USER_ROLE_DONOR = 4;

        // SMS expiry minutes key.
        public static string SMS_EXPIRY_MINUTES_KEY = "SMSExpiryMinutes";

        /// <summary>
        /// 1 mean menu.
        /// </summary>
        public static int MENU_TYPE_ID_MENU = 1;

        public static int MENU_TYPE_ID_OTHER = 2;


        public static int OPERATION_TYPE_ADDED = 1;

        public static int OPERATION_TYPE_UPDATED = 2;

        public static int OPERATION_TYPE_DELETED = 3;

        // Super admin role.
        public static int SUPER_ADMIN_ROLE_ID = 5;
        //public static int EMPLOYEE_STATUS_INCOMPLETE_DATA = 1;
        //public static int EMPLOYEE_STATUS_READY_FOR_APPROVAL = 2;
        //public static int EMPLOYEE_STATUS_APPROVAL_REQUESTED = 3;

        //public static int EMPLOYEE_STATUS_APPROVAL_REJECTED = 5;
        //public static int EMPLOYEE_STATUS_RELEASE_REJECTED = 7;
        //public static int EMPLOYEE_STATUS_BLOCKED = 8;

        //public static int USER_ROLE_ADMIN = 1;
        //public static int USER_ROLE_CLERK = 2;
        //public static int USER_ROLE_MANAGER = 3;
        //public static int USER_ROLE_DONOR = 4;

        // SMS expiry minutes.
        public static double SMS_EXPIRY_MINUTES = 5;

        /// <summary>
        /// 1 mean menu.
        ///// </summary>
        //public static int MENU_TYPE_ID_MENU = 1;

        //public static int MENU_TYPE_ID_OTHER = 2;


        //public static int OPERATION_TYPE_ADDED = 1;

        //public static int OPERATION_TYPE_UPDATED = 2;

        //public static int OPERATION_TYPE_DELETED = 3;

        //// Super admin role.
        //public static int SUPER_ADMIN_ROLE_ID = 5;

        public static int SEARCH_BY_NAME = 0;
        public static int SEARCH_BY_IDENTIFICATION = 1;
        public static int SEARCH_BY_RANK = 3;
        public static int SEARCH_BY_STATUS = 2;

        public static int EMPLOYEE_TYPE_ID = 1;
        public static int GUARANTOR_TYPE_ID = 2;

        public static int BIOMETRIC_INFO_TYPE_ID = 1;



    }

    /// <summary>
    /// 
    /// </summary>
    public enum Language
    {
        English, Arabic
    }


    public class ListData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pData"></param>
        public ListData(object pData)
        {
            data = pData;
            recordsTotal = 2;
            recordsFiltered = 2;
        }


        public ListData(object pData, int count)
        {
            data = pData;
            recordsTotal = count;
            recordsFiltered = count;
        }

        public int draw { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public object data { get; set; }

    }
    public class ResultSet
    {

        /// <summary>
        /// 
        /// </summary>
        public ResultSet()
        {
            Language = Language.English;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public ResultSet(Exception ex)
        {
            ReplyMessage = ex.ToString();
            ResultCode = ResultCode.Failure;
        }

        public ResultSet(Exception ex, string userMessage)
        {
            ReplyMessage = userMessage;
            // Log ex
            ResultCode = ResultCode.Failure;
        }

        public ResultCode ResultCode { get; set; }

        public Language Language { get; set; }

        public string ReplyMessage { get; set; }

        //public bool IsClerk { get; set; }

        public Object data { get; set; }
        //public bool IsAdmin { get; set; }
    }
}