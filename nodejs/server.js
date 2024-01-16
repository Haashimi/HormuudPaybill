const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');
const app = express();
const port = process.env.port || 80;
// parse request data content type application/x-www-form-rulencoded
app.use(bodyParser.urlencoded({ extended: false }));

// parse request data content type application/json
app.use(bodyParser.json());


// enable cors
app.use(cors({
    origin: '*'
}));

// import authe route

app.use(authserver)

const express = require('express');
const billRoutes = require('./routes/billRoutes');



// import peronal route
app.use('/api/values/', billRoutes);
app.use(function (req, res, next) {
    res.header('Content-Type', 'application/json');
    next();
});


// listen the port

app.listen(port, () => {

    console.log('The Express Server is listen on port', port)
})
