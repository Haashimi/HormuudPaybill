const express = require('express');
const jwt = require('jsonwebtoken');
const route = express.Router();

const Controller = require('../controller/controller');
route.post('/queryBillInfo',  Controller.QueryBillInfo)
route.post('/payBillNotification', Controller.payBillNotification)



module.exports = route;
