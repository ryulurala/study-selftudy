var Links = {
  setColor: function (color) {
    // var aList = document.querySelectorAll("a");
    // var i = 0;
    // while (i < aList.length) {
    //   // console.log(aList[i]);
    //   aList[i].style.color = color;
    //   i++;
    // }
    $("a").css("color", color);
  },
};
var Body = {
  setColor: function (color) {
    // document.querySelector("body").style.color = color;
    $("body").css("color", color);
  },
  setBackgroundColor: function (color) {
    // document.querySelector("body").style.backgroundColor = color;
    $("body").css("background-color", color);
  },
};

function nightDayHandler(self) {
  if (self.value === "night") {
    Body.setBackgroundColor("black");
    Body.setColor("white");
    self.value = "day";
    Links.setColor("powderBlue");
  } else {
    Body.setBackgroundColor("white");
    Body.setColor("black");
    self.value = "night";
    Links.setColor("blue");
  }
}
