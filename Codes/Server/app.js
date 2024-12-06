// https://m.blog.naver.com/kw8989/222057496289
const hostname = '127.0.0.1'; // ip 주소
const port = 3000; // 포트 번호

const http = require('http'); // http 형식 사용
const express = require('express'); // express 형식 사용

var app = express(); // express 실행

// swagger 접속
const { swaggerUi, specs } = require("./swagger/swagger")
app.use("/api-docs", swaggerUi.serve, swaggerUi.setup(specs))

var router = require('./routes/index.js');

// 접속 경로
app.use('/domain', router);

http.createServer(app).listen(port);
console.log("http created");