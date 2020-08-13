import React, { Component } from "react";

class Control extends Component {
  render() {
    // 클래스 안에 소속돼 있는 함수는 "function"을 생략한다.
    // 컴포넌트 만들 때는 하나의 최상위 태그로 시작해야한다.
    return (
      <ul>
        <li>
          <a
            href="/create"
            onClick={function (e) {
              e.preventDefault();
              this.props.onChangeMode("create");
            }.bind(this)}
          >
            Create
          </a>
        </li>
        <li>
          <a
            href="/update"
            onClick={function (e) {
              e.preventDefault();
              this.props.onChangeMode("update");
            }.bind(this)}
          >
            Update
          </a>
        </li>
        <li>
          <input
            type="button"
            value="delete"
            onClick={function (e) {
              e.preventDefault();
              this.props.onChangeMode("delete");
            }.bind(this)}
          />
        </li>
      </ul>
    );
  }
}

export default Control;
