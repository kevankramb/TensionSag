import React from "react";
import NumberInput from "./NumberInput";
import InputAdornment from '@material-ui/core/InputAdornment';
import TextField from '@material-ui/core/TextField';

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
            label="test (m)"
            name="length"
            onChange={this.onChange}
          />

        <TextField
          id="wire-name"
          variant="outlined"
          label="Wire Name"        
        />

        <TextField
          id="maxRatedStrength"
          variant="outlined"
          label="Max Rated Strength"
          onChange={this.onChange}
          helperText=" "
          InputProps={{
            endAdornment: <InputAdornment position="end">N</InputAdornment>,
          }}
        />

        <TextField
          id="totalCrossSection"
          variant="outlined"
          label="Cross Section"
          onChange={this.onChange}
          helperText="Total Conductor"
          InputProps={{
            endAdornment: <InputAdornment position="end">m^2</InputAdornment>,
          }}
        />

        <TextField
          id="coreCrossSection"
          variant="outlined"
          label="Cross Section"
          onChange={this.onChange}
          helperText="Core Only"
          InputProps={{
            endAdornment: <InputAdornment position="end">m^2</InputAdornment>,
          }}
        />

        <TextField
          id="initialWireDiameter"
          variant="outlined"
          label="Diameter"
          onChange={this.onChange}
          helperText="Initial or Messenger Only"
          InputProps={{
            endAdornment: <InputAdornment position="end">m</InputAdornment>,
          }}
        />

        <TextField
          id="finalWireDiameter"
          variant="outlined"
          label="Diameter"
          onChange={this.onChange}
          helperText="Final/Complete Diameter"
          InputProps={{
            endAdornment: <InputAdornment position="end">m</InputAdornment>,
          }}
        />

        <TextField
          id="initialLinearWeight"
          variant="outlined"
          label="Linear Weight"
          onChange={this.onChange}
          helperText="Initial or Messenger Only"
          InputProps={{
            endAdornment: <InputAdornment position="end">N/m</InputAdornment>,
          }}
        />

        <TextField
          id="finalLinearWeight"
          variant="outlined"
          label="Linear Weight"
          onChange={this.onChange}
          helperText="Final/Completed Linear Weight"
          InputProps={{
            endAdornment: <InputAdornment position="end">N/m</InputAdornment>,
          }}
        />

        <TextField
          id="outerElasticity"
          variant="outlined"
          label="Final Modulus"
          onChange={this.onChange}
          helperText="Outer Stranding Only"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="outerThermalCoeff"
          variant="outlined"
          label="Expansion Coeff"
          onChange={this.onChange}
          helperText="Outer Stranding Only"
          InputProps={{
            endAdornment: <InputAdornment position="end">1/C</InputAdornment>,
          }}
        />

        <TextField
          id="coreElasticity"
          variant="outlined"
          label="Final Modulus"
          onChange={this.onChange}
          helperText="Core Only"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="coreThermalCoeff"
          variant="outlined"
          label="Expansion Coeff"
          onChange={this.onChange}
          helperText="Core Only"
          InputProps={{
            endAdornment: <InputAdornment position="end">1/C</InputAdornment>,
          }}
        />

        <TextField
          id="outerStressStrain0"
          variant="outlined"
          label="Stress-Strain a0"
          onChange={this.onChange}
          helperText="Outer a0"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="outerStressStrain1"
          variant="outlined"
          label="Stress-Strain a1"
          onChange={this.onChange}
          helperText="Outer a1"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />      
        
        <TextField
          id="outerStressStrain2"
          variant="outlined"
          label="Stress-Strain a2"
          onChange={this.onChange}
          helperText="Outer a2"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />      
      
        <TextField
          id="outerStressStrain3"
          variant="outlined"
          label="Stress-Strain a3"
          onChange={this.onChange}
          helperText="Outer a3"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="outerStressStrain4"
          variant="outlined"
          label="Stress-Strain a4"
          onChange={this.onChange}
          helperText="Outer a4"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="outerCreep0"
          variant="outlined"
          label="Creep a0"
          onChange={this.onChange}
          helperText="Outer a0"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="outerCreep1"
          variant="outlined"
          label="Creep a1"
          onChange={this.onChange}
          helperText="Outer a1"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />      
        
        <TextField
          id="outerCreep2"
          variant="outlined"
          label="Creep a2"
          onChange={this.onChange}
          helperText="Outer a2"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />      
      
        <TextField
          id="outerCreep3"
          variant="outlined"
          label="Creep a3"
          onChange={this.onChange}
          helperText="Outer a3"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="outerCreep4"
          variant="outlined"
          label="Creep a4"
          onChange={this.onChange}
          helperText="Outer a4"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="coreStressStrain0"
          variant="outlined"
          label="Stress-Strain a0"
          onChange={this.onChange}
          helperText="Core a0"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="coreStressStrain1"
          variant="outlined"
          label="Stress-Strain a1"
          onChange={this.onChange}
          helperText="Core a1"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />      
        
        <TextField
          id="coreStressStrain2"
          variant="outlined"
          label="Stress-Strain a2"
          onChange={this.onChange}
          helperText="Core a2"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />      
      
        <TextField
          id="coreStressStrain3"
          variant="outlined"
          label="Stress-Strain a3"
          onChange={this.onChange}
          helperText="Core a3"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="coreStressStrain4"
          variant="outlined"
          label="Stress-Strain a4"
          onChange={this.onChange}
          helperText="Core a4"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="coreCreep0"
          variant="outlined"
          label="Creep a0"
          onChange={this.onChange}
          helperText="Core a0"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="coreCreep1"
          variant="outlined"
          label="creep a1"
          onChange={this.onChange}
          helperText="Core a1"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />      
        
        <TextField
          id="coreCreep2"
          variant="outlined"
          label="Creep a2"
          onChange={this.onChange}
          helperText="Core a2"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />      
      
        <TextField
          id="coreCreep3"
          variant="outlined"
          label="Creep a3"
          onChange={this.onChange}
          helperText="Core a3"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        <TextField
          id="coreCreep4"

          variant="outlined"
          label="Creep a4"
          onChange={this.onChange}
          helperText="Core a4"
          InputProps={{
            endAdornment: <InputAdornment position="end">Pa</InputAdornment>,
          }}
        />

        </form>

      </div>
    );
  }
}
