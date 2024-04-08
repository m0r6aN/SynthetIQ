import React, { useState } from 'react';
import { TextField, Button, Paper, Typography } from '@material-ui/core';
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

export default function LLMConfiguration({ onSave }) {
    const classes = useStyles();

    const [config, setConfig] = useState({
        name: '',
        apiUrl: '',
        endpoints: '',
        apiKey: '',
    });

    const handleChange = (event) => {
        const { name, value } = event.target;
        setConfig((prevConfig) => ({
            ...prevConfig,
            [name]: value,
        }));
    };

    const handleSave = () => {
        onSave(config);
    };

    return (
        <Paper className={classes.root}>
            <Typography variant="h6">LLM Configuration</Typography>
            <TextField
                className={classes.field}
                fullWidth
                label="Name"
                name="name"
                value={config.name}
                onChange={handleChange}
                variant="outlined"
            />
            <TextField
                className={classes.field}
                fullWidth
                label="API URL"
                name="apiUrl"
                value={config.apiUrl}
                onChange={handleChange}
                variant="outlined"
            />
            <TextField
                className={classes.field}
                fullWidth
                label="Endpoints (comma-separated)"
                name="endpoints"
                value={config.endpoints}
                onChange={handleChange}
                variant="outlined"
            />
            <TextField
                className={classes.field}
                fullWidth
                label="API Key"
                name="apiKey"
                value={config.apiKey}
                onChange={handleChange}
                variant="outlined"
                type="password"
            />
            <Button
                className={classes.button}
                variant="contained"
                color="primary"
                onClick={handleSave}
            >
                Save Configuration
            </Button>
        </Paper>
    );
}
