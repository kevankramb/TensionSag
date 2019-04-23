import React from "react";
import WireForm from "./WireForm";
import WireOutput from "./WireOutput";

export default class WireProperties extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  onCalculate() {}

  render() {
    return (
      <div>
        <h1>Wire Properties</h1>
        <WireForm />
        <WireOutput />
      </div>
    );
  }
}
