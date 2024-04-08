//
// A simple implementation for displaying notifications. A more comprehensive solution might use a library like notistack for better control and features.
//

import React from 'react';
import { Snackbar } from '@material-ui/core';

export const Notification = ({ message, open, onClose }) => (
  <Snackbar
    anchorOrigin={{
      vertical: 'bottom',
      horizontal: 'left',
    }}
    open={open}
    autoHideDuration={6000}
    onClose={onClose}
    message={message}
  />
);