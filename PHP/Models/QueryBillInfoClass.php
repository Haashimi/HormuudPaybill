<?php
class BillQueryInputModel{   
    
    private $procedure = "get_bill_info";
    private $PaybilNotiProcedure = "Pay_Bill_Notification";       
    public $requestId;
    public $schemaVersion;
    public $requestHeader;
    public $requestBody;

    
	
    public function __construct($db){
        $this->conn = $db;
        $this->requestHeader = new RequestHeader();
        $this->requestBody = new RequestBody();

    }	
    function loadData(){	

 
        
		
            
			$stmt = $this->conn->prepare( "CALL ".$this->procedure."(?)");
			$stmt->bind_param("s", $this->requestBody->invoiceId);	
            $stmt->execute();			
            $result = $stmt->get_result();		
            return $result;					
			
	
	}

    function PaybilNotification(){	

 
        

        
			$stmt = $this->conn->prepare( "CALL ".$this->PaybilNotiProcedure."(?,?,?,?,?,?,?)");
			$stmt->bind_param("sssssss", $this->requestBody->invoiceId, $this->requestBody->billInfo->paidBy, $this->requestBody->billInfo->paidAt, $this->requestBody->billInfo->paidDate,$this->requestBody->transacionInfo->tansactionId,$this->requestBody->transacionInfo->amount,$this->requestBody->transacionInfo->currency);	
            $stmt->execute();			
            $result = $stmt->get_result();		
            return $result;					
			
	
	}
 
}

class RequestHeader{
    public $timestamp;
    public $apikey;
    public $apiPassword;
}

class RequestBody{
    public $invoiceId;
    public $billInfo;
    public $transacionInfo;
    public function __construct(){
        
        $this->billInfo = new BillInfo();
        $this->transacionInfo = new TansacionInfo();

    }	
}
  

class TansacionInfo{
    public $tansactionId;
    public $amount;
    public $currency;

}

class BillInfo {

    public $paidBy;
    public $paidAt;
    public $paidDate;
}

?>


