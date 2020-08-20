import React, { useState, useEffect } from "react";
import "./App.css";

function App() {
  return (
    <div className="container">
      <h1>Hello World</h1>
      <FuncComp initNumber={2} />
      {/* <ClassComp initNumber={2} /> */}
    </div>
  );
}
var funcStyle = "color:blue";
var funcId = 0;
// 함수로 컴포넌트 구현
function FuncComp(props) {
  var [number, setNumber] = useState(props.initNumber);
  var [_date, setDate] = useState(new Date().toString());

  useEffect(function () {
    console.log("%cfunc => useEffect" + ++funcId, funcStyle);
    document.title = number + ":" + _date;
  });

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
