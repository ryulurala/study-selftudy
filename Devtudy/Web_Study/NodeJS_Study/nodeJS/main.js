var http = require("http");
var fs = require("fs");
var url = require("url"); // url 모듈을 사용
var qs = require("querystring");
var template = require("./lib/template.js");
var path = require("path");
var sanitizeHTML = require("sanitize-html");

var app = http.createServer(function (request, response) {
  var _url = request.url;
  var queryData = url.parse(_url, true).query;
  var pathName = url.parse(_url, true).pathname;
  console.log(`path = ${pathName}`);
  if (pathName === "/") {
    if (queryData.id === undefined) {
      fs.readdir("./data", function (err, files) {
        var title = "Welcome";
        var list = template.list(files);
        var description = "Hello, Node.js";
        var html = template.html(
          title,
          list,
          `<h2>${title}</h2>${description}`,
          `<a href="/create">create</a>`
        );
        response.writeHead(200); // 파일을 성공적으로 전송
        response.end(html);
      });
    } else {
      fs.readdir("./data", function (err, files) {
        var filteredId = path.parse(queryData.id).base;
        fs.readFile(`data/${filteredId}`, "utf8", function (err, description) {
          var title = filteredId;
          var list = template.list(files);
          var html = template.html(
            title,
            list,
            `<h2>${title}</h2>${description}`,
            `
            <a href="/update?id=${title}">update</a>
            <form action="delete_process" method="post">
              <input type="hidden" name="id" value="${title}">
              <input type="submit" value="delete">
            </form>
            `
          );
          response.writeHead(200); // 파일을 성공적으로 전송
          response.end(html);
        });
      });
    }
  } else if (pathName === "/create") {
    fs.readdir("./data", function (err, files) {
      var title = "create";
      var list = template.list(files);
      var html = template.html(
        title,
        list,
        `
        <form action="/create_process" method="post">
          <p>
            <input type="text" name="title" placeholder="title">
          </p>
          <p>
            <textarea name="description" placeholder="description"></textarea>
          </p>
          <p>
            <input type="submit">
          </p>
        </form>
        `
      );
      response.writeHead(200); // 파일을 성공적으로 전송
      response.end(html);
    });
  } else if (pathName === "/create_process") {
    // Post 방식의 request 데이터 받기
    var body = "";
    request.on("data", function (data) {
      // 주기마다 실행
      body += data; // 콜백이 실행할 때마다 추가
    });
    request.on("end", function () {
      // 마지막 수신
      var post = qs.parse(body);
      var title = post.title;
      var description = post.description;
      fs.writeFile(`data/${title}`, description, "utf8", function (err) {
        // 파일 저장이 끝난 후
        response.writeHead(302, { Location: `/?id=${title}` }); // 302: 리다이렉션
        response.end();
      });
    });
  } else if (pathName === "/update") {
    fs.readdir("./data", function (err, files) {
      var filteredId = path.parse(queryData.id).base;
      fs.readFile(`data/${filteredId}`, "utf8", function (err, description) {
        var list = template.list(files);
        var title = filteredId;
        var html = template.html(
          title,
          list,
          `
          <form action="/update_process" method="post">
            <input type="hidden" name="id" value="${title}">
            <p>
              <input type="text" name="title" placeholder="title" value="${title}">
            </p>
            <p>
              <textarea name="description" placeholder="description">${description}</textarea>
            </p>
            <p>
              <input type="submit">
            </p>
          </form>
          `,
          `<a href="/create">create</a>
          <a href="/update?id=${title}">update</a>`
        );
        response.writeHead(200); // 파일을 성공적으로 전송
        response.end(html);
      });
    });
  } else if (pathName === "/update_process") {
    // Post 방식의 request 데이터 받기
    var body = "";
    request.on("data", function (data) {
      // 주기마다 실행
      body += data; // 콜백이 실행할 때마다 추가
    });
    request.on("end", function () {
      // 마지막 수신
      var post = qs.parse(body);
      var title = post.title;
      var description = post.description;
      var id = post.id;
      fs.rename(`data/${id}`, `data/${title}`, function (err) {
        fs.writeFile(`data/${title}`, description, "utf8", function (err) {
          // 파일 저장이 끝난 후
          response.writeHead(302, { Location: `/?id=${title}` }); // 302: 리다이렉션
          response.end();
        });
      });
    });
  } else if (pathName === "/delete_process") {
    // Post 방식의 request 데이터 받기
    var body = "";
    request.on("data", function (data) {
      // 주기마다 실행
      body += data; // 콜백이 실행할 때마다 추가
    });
    request.on("end", function () {
      var post = qs.parse(body);
      var id = post.id;
      var filteredId = path.parse(id).base;
      fs.unlink(`data/${filteredId}`, function (err) {
        response.writeHead(302, { Location: `/` }); // 302: 리다이렉션
        response.end();
      });
    });
  } else {
    response.writeHead(404); // 파일을 찾을 수 없다는 코드 전송
    response.end("Not found!");
  }
});
app.listen(8080);
