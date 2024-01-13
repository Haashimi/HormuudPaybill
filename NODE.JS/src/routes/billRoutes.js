const express = require('express');
const billController = require('../controllers/billController');

const router = express.Router();

router.post('/payBillNotification', billController.payBillNotification);
router.post('/queryBillInfo', billController.queryBillInfo);

module.exports = router;
