import React, { useState, useEffect } from "react";
import "./App.css";

function App() {
  var [funcShow, setFuncShow] = useState(true);
  var [classShow, setClassShow] = useState(true);
  return (
    // funcShow, classShow가 true일 경우 실행
    <div className="container">
      <h1>Hello World</h1>
      <input
        type="button"
        value="remove func"
        onClick={function () {
          setFuncShow(false);
        }}
      />
      <input
        type="button"
        value="remove class"
        onClick={function () {
          setClassShow(false);
        }}
      />
      <input
        type="button"
        value="Insert func"
        onClick={function () {
          setFuncShow(true);
        }}
      />
      <input
        type="button"
        value="Insert class"
        onClick={function () {
          setClassShow(true);
        }}
      />
      {funcShow ? <FuncComp initNumber={2} /> : null}
      {classShow ? <ClassComp initNumber={2} /> : null}
    </div>
  );
}
var funcStyle = "color:blue";
var funcId = 0;
// 함수로 컴포넌트 구현
function FuncComp(props) {
  var [number, setNumber] = useState(props.initNumber);
  var [_date, setDate] = useState(new Date().toString());

  // side effect
  useEffect(
    function () {
      console.log(
        "%cfunc => useEffect number(componentDidMount & ComponentDidUpdate)" +
          ++funcId,
        funcStyle
      );
      document.title = number; // 타이틀을 바꿈.
      return function () {
        // 이전에 호출된 useEffect()를 정리하는 작업(clean up)
        console.log(
          console.log(
            "%cfunc => useEffect return(componentWillUnmount)" + ++funcId,
            funcStyle
          )
        );
      };
    },
    [number]
  );

  console.log("%cfunc => render" + ++funcId, funcStyle);
  return (
    <div className="container">
      <h2>function style component</h2>
      <p>Number: {number}</p>
      <p>date: {_date}</p>
      <input
        type="button"
        value="random"
        onClick={function () {
          setNumber(Math.random());
        }}
      />
      <input
        type="button"
        value="date"
        onClick={function () {
          setDate(new Date().toString());
        }}
      />
    </div>
  );
}

var classStyle = "color:red";
// 클래스로 컴포넌트 구현
class ClassComp extends React.Component {
  state = {
    number: this.props.initNumber,
    date: new Date().toString(),
  };
  componentWillMount() {
    console.log("%cclass => componentWillMount", classStyle);
  }
  componentDidMount() {
    console.log("%cclass => componentDidMount", classStyle);
  }
  shouldComponentUpdate(nextProps, nextState) {
    console.log("%cclass => shouldComponentUpdate", classStyle);
    // return false; // render() 호출 X
    return true; // render() 호출 O
  }
  componentWillUpdate(nextProps, nextState) {
    console.log("%cclass => componentWillUpdate", classStyle);
  }
  componentDidUpdate(nextProps, nextState) {
    console.log("%cclass => componentDidUpdate", classStyle);
  }
  componentWillUnmount() {
    console.log("%cclass => componentWillUnmount", classStyle);
  }
  render() {
    console.log("%cclass => render", classStyle);
    return (
      <div className="container">
        <h2>class style component</h2>
        <p>Number: {this.state.number}</p>
        <p>Date: {this.state.date}</p>
        <input
          type="button"
          value="random"
          onClick={function () {
            this.setState({ number: Math.random() });
          }.bind(this)}
        />
        <input
          type="button"
          value="date"
          onClick={function () {
            this.setState({ date: new Date().toString() });
          }.bind(this)}
        />
      </div>
    );
  }
}

export default App;
