// var Conn = require('../../config/d.config');
var dbConn = require('../../config/connection')
var md5 = require('md5');


let apiKey = 'Hormuud@@'

// responseHeader": { 
//   "timestamp": "202002230231032", 
//   "resultCode": "0", 
//   "resultMessage": "SUCCESS" 
//   }, 

let ResponseData = new Object();
function QueryBillInfo(data, result) {

  try {
    var invoiceId = data.requestBody.invoiceId
    var ResHeader = {
      requestId: data.requestId,
      schemaVersion: data.schemaVersion,
      responseHeader: {
        timestamp: data.requestHeader.timestamp
      }
    }

    let parterCode = 103911
    let timestamp = data.requestHeader.timestamp
    let apiPassword = data.requestHeader.apiPassword
    var Pass = md5(timestamp + "Ayanle@@" + parterCode)

    // console.log('Pass in md5 ', Pass);

    // console.log('apiPassword in headr  ', apiPassword);


    if (apiPassword === Pass && apiKey === data.requestHeader.apikey) {
      let query = `CALL get_bill_info('${invoiceId}')`;
      dbConn.query(query).then((res) => {
        // console.log('res ', res);
        var length = res.length;
        ResponseData.headers = {
          "content-type": "content-type"
        }
        ResponseData.requestId = data.requestId,
          ResponseData.schemaVersion = data.schemaVersion,
          ResponseData.responseHeader = {
            timestamp: data.requestHeader.timestamp,
            resultCode: length > 0 ? "0" : "1",
            resultMessage: length > 0 ? "Success" : "NotFound"

          },
          ResponseData.billInfo = res
        // ResHeader.responseHeader.ReplayMessage = length > 0 ? "Success" : "NotFound";
        // ResponseData.Header = ResHeader;
        // ResponseData.billInfo = res;
        // console.log('ResponseData.billInfo ', ResponseData.billInfo);
        result(null, ResponseData);
      });
    }

    else {
      returndata = {

        "message": "Unauthorized",
        "id": "200"
      }
      result(null, returndata);
    }

  } catch (error) {
    console.log('error accured', error);
  }
}

async function payBillNotification(data, result) {

  try {
    // console.log('data ', data);
    let parterCode = 103911
    let timestamp = data.requestHeader.timestamp
    let apiPassword = data.requestHeader.apiPassword
    var Pass = md5(timestamp + "Ayanle@@" + parterCode)

    // console.log('Pass in md5 ', Pass);

    // console.log('apiPassword in headr  ', apiPassword);

    if (apiPassword === Pass && apiKey === data.requestHeader.apikey) {

      let query = `CALL Pay_Bill_Notification('${data.requestBody.billInfo.invoiceId}','${data.requestBody.billInfo.paidBy}','${data.requestBody.billInfo.paidAt}','${data.requestBody.billInfo.paidDate}','${data.requestBody.transacionInfo.tansactionId}','${data.requestBody.transacionInfo.amount}','${data.requestBody.transacionInfo.currency}')`;
      await dbConn.query(query).then((res) => {
        // console.log('res Pay_Bill_Notification ', res);
        var length = res.length;
        ResponseData.headers = {
          "content-type": "content-type"
        }
        ResponseData.requestId = data.requestId,
          ResponseData.schemaVersion = data.schemaVersion,
          ResponseData.requestHeader = {
            timestamp: data.requestHeader.timestamp,
            resultCode: length > 0 ? "0" : "401",
            resultMessage: length > 0 ? "Success" : "NotFound"
          },
          ResponseData.confirmationId = res[0].confirmationId
        result(null, ResponseData);
      }).catch((err) => {
        console.log('error ', err);
      })
    }

    else {
      returndata = {

        "message": "Unauthorized",
        "id": "200"
      }
      result(null, returndata);
    }





  } catch (error) {
    console.log('error accured', error);
  }
}

module.exports = { QueryBillInfo, payBillNotification };