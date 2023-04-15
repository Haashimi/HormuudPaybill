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
{ </br>
"requestId": "0637573673-836bdsgs7-stgsgs988", </br>
"schemaVersion": "1.0", </br>
"requestHeader": { </br>
"timestamp": "202002230231032", </br>
//api key usameeso nala wadaag </br>
"apikey": "ext_biller",  </br>
"apiPassword": "390dc48b78be0f08e150c2e411b3421a" </br>
}, </br>
"requestBody": { </br>
"invoiceId": "studentid" </br>
} </br>
} </br>



**Response:** </br>
"headers": { </br>
“content-type”: “application/json” </br>
} </br>
{ </br>
"requestId": "0637573673-836bdsgs7-stgsgs988", "schemaVersion": "1.0", </br>
"responseHeader": { </br>
"timestamp": "202002230231032", </br>
"resultCode": "0", </br>
"resultMessage": "SUCCESS" </br>
}, </br>
"billInfo": [ </br>
{ </br>
"billId": "STUDENT ID", </br>
"billTo": "SUTDENT NAME", </br>
"billAmount": "8", </br>
"billCurrency": "USD", </br>
"billNumber": "STUDENT ID", </br>
"dueDate": "2020-03-29T15:56:20.447Z", "status": "PENDING", </br>
"partialPayAllowed": "1", </br>
"description": "NAME OF UNIVERSITY" </br>
} </br>
] </br>
} </br>







2.**PayBill Notification:** </br>
**Request** </br>
URL: https://domain.com/api/values/payBillNotification </br>
{ </br>
"requestId": "0637573673-836bdsgs7-stgsgs97789", </br>
"schemaVersion": "1.0", </br>
"requestHeader": { </br>
"timestamp": "202002230231032", </br>
//api key usameeso nala wadaag </br>
"apikey": "ext_biller", </br>
"apiPassword": "390dc48b78be0f08e150c2e411b3421a" </br>
}, </br>
"requestBody": </br>
{ </br>
"billInfo": { </br>
"invoiceId": "STUDENT ID", </br>
"paidBy": "252634455678", </br>
"paidAt": "BANK", </br>
"paidDate": "2020-02-30T15:56:55" </br>
}, </br>
"transacionInfo": { </br>
"tansactionId": "2784458", </br>
"amount": "8", </br>
"currency": "USD" </br>
} </br>
} </br>
} </br>







**Response:** </br>
{ </br>
"requestId": "0637573673-836bdsgs7-stgsgs988", </br>
"schemaVersion": "1.0", </br>
"responseHeader": { </br>
"timestamp": "202002230231032", </br>
"resultCode": "0", </br> 
"resultMessage": "SUCCESS" </br>
}, </br>
"confirmationId": "STUDENTID+date" </br>
} </br>






3. **Already Pail Bill response format when customer paid the bill Response** </br>
{ </br>
"requestId": "3990c643-1c7c-49de-b593-34a4defa7e4b", </br>
"schemaVersion": "1.0", </br>
"responseHeader": { </br>
"timestamp": "20211005111308381291", </br>
"resultCode": "402", </br>
"resultMessage": "AlreadyPaidBill" </br>
}, </br>
"billInfo": [ </br>
{ </br>
"billId": 3386841, </br>
"billTo": "Customer name", </br>
"billAmount": "0", </br>
"billCurrency": "USD", </br>
"billNumber": "STUDENT ID", </br>
"dueDate": "20211005111308406825", </br>
"status": "PAID", </br>
"partialPayAllowed": "1", </br>
"description": "Bill Description" </br>
} </br>
] </br>
} </br>






4. **Invalid Bill response format where bill ID doesn’t exit** </br>
**Response** </br>
{ </br>
"requestId": "Not Found", </br>
"schemaVersion": "1.0", </br>
"responseHeader": { </br>
"timestamp": "20211005111244", </br>
"resultCode": "401", </br>
"resultMessage": "Not Found" </br>
}, </br>
"billInfo": [ </br>
{ </br>
"billId": "Not Found", </br>
"billTo": "Not Found", </br>
"billAmount": 0, </br>
"billCurrency": "USD", </br>
"billNumber": 0, </br>
"dueDate": "20211005081034", </br>
"status": "UNknown", </br>
"partialPayAllowed": "1",  </br>
"description": "Bill Doesn’t exit” </br>
} </br>
] </br>
} </br>




5. **Payment transaction status checking** </br>
**Request** </br>
URL: https://domain.com/api/getTransactionStatus </br>
{ </br>
"requestId": "b1bcb677-16af-44f0-9f2e-40d12041e4ca", </br>
"schemaVersion": "1.0", </br>
"requestHeader": { </br> 
"timestamp": "20210623133607", </br>
"apikey": "apiusername", </br>
"apiPassword": "apipassword" </br>
}, </br>
"requestBody": { </br>
"transacionInfo": { </br>
"tansactionId": "10742611322" </br>
} </br>
} </br>
} </br>
Response if transaction exists in your database </br>
{ </br>
"schemaVersion": "1.0", </br>
"responseHeader": { </br> 
"timestamp": "20210623133607", </br>
"resultCode": "0", </br>
"resultMessage": "SUCCESS" </br>
}, </br>
"confirmationId": "20210623133607-225152" </br>
} </br>
Response if transaction doesn’t exist in your database </br>
{ </br>
"schemaVersion": "1.0", </br>
"responseHeader": { </br>
"timestamp": "20210623133607", </br>
"resultCode": 401, </br>
"resultMessage": "not found" </br>
}
}
**NOTE** </br>
"apiPassword" in requestHeader is hashed using md5 hashing algorithm, please hash your password using formula: </br>
md5(timestamp+password+partnercode)  </br>
timestamp = coming in the request password = is the one you have at ur system partnercode = is the one @billingserver, billers will be given to them for this purpose Credentials Password: Waafi@21#$


