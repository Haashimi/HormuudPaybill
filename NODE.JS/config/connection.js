const Sequelize = require('sequelize');
// read in the .env file


require('dotenv').config();

// console.log('process.env.DB_Name ', process.env.DB_Name);
  const dbConn = new Sequelize(
    process.env.DB_Name,
    process.env.DB_User,
    '',
 { 
    host: 'localhost',
    port: process.env.DB_port,
    dialect: 'mysql',
     
    pool: {
    "max": 10,
    "min": 0,
    "idle": 25000,
    "acquire": 25000,
    "requestTimeout": 300000
  },
  dialectOptions: {
    "connectTimeout":30000 
 
  }
  });


  dbConn.authenticate().then((res) => {
    console.log('Connection successful', );
  })
  .catch((err) => {
    console.log('Unable to connect to database', err);
  });




  module.exports = dbConn