<?php


$apikey="Hormuud@@"; 
$partercode = 10911;
// $apiPassword="390dc48b78be0f08e150c2e411b3421a";
// $data = json_decode(file_get_contents("php://input"));
// $timestamp= $data->requestHeader->apiPassword;
// $str = $timestamp.$apikey.$partercode;
// $apiPassword= "390dc48b78be0f08e150c2e411b3421a"; 
// // $apiPassword = md5($str);

 

header("Access-Control-Allow-Origin: *");
// header($headers);
header("Access-Control-Allow-Methods: POST");
// header("timestamp:".$timestamp);
// header("apikey:".$apikey);
// header("apiPassword:".$apiPassword);
header("Content-Type: application/json; charset=UTF-8");
header('Authorization: Basic '. base64_encode("user:password"));


include_once '../config/Database.php';
include_once '../Models/QueryBillInfoClass.php';

$database = new Database();
$db = $database->getConnection();


 
$qBillinfo = new BillQueryInputModel($db);
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
$data = json_decode(file_get_contents("php://input"));

$timestamp =  $data->requestHeader->timestamp;
$apiPassword = $data->requestHeader->apiPassword;
$partercode = 10911;
$pass = 'Ayanle@@'; 

$str = $timestamp.$pass.$partercode;
$password = md5($str);

$qBillinfo->requestId = $data->requestId;
$qBillinfo->schemaVersion = $data->schemaVersion;
$qBillinfo->requestHeader->timestamp = $data->requestHeader->timestamp;
$qBillinfo->requestHeader->apikey =  $data->requestHeader->apikey;
$qBillinfo->requestHeader->apiPassword = $data->requestHeader->apiPassword;
$qBillinfo->requestBody->invoiceId =  $data->requestBody->billInfo->invoiceId;
$qBillinfo->requestBody->billInfo->paidBy =  $data->requestBody->billInfo->paidBy;
$qBillinfo->requestBody->billInfo->paidAt =  $data->requestBody->billInfo->paidAt;
$qBillinfo->requestBody->billInfo->paidDate =  $data->requestBody->billInfo->paidDate;

$qBillinfo->requestBody->transacionInfo->tansactionId =  $data->requestBody->transacionInfo->tansactionId;
$qBillinfo->requestBody->transacionInfo->amount  =  $data->requestBody->transacionInfo->amount;
$qBillinfo->requestBody->transacionInfo->currency  =  $data->requestBody->transacionInfo->currency;

if($apikey== $qBillinfo->requestHeader->apikey && $qBillinfo->requestHeader->apiPassword == $apiPassword){

$result = $qBillinfo->PaybilNotification();
 
if($result){
if($result->num_rows > 0){    
    $billRecords=array();
    $billRecords["billInfo"]=array(); 

	while ($bill = $result->fetch_assoc()) { 	
        extract($bill); 
        $billDetails=array(
            "confirmationId" => $confirmationId
        ); 
       array_push($billRecords["billInfo"], $billDetails);
    }    

    $headers = apache_request_headers(); 
    

   $headers = array("content-type"=> $_SERVER["CONTENT_TYPE"]);
//    echo json_encode(
//     "headers" =>$headers,
//    );
    echo json_encode( 
      
        array(
        "requestId " => $data->requestId,
        "schemaVersion " =>$data->schemaVersion,
        "responseHeader"=> array("timestamp" => $timestamp,
        "resultCode" => http_response_code(200)==200?"0":"1",
        "resultMessage" => "SUCCESS"
            

    ),
    "billInfo"=>array($billDetails),

    ),
   
    );

 
}
else{     
    http_response_code(404);     
    echo json_encode(
        array("message" => "No bill not found.")
    );
} 
}


}
else {
    http_response_code(404);   
    echo json_encode(
        array("message" => "Unauthorized", "id"=> "200")
    );
}
 

}


?>