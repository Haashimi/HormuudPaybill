**API Technical Documentation:** </br>
--------------------------- </br>
External Billers </br>
_______________________________ </br>
**Services:** </br>
1. Query Bill Info </br>
2. Pay Bill Notification </br>
3. Already Paid Bill </br>
4. Invalid Bill </br>

**Request/Response:** </br>
================
1. **QueryBillInfo:** </br>
//api urlkaad gili oo aan soo gaari karno URL: https://domain.com/api/values/queryBillInfo Request: </br>
{
"requestId": "0637573673-836bdsgs7-stgsgs988",
"schemaVersion": "1.0",
"requestHeader": {
"timestamp": "202002230231032",
//api key usameeso nala wadaag
"apikey": "ext_biller",
"apiPassword": "390dc48b78be0f08e150c2e411b3421a"
},
"requestBody": {
"invoiceId": "studentid"
}
}



**Response:** </br>
"headers": {
“content-type”: “application/json”
}
{
"requestId": "0637573673-836bdsgs7-stgsgs988", "schemaVersion": "1.0",
"responseHeader": {
"timestamp": "202002230231032",
"resultCode": "0",
"resultMessage": "SUCCESS"
},
"billInfo": [
{
"billId": "STUDENT ID",
"billTo": "SUTDENT NAME",
"billAmount": "8",
"billCurrency": "USD",
"billNumber": "STUDENT ID",
"dueDate": "2020-03-29T15:56:20.447Z", "status": "PENDING",
"partialPayAllowed": "1",
"description": "NAME OF UNIVERSITY"
}
]
}







2.**PayBill Notification:** </br>
**Request** </br>
URL: https://domain.com/api/values/payBillNotification
{
"requestId": "0637573673-836bdsgs7-stgsgs97789",
"schemaVersion": "1.0",
"requestHeader": {
"timestamp": "202002230231032",
//api key usameeso nala wadaag
"apikey": "ext_biller",
"apiPassword": "390dc48b78be0f08e150c2e411b3421a"
},
"requestBody":
{
"billInfo": {
"invoiceId": "STUDENT ID",
"paidBy": "252634455678",
"paidAt": "BANK",
"paidDate": "2020-02-30T15:56:55"
},
"transacionInfo": {
"tansactionId": "2784458",
"amount": "8",
"currency": "USD"
}
}
}







**Response:** </br>
{
"requestId": "0637573673-836bdsgs7-stgsgs988",
"schemaVersion": "1.0",
"responseHeader": {
"timestamp": "202002230231032",
"resultCode": "0",
"resultMessage": "SUCCESS"
},
"confirmationId": "STUDENTID+date"
}






3. **Already Pail Bill response format when customer paid the bill Response** </br>
{
"requestId": "3990c643-1c7c-49de-b593-34a4defa7e4b",
"schemaVersion": "1.0",
"responseHeader": {
"timestamp": "20211005111308381291",
"resultCode": "402",
"resultMessage": "AlreadyPaidBill"
},
"billInfo": [
{
"billId": 3386841,
"billTo": "Customer name",
"billAmount": "0",
"billCurrency": "USD",
"billNumber": "STUDENT ID",
"dueDate": "20211005111308406825",
"status": "PAID",
"partialPayAllowed": "1",
"description": "Bill Description"
}
]
}






4. **Invalid Bill response format where bill ID doesn’t exit** </br>
**Response** </br>
{
"requestId": "Not Found",
"schemaVersion": "1.0",
"responseHeader": {
"timestamp": "20211005111244",
"resultCode": "401",
"resultMessage": "Not Found"
},
"billInfo": [
{
"billId": "Not Found",
"billTo": "Not Found",
"billAmount": 0,
"billCurrency": "USD",
"billNumber": 0,
"dueDate": "20211005081034",
"status": "UNknown",
"partialPayAllowed": "1",
"description": "Bill Doesn’t exit”
}
]
}




5. **Payment transaction status checking** </br>
**Request** </br>
URL: https://domain.com/api/getTransactionStatus
{
"requestId": "b1bcb677-16af-44f0-9f2e-40d12041e4ca",
"schemaVersion": "1.0",
"requestHeader": {
"timestamp": "20210623133607",
"apikey": "apiusername",
"apiPassword": "apipassword"
},
"requestBody": {
"transacionInfo": {
"tansactionId": "10742611322"
}
}
}
Response if transaction exists in your database
{
"schemaVersion": "1.0",
"responseHeader": {
"timestamp": "20210623133607",
"resultCode": "0",
"resultMessage": "SUCCESS"
},
"confirmationId": "20210623133607-225152"
}
Response if transaction doesn’t exist in your database
{
"schemaVersion": "1.0",
"responseHeader": {
"timestamp": "20210623133607",
"resultCode": 401,
"resultMessage": "not found"
}
}
**NOTE** </br>
"apiPassword" in requestHeader is hashed using md5 hashing algorithm, please hash your password using formula:
md5(timestamp+password+partnercode)
timestamp = coming in the request password = is the one you have at ur system partnercode = is the one @billingserver, billers will be given to them for this purpose Credentials
Password: Waafi@21#$


