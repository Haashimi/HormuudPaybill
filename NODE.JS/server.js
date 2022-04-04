const express = require('express');
const bodyParser = require('body-parser');

const apiroute = require('./src/routes/Routes');
const authserver = require('./src/middleware/authServer')
const cors = require('cors');
const app = express();
const port = process.env.port || 80;
// parse request data content type application/x-www-form-rulencoded
app.use(bodyParser.urlencoded({ extended: false }));

// parse request data content type application/json
app.use(bodyParser.json());

// app.get('/', (req, res) => {

//     res.send("Hello World Garaad")
// })

// enable cors
app.use(cors({
    origin: '*'
}));

// import authe route

app.use(authserver)

// import peronal route
app.use('/api/values/', apiroute);
app.use(function (req, res, next) {
    res.header('Content-Type', 'application/json');
    next();
});


// listen the port

app.listen(port, () => {

    console.log('The Express Server is listen on port', port)
})
