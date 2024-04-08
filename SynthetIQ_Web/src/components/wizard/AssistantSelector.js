import React from 'react';
import { FormControl, FormGroup, FormControlLabel, Checkbox } from '@material-ui/core';

const assistants = [
    { id: 'dev', name: 'Developer Assistant' },
    { id: 'arch', name: 'Architecture Assistant' },
    // Add more assistants as needed
];

export default function AssistantSelector({ selectedAssistants, onSelectionChange }) {
    const handleChange = (event) => {
        onSelectionChange(event.target.name, event.target.checked);
    };

    return (
        <FormControl component="fieldset">
            <FormGroup>
                {assistants.map((assistant) => (
                    <FormControlLabel
                        key={assistant.id}
                        control={<Checkbox checked={selectedAssistants.includes(assistant.id)} onChange={handleChange} name={assistant.id} />}
                        label={assistant.name}
                    />
                ))}
            </FormGroup>
        </FormControl>
    );
}
