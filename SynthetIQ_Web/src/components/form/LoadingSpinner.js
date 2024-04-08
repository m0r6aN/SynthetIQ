//
// A simple spinner for indicating loading states.
//

import React from 'react';
import { CircularProgress } from '@material-ui/core';

export const LoadingSpinner = () => (
  <div style={{ display: 'flex', justifyContent: 'center', padding: '20px' }}>
    <CircularProgress />
  </div>
);