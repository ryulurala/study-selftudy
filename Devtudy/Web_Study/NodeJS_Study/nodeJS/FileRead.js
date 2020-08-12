var fs = require("fs"); // file system 모듈 추가

fs.readFile("./public/data/sample.txt", "utf-8", function (err, data) {
  // utf-8로 옵션 이용하여 출력
  // node를 실행한 위치(해당 .js파일의 위치가 아니다)
  console.log(data);
});
