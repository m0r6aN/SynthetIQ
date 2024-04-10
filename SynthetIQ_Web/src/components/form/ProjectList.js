//
// Displays a list of projects with options to view, edit, or delete.
// 

import React from 'react';
import { List, ListItem, ListItemText, ListItemSecondaryAction, IconButton } from '@material-ui/core';
import { Edit, Delete, Visibility } from '@material-ui/icons';

export const ProjectList = ({ projects, onEdit, onDelete, onView }) => (
  <List>
    {projects.map((project) => (
      <ListItem key={project.id}>
        <ListItemText primary={project.name} secondary={project.description} />
        <ListItemSecondaryAction>
          <IconButton edge="end" aria-label="view" onClick={() => onView(project.id)}>
            <Visibility />
          </IconButton>
          <IconButton edge="end" aria-label="edit" onClick={() => onEdit(project.id)}>
            <Edit />
          </IconButton>
          <IconButton edge="end" aria-label="delete" onClick={() => onDelete(project.id)}>
            <Delete />
          </IconButton>
        </ListItemSecondaryAction>
      </ListItem>
    ))}
  </List>
);