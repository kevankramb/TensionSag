import React from "react";

export default class NumberInput extends React.Component {
  constructor(props) {
    super(props);
    this.state = { value: 0 };
    this.onChange = this.onChange.bind(this);
  }

  onChange(event) {
    this.setState(
      { value: event.target.value },
      this.props.onChange(this.props.name, parseInt(this.state.value, 10))
    );
  }

  render() {
    return (
      <div>
        <label>
          {this.props.label} &nbsp;
          <input
            type="number"
            value={this.state.value}
            name={this.props.name}
            onChange={this.onChange}
          />
        </label>
      </div>
    );
  }
}
