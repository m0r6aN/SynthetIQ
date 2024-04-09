import React from 'react';
import { FormControl, FormGroup, FormControlLabel, Checkbox } from '@material-ui/core';

const assistants = [
    { id: 'arch', name: 'Architecture Assistant' },
    { id: 'dev', name: 'Developer Assistant' },
    { id: 'doc', name: 'Documentation Assistant' },
    { id: 'mark', name: 'Marketing Assistant' },
    { id: 'prom', name: 'Prompt Optimization Assistant' },
    { id: 'pmgr', name: 'Project Manager Assistant' },
    { id: 'res', name: 'Research Assistant' },
    { id: 'qa', name: 'Quality Assurance Assistant' },
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
