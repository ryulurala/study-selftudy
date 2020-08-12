import React, { Component } from "react";
import "./App.css";
import TOC from "./Components/TOC";
import Content from "./Components/Content";
import Subject from "./Components/Subject";

class App extends Component {
  constructor(props) {
    // 생성자
    super(props); // 초기화
    this.state = {
      mode: "read",
      selected_content_id: 2,
      subject: { title: "WEB", sub: "World Wide Web!" },
      welcome: { title: "Welcome", desc: "Hello, React!!" },
      contents: [
        { id: 1, title: "HTML", desc: "THML is HyperText ..." },
        { id: 2, title: "CSS", desc: "CSS is for design" },
        { id: 3, title: "JavaScript", desc: "JavaScript is for interactive" },
      ],
    };
  }
  render() {
    // 화면을 그리는 함수
    console.log("App render");
    var _title,
      _desc = null;
    if (this.state.mode === "welcome") {
      _title = this.state.welcome.title;
      _desc = this.state.welcome.desc;
    } else if (this.state.mode === "read") {
      var i = 0;
      while (i < this.state.contents.length) {
        var data = this.state.contents[i];
        if (data.id === this.state.selected_content_id) {
          _title = data.title;
          _desc = data.desc;
          break;
        }
        i++;
      }
    }
    // console.log("render :>> ", this);
    return (
      <div className="App">
        <Subject
          title={this.state.subject.title}
          sub={this.state.subject.sub}
          onChangePage={function () {
            // 함수가 전달됨
            this.setState({ mode: "welcome" });
          }.bind(this)}
        />
        <TOC
          onChangePage={function (id) {
            // debugger;
            this.setState({
              mode: "read",
              selected_content_id: Number(id),
            });
          }.bind(this)}
          data={this.state.contents}
        />
        <Content title={_title} desc={_desc} />
      </div>
    ); // React가 최종적으로 html 코드를 공급해줌.
  }
}

export default App;
