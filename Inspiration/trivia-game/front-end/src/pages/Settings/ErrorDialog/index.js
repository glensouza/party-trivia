import PropTypes from 'prop-types';
import {
  Button, Dialog, DialogActions,
  DialogContent, DialogContentText, DialogTitle,
} from '@mui/material';
import React from 'react';

export default function ErrorDialog({ openError, setOpenError }) {
  return (
    <Dialog
      open={openError}
      onClose={() => setOpenError(false)}
      aria-labelledby="alert-dialog-title"
      aria-describedby="alert-dialog-description"
    >
      <DialogTitle id="alert-dialog-title">
        Error: Unable to find questions with the current settings.
      </DialogTitle>
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          Please change the Category, Difficulty and/or Type.
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={() => setOpenError(false)}
          autofocus
        >
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
}

ErrorDialog.propTypes = {
  openError: PropTypes.bool.isRequired,
  setOpenError: PropTypes.func.isRequired,
};
