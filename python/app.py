# This is the main entry point for the Flask application.
from flask import Flask
from routes.bill_routes import bill_routes

app = Flask(__name__)
app.register_blueprint(bill_routes, url_prefix='/api')

if __name__ == '__main__':
    app.run(debug=True)
