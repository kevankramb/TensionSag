import React from "react";
import NumberInput from "./NumberInput";

export default class WireForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = { valid: false };
    this.onChange = this.onChange.bind(this);
  }

  onSubmit(event) {
    event.preventDefault();
  }

  onChange(name, value) {
    this.setState({ [name]: value });
    if (this.isValid) this.onValid();
  }

  isValid() {
    return true;
  }

  onValid() {
    console.log("posting data");
  }

  render() {
    return (
      <div>
        <form onSubmit={this.onSubmit}>
          <NumberInput
            label="Length (m)"
            name="length"
            onChange={this.onChange}
          />
        </form>
      </div>
    );
  }
}
