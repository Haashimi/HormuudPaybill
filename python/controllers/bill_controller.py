import hashlib
from flask import jsonify, request
from your_database_module import db_query  # Replace with your actual database module

partner_code = 103911

def verify_credentials(data):
    timestamp = data['requestHeader']['timestamp']
    api_password = data['requestHeader']['apiPassword']
    computed_password = hashlib.md5((timestamp + "Ayanle@@" + str(partner_code)).encode()).hexdigest()
    return api_password == computed_password and data['requestHeader']['apikey'] == data['requestHeader']['apikey']

def pay_bill_notification():
    data = request.json
    if not verify_credentials(data):
        return jsonify({"message": "Unauthorized", "id": "200"}), 401

    bill_info = data['requestBody']['billInfo']
    transaction_info = data['requestBody']['transacionInfo']

    query = "CALL Pay_Bill_Notification(%s, %s, %s, %s, %s, %s, %s)"
    query_params = (bill_info['invoiceId'], bill_info['paidBy'], bill_info['paidAt'],
                    bill_info['paidDate'], transaction_info['tansactionId'],
                    transaction_info['amount'], transaction_info['currency'])

    result = db_query(query, query_params)  # Replace with your actual database query function

    if result:
        confirmation_id = result[0]['confirmationId']  # Adjust based on your actual result structure
        response_data = {
            "headers": {"content-type": "application/json"},
            "requestId": data['requestId'],
            "schemaVersion": data['schemaVersion'],
            "requestHeader": {
                "timestamp": data['requestHeader']['timestamp'],
                "resultCode": "0",
                "resultMessage": "Success"
            },
            "confirmationId": confirmation_id
        }
        return jsonify(response_data)

    return jsonify({
        "headers": {"content-type": "application/json"},
        "requestId": data['requestId'],
        "schemaVersion": data['schemaVersion'],
        "requestHeader": {
            "timestamp": data['requestHeader']['timestamp'],
            "resultCode": "401",
            "resultMessage": "NotFound"
        }
    }), 404

def query_bill_info():
    data = request.json
    if not verify_credentials(data):
        return jsonify({"message": "Unauthorized", "id": "200"}), 401

    invoice_id = data['requestBody']['invoiceId']
    query = "CALL get_bill_info(%s)"
    query_params = (invoice_id,)

    try:
        bill_info_results = db_query(query, query_params)  # Replace with your actual database query function

        if bill_info_results:
            response_data = {
                "headers": {"content-type": "application/json"},
                "requestId": data['requestId'],
                "schemaVersion": data['schemaVersion'],
                "responseHeader": {
                    "timestamp": data['requestHeader']['timestamp'],
                    "resultCode": "0",
                    "resultMessage": "Success"
                },
                "billInfo": bill_info_results
            }
            return jsonify(response_data)

        return jsonify({
            "headers": {"content-type": "application/json"},
            "requestId": data['requestId'],
            "schemaVersion": data['schemaVersion'],
            "responseHeader": {
                "timestamp": data['requestHeader']['timestamp'],
                "resultCode": "1",
                "resultMessage": "NotFound"
            }
        }), 404

    except Exception as e:
        print(f"An error occurred: {e}")
        return jsonify({"message": "Internal server error"}), 500
