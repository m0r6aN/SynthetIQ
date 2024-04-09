import React, { useState } from 'react';
import { TextField, Button, makeStyles } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
  formControl: {
    margin: theme.spacing(1),
    minWidth: 120,
    width: '100%',
  },
  selectEmpty: {
    marginTop: theme.spacing(2),
  },
}));

export const ProjectDescriptionComponent = ({ onContinue }) => {
  const classes = useStyles();
  const [projectName, setProjectName] = useState('');
  const [description, setDescription] = useState('');

  return (
    <div>
      <TextField
        className={classes.formControl}
        label="Project Name"
        value={projectName}
        onChange={(e) => setProjectName(e.target.value)}
        variant="outlined"
      />
      <TextField
        className={classes.formControl}
        label="Description"
        value={description}
        multiline
        rows={4}
        onChange={(e) => setDescription(e.target.value)}
        variant="outlined"
      />
      <Button
        variant="contained"
        color="primary"
        onClick={() => onContinue({ projectName, description })}
      >
        Continue
      </Button>
    </div>
  );
};
