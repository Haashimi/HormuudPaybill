
**Requirements**                                
    PHP 7 and above,                        
    MySQL8,                                     
    Apache web server or XAMPP web server,                                   
    Postman for testing

**Steps to create a REST API in PHP with MySQL:**

    Create a Database and Table with Dummy Data
    Create a Database Connection
    Create a Models
    Create a REST API File

**REST APIFiles**                                                                                         
    QueryBillInfo and payBillNotification in values directory are two REST API Files which are 
    QueryBillInfo takes invoiceId in request body and returns billinfo while payBillNotification takes billinfo and transacionInfo in request body and then returns confirmationId("invoiceId+date")

**Folder Structure**                                                                                
    │   API  Technical Documentation for university.pdf                                             
    │   README.md                                                                                   
    │                                                                                                     
    ├───config                                                                                          
    │       Database.php                                                                                
    │                                                                                                   
    ├───Models                                                                                          
    │       QueryBillInfoClass.php                                                                      
    │                                                                                                   
    └───values                                                                                  
            .htaccess                                                                               
            payBillNotification.php                                                                     
            QueryBillInfo.php                                                                             

**What is REST?**                                                                               
    REST (Representational State Transfer) is a software architectural style that developers established to assist in creating and developing the World Wide Web’s architecture. REST specifies a set of criteria on how the architecture of an Internet-scale distributed hypermedia system, such as the Web, should operate. It is one of the various ways applications, servers, and websites may communicate data and services. It generally provides the rules for how developers working with data and services represent elements through the API. Other programs may appropriately request and receive the data and services that an API makes accessible.

**Overview of MySQL databases**                               
    MySQL is an open-source relational database management system (RDBMS). It is the most popular database system used with PHP. MySQL is a fully-managed database service used to deploy cloud-native applications


**What is Postman?**                                   
    Postman is an application used for API testing. It is an HTTP client that tests HTTP requests, utilizing a graphical user interface, through which we obtain different types of responses that need to be subsequently validated. 

**What is XAMPP?**                              
    XAMPP is a free and open-source cross-platform web server solution stack package developed by Apache Friends, consisting mainly of the Apache HTTP Server, MariaDB database, and interpreters for scripts written in the PHP and Perl programming languages.
**Running and Testing**
    open postman and then follow steps below
    








