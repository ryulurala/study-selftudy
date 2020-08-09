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
      subject: { title: "WEB", Sub: "World Wide Web!" },
      contents: [
        { id: 1, title: "HTML", desc: "THML is HyperText ..." },
        { id: 2, title: "CSS", desc: "CSS is for design" },
        { id: 3, title: "JavaScript", desc: "JavaScript is for interactive" },
      ],
    };
  }
  render() {
    return (
      <div className="App">
        <Subject
          title={this.state.subject.title}
          sub={this.state.subject.Sub}
        />
        <TOC data={this.state.contents} />
        <Content title="HTML" desc="HTML is HyperText Markup Language." />
      </div>
    ); // React가 최종적으로 html 코드를 공급해줌.
  }
}

export default App;
