module.exports = {
  html: function (title, list, body, control = "") {
    return `
          <!DOCTYPE html>
          <html>
            <head>
              <title>WEB - ${title}</title>
              <meta charset="utf-8" />
            </head>
            <body>
              <h1><a href="/">WEB</a></h1>
              ${list}
              ${control}
              ${body}
            </body>
          </html>
          `;
  },
  list: function (files) {
    console.log(`files = ${files}`);
    var list = "<ul>";
    for (i = 0; i < files.length; i++) {
      list += `<li><a href="/?id=${files[i]}">${files[i]}</a></li>`;
    }
    list += "</ul>";
    return list;
  },
};
