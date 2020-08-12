var http = require("http");
var fs = require("fs");
var url = require("url"); // url 모듈을 사용

var app = http.createServer(function (request, response) {
  var _url = request.url;
  var queryData = url.parse(_url, true).query;
  var title = queryData.id;

  if (_url == "/") {
    title = "Welcome";
  }
  if (_url == "/favicon.ico") {
    return response.writeHead(404);
  }
  response.writeHead(200);
  fs.readFile(`./public/data/${title}.txt`, "utf-8", function (
    err,
    description
  ) {
    var template = `
  <!DOCTYPE html>
<html>
  <head>
    <title>WEB1 - ${title}</title>
    <meta charset="utf-8" />
  </head>
  <body>
    <h1><a href="/">WEB</a></h1>
    <ul>
      <li><a href="/?id=HTML">HTML</a></li>
      <li><a href="/?id=CSS">CSS</a></li>
      <li><a href="/?id=JavaScript">JavaScript</a></li>
    </ul>
    <h2>${title}</h2>
    ${description}
  </body>
</html>`;
    console.log("title :>> ", title);
    response.end(template);
  });
});
app.listen(8080);
