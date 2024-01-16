const md5 = require('md5');
const dbConn = require('../db'); // Adjust the path as necessary

const partnerCode = 103911;

async function verifyCredentials(data) {
  const timestamp = data.requestHeader.timestamp;
  const apiPassword = data.requestHeader.apiPassword;
  const computedPassword = md5(`${timestamp}Ayanle@@${partnerCode}`);
  return apiPassword === computedPassword && data.requestHeader.apikey === data.requestHeader.apikey;
}

exports.payBillNotification = async (req, res) => {
  try {
    if (!await verifyCredentials(req.body)) {
      return res.status(401).json({
        message: "Unauthorized",
        id: "200"
      });
    }

    const { invoiceId, paidBy, paidAt, paidDate } = req.body.requestBody.billInfo;
    const { tansactionId, amount, currency } = req.body.requestBody.transacionInfo;

    const query = `CALL Pay_Bill_Notification(?,?,?,?,?,?,?)`;
    const queryParams = [invoiceId, paidBy, paidAt, paidDate, tansactionId, amount, currency];
    
    const res = await dbConn.query(query, queryParams);
    
    if (res.length > 0) {
      const responseData = {
        headers: { "content-type": "application/json" },
        requestId: req.body.requestId,
        schemaVersion: req.body.schemaVersion,
        requestHeader: {
          timestamp: req.body.requestHeader.timestamp,
          resultCode: "0",
          resultMessage: "Success"
        },
        confirmationId: res[0].confirmationId
      };
      res.json(responseData);
    } else {
      res.status(404).json({
        headers: { "content-type": "application/json" },
        requestId: req.body.requestId,
        schemaVersion: req.body.schemaVersion,
        requestHeader: {
          timestamp: req.body.requestHeader.timestamp,
          resultCode: "401",
          resultMessage: "NotFound"
        }
      });
    }
  } catch (error) {
    console.error('Error occurred', error);
    res.status(500).json({ message: 'Internal server error' });
  }
};




exports.queryBillInfo = async (req, res) => {
    try {
      const data = req.body;
      if (!await verifyCredentials(data)) {
        return res.status(401).json({
          message: "Unauthorized",
          id: "200"
        });
      }
  
      const invoiceId = data.requestBody.invoiceId;
      const query = `CALL get_bill_info(?)`;
      const queryParams = [invoiceId];
  
      const billInfoResults = await dbConn.query(query, queryParams);
  
      if (billInfoResults.length > 0) {
        const responseData = {
          headers: { "content-type": "application/json" },
          requestId: data.requestId,
          schemaVersion: data.schemaVersion,
          responseHeader: {
            timestamp: data.requestHeader.timestamp,
            resultCode: "0",
            resultMessage: "Success"
          },
          billInfo: billInfoResults
        };
        res.json(responseData);
      } else {
        res.status(404).json({
          headers: { "content-type": "application/json" },
          requestId: data.requestId,
          schemaVersion: data.schemaVersion,
          responseHeader: {
            timestamp: data.requestHeader.timestamp,
            resultCode: "1",
            resultMessage: "NotFound"
          }
        });
      }
    } catch (error) {
      console.error('Error occurred', error);
      res.status(500).json({ message: 'Internal server error' });
    }
  };