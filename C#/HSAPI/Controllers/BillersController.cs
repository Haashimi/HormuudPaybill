using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace HSAPI.Controllers
{ 
    //[Authorize(Roles = "Payment")]
    public class BillersController : ApiController
    {
        //CONNCECTIONS
        //public static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["billingConnectionString"].ConnectionString;
        public static string connection = "Data Source=XXXX;Initial Catalog=btdata;Integrated Security=false;User ID=sa;Password=12345;Pooling=false;";

        // QueryBillInfo: GET STUDENT INFORMATION
        [HttpPost]
        public HttpResponseMessage QueryBillInfo(QueryBillInfoModel Info)
        {
            var apiPassword = CreateMD5(Info.requestHeader.timestamp + "Ayanle@@" + 103911);
            var apikey = "Hormuud@@";
            var result = new BilInfo();
            try
            {

                if (apikey == Info.requestHeader.apikey && apiPassword == Info.requestHeader.apiPassword)
                {
                    result = CHECKSTUDENTS(Info.requestBody.invoiceId);

                    List<BilInfoObj> obj = new List<BilInfoObj>();
                    obj.Add(new BilInfoObj()
                    {
                        billId = result.billId,
                        billTo = result.billTo,
                        billAmount = result.billAmount,
                        billCurrency = result.billCurrency,
                        billNumber = result.billNumber,
                        dueDate = result.dueDate,
                        status = result.status,
                        partialPayAllowed = result.partialPayAllowed,
                        description = result.description,
                    });

                    var json = new

                    {
                        requestId = Info.requestId,
                        schemaVersion = "1.0",
                        responseHeader = new
                        {
                            timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                            resultCode = result.ResultCode,
                            resultMessage = result.ReplyMessage
                        },

                        billInfo = obj,

                    };
                    var postData = JsonConvert.SerializeObject(json);
                    var postJson = _UpdateJSON(postData);
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject(postJson));

                }

                else
                {
                    var data = new
                    {
                        message = "Unauthorized",
                        ResultCode = "401"
                    };
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, data);
                }

            }
            catch (Exception ex)
            {
                var data = new
                {
                    message = "Something went wrong please try again",
                    //ResultCode = "400"
                };
                return Request.CreateResponse(HttpStatusCode.Unauthorized, data);
            }

        }

        //CONVERT content_type TO content-type
        public static string _UpdateJSON(string json)
        {
            json = json.Replace("content_type", "content-type");
            return json;
        }

        //CHECKSTUDENTS : FUNCTION GET DATABASE STUDENT INFORMATION
        public static BilInfo CHECKSTUDENTS(string invoiceId)
        {
            var result = new BilInfo();

            try
            {
                BilInfo BaypillInfo = new BilInfo();
                SqlConnection con = new SqlConnection(connection);
                {
                    con.Open();

                    DataSet Myds1 = new DataSet();
                    SqlDataAdapter sqldr1 = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("[Ayaanle_Test_Paybill]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@invoiceId", SqlDbType.VarChar, 50).Value = invoiceId;
                    sqldr1.SelectCommand = cmd;
                    sqldr1.Fill(Myds1);
                    DataTable dt1 = Myds1.Tables[0];
                    int i = 0;

                    if (dt1.Rows.Count > 0)
                    {
                        BaypillInfo.billId = Convert.ToString(dt1.Rows[i]["billId"]);
                        BaypillInfo.billTo = Convert.ToString(dt1.Rows[i]["billTo"]);
                        BaypillInfo.billAmount = Convert.ToDouble(dt1.Rows[i]["billAmount"]);
                        BaypillInfo.billCurrency = Convert.ToString(dt1.Rows[i]["billCurrency"]);
                        BaypillInfo.billNumber = Convert.ToString(dt1.Rows[i]["billNumber"]);
                        BaypillInfo.dueDate = Convert.ToString(dt1.Rows[i]["dueDate"]);
                        BaypillInfo.status = Convert.ToString(dt1.Rows[i]["status"]);
                        BaypillInfo.partialPayAllowed = Convert.ToString(dt1.Rows[i]["partialPayAllowed"]);
                        BaypillInfo.description = Convert.ToString(dt1.Rows[i]["description"]);

                        if (Convert.ToDouble(dt1.Rows[i]["billAmount"]) > 0)
                        {
                            result = BaypillInfo;
                            result.ReplyMessage = "Success";
                            result.ResultCode = "0";
                        }

                        else if (Convert.ToDouble(dt1.Rows[i]["billAmount"]) == 0)
                        {
                            result = BaypillInfo;
                            result.ReplyMessage = "AlreadyPaidBill";
                            result.ResultCode = "402";
                        }

                    }
                    else
                    {
                        result = BaypillInfo;
                        result.ReplyMessage = "NofFound";
                        result.ResultCode = "401";
                    }

                    con.Close();
                    con.Dispose();


                }

            }
            catch (Exception ex)
            {
                result.ReplyMessage = ex.Message;
                //result.ReplyMessage = "Not Found";
                result.ResultCode = "3";
                return result;
            }
            return result;
        }

        //PayBillNotification: INSERT STUDENT INFORMATION for paid
        [HttpPost]
        public HttpResponseMessage PayBillNotification(PayBillNotificationModel Info)
        {
            var apiPassword = CreateMD5(Info.requestHeader.timestamp + "Ayanle@@" + 103911);
            var apikey = "Hormuud@@";
            var result = new confirmationIdinfo();
            try
            {
                if (apikey == Info.requestHeader.apikey && apiPassword == Info.requestHeader.apiPassword)
                {
                    result = CHECKPayBillNotification(Info.requestBody.billInfo.invoiceId, Info.requestBody.billInfo.paidBy, Info.requestBody.billInfo.paidAt,
                    Info.requestBody.billInfo.paidDate, Info.requestBody.transacionInfo.tansactionId, Info.requestBody.transacionInfo.amount, Info.requestBody.transacionInfo.currency);

                    var json = new

                    {
                        requestId = Info.requestId,
                        schemaVersion = "1.0",
                        responseHeader = new
                        {
                            timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                            resultCode = result.ResultCode,
                            resultMessage = result.ReplyMessage
                        },

                        confirmationId = result.confirmationId,

                    };

                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }

                else
                {
                    var data = new
                    {
                        message = "Unauthorized",
                        ResultCode = "401"
                    };
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, data);
                }

            }
            catch (Exception ex)
            {
                var data = new
                {
                    message = "Something went wrong please try again",
                    //id = "400"
                };
                return Request.CreateResponse(HttpStatusCode.Unauthorized, data);
            }
        }

        //PayBillNotification : FUNCTION INSERT DATABASE STUDENT INFORMATION
        public static confirmationIdinfo CHECKPayBillNotification(string invoiceId, string paidBy, string paidAt, string paidDate, string tansactionId, string amount, string currency)
        {
            var result = new confirmationIdinfo();

            try
            {
                confirmationIdinfo PayBillNotificationInfo = new confirmationIdinfo();
                SqlConnection con = new SqlConnection(connection);
                {
                    con.Open();

                    DataSet Myds1 = new DataSet();
                    SqlDataAdapter sqldr1 = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("[Pay_Bill_Notification]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@invoiceId", SqlDbType.VarChar, 50).Value = invoiceId;
                    cmd.Parameters.Add("@paidBy", SqlDbType.VarChar, 50).Value = paidBy;
                    cmd.Parameters.Add("@paidAt", SqlDbType.VarChar, 50).Value = paidAt;
                    cmd.Parameters.Add("@paidDate", SqlDbType.VarChar, 50).Value = paidDate;
                    cmd.Parameters.Add("@tansactionId", SqlDbType.VarChar, 50).Value = tansactionId;
                    cmd.Parameters.Add("@amount", SqlDbType.VarChar, 50).Value = amount;
                    cmd.Parameters.Add("@currency", SqlDbType.VarChar, 50).Value = currency;

                    sqldr1.SelectCommand = cmd;
                    sqldr1.Fill(Myds1);
                    DataTable dt1 = Myds1.Tables[0];
                    int i = 0;
                    for (i = 0; i < dt1.Rows.Count; i++)
                    {
                        PayBillNotificationInfo.confirmationId = Convert.ToString(dt1.Rows[i]["confirmationId"]);
                    }

                    con.Close();
                    con.Dispose();

                    if (i > 0)
                    {

                        result = PayBillNotificationInfo;
                        result.ReplyMessage = "Success";
                        result.ResultCode = "0";
                        return result;
                    }
                    else
                    {
                        result = PayBillNotificationInfo;
                        result.ReplyMessage = "Not Found";
                        result.ResultCode = "2";
                        return result;
                    }

                }

            }
            catch (Exception ex)
            {
                result.ReplyMessage = ex.Message;
                //result.ReplyMessage = "Not Found";
                result.ResultCode = "3";
                return result;
            }
        }

        //CreateMD5: FUNCTION MD5 PASSWORD
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        //CLASS Paybill
        public class PayBillNotificationModel

        {
            public string requestId { get; set; }
            public string schemaVersion { get; set; }
            public requestHeader requestHeader { get; set; }
            public requestBody requestBody { get; set; }
        }
        public class QueryBillInfoModel
        {
            public string requestId { get; set; }
            public string schemaVersion { get; set; }
            public requestHeader requestHeader { get; set; }
            public requestBodys requestBody { get; set; }

        }
        public class requestHeader
        {
            public string timestamp { get; set; }
            public string apikey { get; set; }
            public string apiPassword { get; set; }
        }
        public class billInfo
        {
            public string invoiceId { get; set; }
            public string paidBy { get; set; }
            public string paidAt { get; set; }
            public string paidDate { get; set; }
        }
        public class requestBody
        {
            public billInfo billInfo { get; set; }
            public transacionInfo transacionInfo { get; set; }
        }
        public class requestBodys
        {
            public string invoiceId { get; set; }

        }
        public class transacionInfo
        {
            public string tansactionId { get; set; }
            public string amount { get; set; }
            public string currency { get; set; }

        }
        public class ResponseHeader
        {
            public string timestamp { get; set; }
            public string resultCode { get; set; }
            public string resultMessage { get; set; }

        }
        public class confirmationIdinfo
        {
            public string confirmationId { get; set; }
            public string ReplyMessage { get; set; }
            public string ResultCode { get; set; }
        }
        public class BilInfo
        {
            public string billId { get; set; }
            public string billTo { get; set; }
            public double billAmount { get; set; }
            public string billCurrency { get; set; }
            public string billNumber { get; set; }
            public string dueDate { get; set; }
            public string status { get; set; }
            public string partialPayAllowed { get; set; }
            public string description { get; set; }
            public string ReplyMessage { get; set; }
            public string ResultCode { get; set; }

        }
        public class BilInfoObj
        {
            public string billId { get; set; }
            public string billTo { get; set; }
            public double billAmount { get; set; }
            public string billCurrency { get; set; }
            public string billNumber { get; set; }
            public string dueDate { get; set; }
            public string status { get; set; }
            public string partialPayAllowed { get; set; }
            public string description { get; set; }


        }
    }
}
 
