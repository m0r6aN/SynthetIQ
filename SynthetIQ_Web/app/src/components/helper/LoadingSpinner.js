import React from 'react';
import { CircularProgress } from '@material-ui/core';

const LoadingSpinner = () => (
  <div style={{ display: 'flex', justifyContent: 'center', padding: '20px' }}>
    <CircularProgress />
  </div>
);

export default LoadingSpinner;