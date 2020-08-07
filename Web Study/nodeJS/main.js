var http = require("http");
var fs = require("fs");
var app = http.createServer(function (request, response) {
  var url = request.url;
  if (request.url == "/") {
    url = "/index.html";
  }
  if (request.url == "/favicon.ico") {
    response.writeHead(404);
    response.end(); // 서버가 클라이언트에 의해 반응
    return;
  }
  response.writeHead(200);
  console.log(__dirname + url);
  response.end(fs.readFileSync(__dirname + "/public" + url)); // 파일을 읽음
});
app.listen(3000);
