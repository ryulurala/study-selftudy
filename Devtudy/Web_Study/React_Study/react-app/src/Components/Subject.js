import React, { Component } from "react";

class Subject extends Component {
  render() {
    console.log("Subject render");
    // 클래스 안에 소속돼 있는 함수는 "function"을 생략한다.
    // 컴포넌트 만들 때는 하나의 최상위 태그로 시작해야한다.
    return (
      <header>
        <h1>
          <a
            href="/"
            onClick={function (e) {
              e.preventDefault(); // 리로드를 막는다.
              this.props.onChangePage(); // props로 전달된 onChangePage() 함수 호출
            }.bind(this)}
          >
            {this.props.title}
          </a>
        </h1>
        {this.props.sub}
      </header>
    );
  }
}

export default Subject;
