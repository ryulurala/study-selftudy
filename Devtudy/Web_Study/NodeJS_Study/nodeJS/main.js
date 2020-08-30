var http = require("http");
var fs = require("fs");
var url = require("url"); // url 모듈을 사용

function templateHTML(title, list, body) {
  return `
    <!DOCTYPE html>
    <html>
      <head>
        <title>WEB1 - ${title}</title>
        <meta charset="utf-8" />
      </head>
      <body>
        <h1><a href="/">WEB</a></h1>
        ${list}
        ${body}
      </body>
    </html>`;
}

function templateList(files) {
  var list = "<ul>";
  for (i = 0; i < files.length; i++) {
    var index = files[i].lastIndexOf(".");
    files[i] = files[i].substring(0, index);
    list += `<li><a href="/?id=${files[i]}">${files[i]}</a></li>`;
  }
  list += "</ul>";
  return list;
}

var app = http.createServer(function (request, response) {
  var _url = request.url;
  var queryData = url.parse(_url, true).query;
  var pathName = url.parse(_url, true).pathname;
  var title = queryData.id;

  if (pathName === "/") {
    if (title === undefined) {
      fs.readdir("./public/data", function (err, files) {
        var list = templateList(files);
        var description = "Hello, Node.js";
        var template = templateHTML(
          title,
          list,
          `<h2>${title}</h2>${description}`
        );
        response.writeHead(200); // 파일을 성공적으로 전송
        response.end(template);
      });
    } else {
      fs.readdir("./public/data", function (err, files) {
        fs.readFile(`./public/data/${queryData.id}.txt`, "utf-8", function (
          err,
          description
        ) {
          var list = templateList(files);
          var title = queryData.id;
          var template = templateHTML(
            title,
            list,
            `<h2>${title}</h2>${description}`
          );
          response.writeHead(200); // 파일을 성공적으로 전송
          response.end(template);
        });
      });
    }
  } else {
    response.writeHead(404); // 파일을 찾을 수 없다는 코드 전송
    response.end("Not found!");
  }
});
app.listen(8080);
