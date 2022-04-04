// import Personal Model


const { response } = require('express');
const Model = require('../models/Models');



// get Sngle Personal Infor

exports.QueryBillInfo = (req, res) => {
    // console.log('req.body ',req)
    // var invoiceId = req.body.requestBody.invoiceId
    // res.setHeader("Content-Type", "application/json");
    res.setHeader("Content-Type", "application/json; charset=utf-8");
    // console.log(' resposess ', res)
    Model.QueryBillInfo(req.body, (err, resdata) => {
        
        if (err){
          
          res.json(err);
        }
        else{
        //  console.log('fetch  data',resdata);

        
        res.json(resdata);
        }
    })
}

// create new employee
exports.payBillNotification = (req, res) => {
    // console.log('req ',req)
    res.setHeader("Content-Type", "application/json; charset=utf-8");
    // console.log(' resposess ', res);
    const data = req.body
    // console.log('req.body ', data);


    // check null
    if (req.body.constructor === Object && Object.keys(req.body).length === 0) {
        // console.log('data ', data);
        res.json(400).send({ success: false, message: 'Please fill all fields' });
    } else {
        Model.payBillNotification(data, (error, response) => {
            if (error){
                res.send(error);
            }
            else{
                console.log('response ', response);
            res.json(response)
            }
        })
    }
}
