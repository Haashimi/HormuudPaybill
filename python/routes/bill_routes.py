# This file defines the routes/endpoints for bill-related actions.
from flask import Blueprint
from controllers.bill_controller import pay_bill_notification, query_bill_info

bill_routes = Blueprint('bill_routes', __name__)

@bill_routes.route('/pay-bill-notification', methods=['POST'])
def handle_pay_bill_notification():
    return pay_bill_notification()

@bill_routes.route('/query-bill-info', methods=['POST'])
def handle_query_bill_info():
    return query_bill_info()
