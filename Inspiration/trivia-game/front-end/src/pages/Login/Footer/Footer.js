import { Link, Typography } from '@mui/material';
import React from 'react';

export default function Footer() {
  return (
    <footer>
      <hr />
      <Typography
        sx={{ mt: 2, pb: 2 }}
        variant="body2"
        color="text.secondary"
        align="center"
      >
        {'© '}
        <Link color="text.primary" target="_blank" href="https://github.com/matheuspor/trivia-game">
          matheuspor
        </Link>
        {' 2021'}
      </Typography>
    </footer>
  );
}
