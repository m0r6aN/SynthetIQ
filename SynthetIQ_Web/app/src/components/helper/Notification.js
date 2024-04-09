import React from 'react';
import { Snackbar } from '@material-ui/core';

const Notification = ({ message, open, onClose }) => (
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

export default Notification;