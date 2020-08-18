var http = require("http");
var fs = require("fs");
var url = require("url"); // url 모듈을 사용

var app = http.createServer(function (request, response) {
  var _url = request.url;
  var queryData = url.parse(_url, true).query;
  var pathName = url.parse(_url, true).pathname;
  var title = queryData.id;

  // console.log("pathName :>> ", pathName);
  // console.log("queryData :>> ", queryData);
  if (pathName === "/") {
    if (title === undefined) {
      fs.readFile(`./public/data/${title}.txt`, "utf-8", function (
        err,
        description
      ) {
        title = "Welcome";
        var description = "Hello, Node.js";
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
        response.writeHead(200); // 파일을 성공적으로 전송
        response.end(template);
      });
    } else {
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
        response.writeHead(200); // 파일을 성공적으로 전송
        response.end(template);
      });
    }
  } else {
    response.writeHead(404); // 파일을 찾을 수 없다는 코드 전송
    response.end("Not found!");
  }
});
app.listen(8080);
