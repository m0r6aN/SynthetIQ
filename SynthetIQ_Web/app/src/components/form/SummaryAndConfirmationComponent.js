import React from 'react';
import { Typography, List, ListItem, ListItemText, Button } from '@material-ui/core';

export const SummaryAndConfirmationComponent = ({ projectData, onBack, onFinish }) => {
  // Assume projectData contains all the necessary project configuration details
  return (
    <div>
      <Typography variant="h6">Summary</Typography>
      <List>
        <ListItem>
          <ListItemText primary="Project Name" secondary={projectData.projectName} />
        </ListItem>
        <ListItem>
          <ListItemText primary="Description" secondary={projectData.description} />
        </ListItem>
        {/* Render more project details as needed */}
      </List>
      <Button variant="contained" onClick={onBack}>Back</Button>
      <Button variant="contained" color="primary" onClick={onFinish}>
        Finish
      </Button>
    </div>
  );
};
