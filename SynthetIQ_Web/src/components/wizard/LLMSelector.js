import React from 'react';
import { FormControl, FormGroup, FormControlLabel, Checkbox } from '@material-ui/core';

const llms = [
    { id: 'openai', name: 'OpenAI' },
    { id: 'claude', name: 'Claude' },
    // Add more LLMs as needed
];

export default function LLMSelector({ selectedLLMs, onSelectionChange }) {
    const handleChange = (event) => {
        onSelectionChange(event.target.name, event.target.checked);
    };

    return (
        <FormControl component="fieldset">
            <FormGroup>
                {llms.map((llm) => (
                    <FormControlLabel
                        key={llm.id}
                        control={<Checkbox checked={selectedLLMs.includes(llm.id)} onChange={handleChange} name={llm.id} />}
                        label={llm.name}
                    />
                ))}
            </FormGroup>
        </FormControl>
    );
}