import React, { useState, useEffect } from 'react';
import { TextField, Button, Paper, Typography, MenuItem } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    root: {
        padding: theme.spacing(3),
        margin: theme.spacing(2, 0),
    },
    field: {
        margin: theme.spacing(1, 0),
    },
    button: {
        marginTop: theme.spacing(2),
    },
}));

// Mock data for LLMs, replace with your data source
const llms = [
    { id: 'openai', name: 'OpenAI' },
    { id: 'claude', name: 'Claude' },
    // Add more LLMs as necessary
];

export default function AssistantForm({ initialAssistant, onSave }) {
    const classes = useStyles();

    const [assistant, setAssistant] = useState({
        name: '',
        description: '',
        llmId: '',
        // Add other fields as necessary
    });

    useEffect(() => {
        if (initialAssistant) {
            setAssistant(initialAssistant);
        }
    }, [initialAssistant]);

    const handleChange = (event) => {
        const { name, value } = event.target;
        setAssistant((prevAssistant) => ({
            ...prevAssistant,
            [name]: value,
        }));
    };

    const handleSubmit = () => {
        onSave(assistant);
    };

    return (
        <Paper className={classes.root}>
            <Typography variant="h6">{initialAssistant ? 'Edit Assistant' : 'Create Assistant'}</Typography>
            <TextField
                className={classes.field}
                fullWidth
                label="Name"
                name="name"
                value={assistant.name}
                onChange={handleChange}
                variant="outlined"
            />
            <TextField
                className={classes.field}
                fullWidth
                label="Description"
                name="description"
                value={assistant.description}
                onChange={handleChange}
                variant="outlined"
            />
            <TextField
                className={classes.field}
                fullWidth
                select
                label="Associated LLM"
                name="llmId"
                value={assistant.llmId}
                onChange={handleChange}
                variant="outlined"
            >
                {llms.map((llm) => (
                    <MenuItem key={llm.id} value={llm.id}>
                        {llm.name}
                    </MenuItem>
                ))}
            </TextField>
            <Button
                className={classes.button}
                variant="contained"
                color="primary"
                onClick={handleSubmit}
            >
                {initialAssistant ? 'Save Changes' : 'Create Assistant'}
            </Button>
        </Paper>
    );
}
