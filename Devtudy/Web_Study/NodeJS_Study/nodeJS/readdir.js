var testFolder = "./data";
var fs = require("fs");

fs.readdir(testFolder, function (err, files) {
  console.log(files);
});
