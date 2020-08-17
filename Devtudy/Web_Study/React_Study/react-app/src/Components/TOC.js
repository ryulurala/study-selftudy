import React, { Component } from "react"; // 필수적임

class TOC extends Component {
  shouldComponentUpdate(newProps, newState) {
    // console.log("newProps.data :>> ", newProps.data);
    // console.log("props.data :>> ", this.props.data);
    if (this.props.data === newProps.data) {
      return false;
    }
    return true;
  }
  render() {
    console.log("TOC render");
    var lists = [];
    var data = this.props.data;
    var i = 0;
    while (i < data.length) {
      // Key는 리액트가 필요적으로 요청
      lists.push(
        <li key={data[i].id}>
          <a
            href={"/content/" + data[i].id}
            data-id={data[i].id} // dataset.id
            onClick={function (id, e) {
              // debugger; // e라는 객체의 정보를 보기 위해
              e.preventDefault(); // 리로드하지 않음.
              this.props.onChangePage(id); // target(=<a>), dataset(=data).id(=-id)
            }.bind(this, data[i].id)} // 두 번째 인자 값-> 함수의 첫 번째 매개변수 값으로 넣어줌
          >
            {data[i].title}
          </a>
        </li>
      );
      i++;
    }
    return (
      <nav>
        <ul>{lists}</ul>
      </nav>
    );
  }
}

export default TOC;
