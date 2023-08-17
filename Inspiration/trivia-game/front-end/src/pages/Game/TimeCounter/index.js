import PropTypes from 'prop-types';
import { TimerTwoTone } from '@mui/icons-material';
import { CircularProgress, Typography } from '@mui/material';
import { Box } from '@mui/system';
import React from 'react';

export default class TimeCounter extends React.Component {
  render() {
    const { timer } = this.props;
    const ONE_PERCENT = 3.33;
    return (
      <Box
        sx={{
          position: 'relative', display: 'inline-flex',
        }}
      >
        <Box
          sx={{
            top: 0,
            left: 0,
            bottom: 25,
            right: 0,
            position: 'absolute',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
          }}
        >
          <TimerTwoTone sx={{ fontSize: 20 }} />
        </Box>
        <CircularProgress
          variant="determinate"
          value={timer * ONE_PERCENT}
          sx={{ color: '#006600' }}
          size={70}
          thickness={3}
        />
        <Box
          sx={{
            top: 22,
            left: 0,
            bottom: 0,
            right: 0,
            position: 'absolute',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
          }}
        >
          <Typography
            sx={{ fontWeight: 600, fontSize: 26 }}
            variant="h6"
            component="div"
            color="text.secondary"
          >
            {timer}
          </Typography>
        </Box>
      </Box>
    );
  }
}

TimeCounter.propTypes = {
  timer: PropTypes.number.isRequired,
};
