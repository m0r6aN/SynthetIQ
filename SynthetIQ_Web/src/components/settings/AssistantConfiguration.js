//
// Allows users to configure AI assistants for their project. For simplicity, let's assume it allows toggling available assistants.
//

import React, { useState } from 'react';
import { FormGroup, FormControlLabel, Checkbox, Button } from '@material-ui/core';

export const AssistantConfigurationComponent = ({ onContinue, onBack }) => {
  const [selectedAssistants, setSelectedAssistants] = useState([]);

  const handleChange = (event) => {
    const index = selectedAssistants.indexOf(event.target.name);
    if (index > -1) {
      setSelectedAssistants(selectedAssistants.filter((item) => item !== event.target.name));
    } else {
      setSelectedAssistants([...selectedAssistants, event.target.name]);
    }
  };

  return (
    <div>
      <FormGroup>
        <FormControlLabel
          control={<Checkbox onChange={handleChange} name="developer" />}
          label="Developer Assistant"
        />
        <FormControlLabel
          control={<Checkbox onChange={handleChange} name="architect" />}
          label="Architect Assistant"
        />
        {/* Add more assistants as needed */}
      </FormGroup>
      <Button variant="contained" onClick={onBack}>Back</Button>
      <Button variant="contained" color="primary" onClick={() => onContinue(selectedAssistants)}>
        Continue
      </Button>
    </div>
  );
};
