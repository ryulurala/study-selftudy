var http = require("http");
var fs = require("fs");
var url = require("url"); // url 모듈을 사용
var qs = require("querystring");

function templateHTML(title, list, body, control = "") {
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
        ${control}
        ${body}
      </body>
    </html>`;
}

function templateList(files) {
  var list = "<ul>";
  for (i = 0; i < files.length; i++) {
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
  console.log(pathName);
  if (pathName === "/") {
    if (title === undefined) {
      fs.readdir("./public/data", function (err, files) {
        var title = "Welcome";
        var list = templateList(files);
        var description = "Hello, Node.js";
        var template = templateHTML(
          title,
          list,
          `<h2>${title}</h2>${description}`,
          `<a href="/create">create</a>`
        );
        response.writeHead(200); // 파일을 성공적으로 전송
        response.end(template);
      });
    } else {
      fs.readdir("./public/data", function (err, files) {
        fs.readFile(`./public/data/${queryData.id}`, "utf-8", function (
          err,
          description
        ) {
          var list = templateList(files);
          var title = queryData.id;
          var template = templateHTML(
            title,
            list,
            `<h2>${title}</h2>${description}`,
            `
            <a href="/create">create</a>
            <a href="/update?id=${title}">update</a>
            <form action="/delete_process" method="post">
              <input type="hidden" name="id" value="${title}">
              <input type="submit" value="delete">
            </form>
            `
          );
          response.writeHead(200); // 파일을 성공적으로 전송
          response.end(template);
        });
      });
    }
  } else if (pathName === "/create") {
    fs.readdir("./public/data", function (err, files) {
      var title = "WEB - create";
      var list = templateList(files);
      var template = templateHTML(
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
      response.end(template);
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
      fs.writeFile(`public/data/${title}`, description, "utf8", function (err) {
        // 파일 저장이 끝난 후
        response.writeHead(302, { Location: `/?id=${title}` }); // 302: 리다이렉션
        response.end();
      });
    });
  } else if (pathName === "/update") {
    fs.readdir("./public/data", function (err, files) {
      fs.readFile(`./public/data/${queryData.id}`, "utf-8", function (
        err,
        description
      ) {
        var list = templateList(files);
        var title = queryData.id;
        var template = templateHTML(
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
        response.end(template);
      });
    });
  } else if ((pathName = "update_process")) {
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
      fs.rename(`public/data/${id}`, `public/data/${title}`, function (err) {
        fs.writeFile(`public/data/${title}`, description, "utf8", function (
          err
        ) {
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
    request.end("end", function () {
      var post = qs.parse(body);
      var id = post.id;
      fs.unlink(`public/data/${id}`, function (err) {
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
