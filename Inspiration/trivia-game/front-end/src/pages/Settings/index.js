import PropTypes from 'prop-types';
import {
  Button, DialogActions,
  DialogContent, DialogTitle, Typography, Grid, Stack, Dialog,
} from '@mui/material';
import { SettingsOutlined } from '@mui/icons-material';
import React, { useState } from 'react';
import { connect } from 'react-redux';
import { fetchQuestions } from '../../services/apiHelper';
import BackdropComp from '../../components/Backdrop';
import theme from '../../theme';
import SettingsSelect from './SettingsSelect';
import { setPlayerSettings } from '../../store/actions';
import ErrorDialog from './ErrorDialog';

function Settings({ setOpenSettings, sendSettings, openSettings }) {
  const [openBackdrop, setOpenBackdrop] = useState(false);
  const [openError, setOpenError] = useState(false);
  const [settings, setSettings] = useState({
    category: 'All',
    difficulty: 'All',
    type: 'All',
  });
  const categories = JSON.parse(localStorage.getItem('categories'));

  function handleClick() {
    sendSettings(settings);
    setOpenBackdrop(true);
    const token = localStorage.getItem('token');
    fetchQuestions(token, settings)
      .then((questions) => {
        if (!questions.length) {
          setOpenError(true);
          setOpenBackdrop(false);
        } else {
          setOpenSettings(false);
          setOpenBackdrop(false);
        }
      });
  }

  function handleChange({ target: { name, value } }) {
    if (name === 'category') {
      const cat = categories
        .find((category) => category.name === value);
      setSettings({
        ...settings,
        [name]: cat ? cat.id : 'All',
      });
    } else {
      setSettings({
        ...settings,
        [name]: value,
      });
    }
  }

  return (
    <Dialog open={openSettings}>
      <DialogTitle id="alert-dialog-title">
        <Grid
          container
          direction="row"
          alignItems="center"
          justifyContent="center"
        >
          <Typography variant="h2" sx={{ fontSize: 42 }}>
            Settings
          </Typography>
          <SettingsOutlined sx={{ fontSize: 50 }} />
        </Grid>
      </DialogTitle>
      <DialogContent>
        <Stack spacing={3} sx={{ mt: 1 }}>
          <SettingsSelect
            name="category"
            values={categories}
            handler={handleChange}
          />
          <SettingsSelect
            name="difficulty"
            values={['Easy', 'Medium', 'Hard']}
            handler={handleChange}
          />
          <SettingsSelect
            name="type"
            values={['Multiple', 'True/False']}
            handler={handleChange}
          />
        </Stack>
      </DialogContent>
      <DialogActions>
        <Button
          variant="contained"
          type="button"
          onClick={handleClick}
          size="medium"
          color="primary"
          sx={{
            '&:hover': { backgroundColor: theme.palette.secondary.main },
          }}
        >
          Save
        </Button>
      </DialogActions>
      <ErrorDialog openError={openError} setOpenError={setOpenError} />
      <BackdropComp open={openBackdrop} />
    </Dialog>
  );
}

const mapDispatchToProps = (dispatch) => ({
  sendSettings: (payload) => dispatch(setPlayerSettings(payload)),
});

export default connect(null, mapDispatchToProps)(Settings);

Settings.propTypes = {
  setOpenSettings: PropTypes.func.isRequired,
  sendSettings: PropTypes.func.isRequired,
  openSettings: PropTypes.bool.isRequired,
};
