/* eslint-disable max-lines-per-function */
/* eslint-disable react/jsx-max-depth */
import PropTypes from 'prop-types';
import {
  Avatar, Button, Container, Grid,
  Paper, Typography, Stack,
} from '@mui/material';
import React, { Component } from 'react';

export default class Feedback extends Component {
  render() {
    const userData = JSON.parse(localStorage.getItem('state'));
    const SMALL_SCREEN_FONT_SIZE = 'h6.fontSize';
    const THREE_QUESTIONS = 3;
    const { player: { picture, name, score, assertions } } = userData;
    const { history } = this.props;
    return (
      <Container component="main" maxWidth="md" sx={{ pb: 5 }}>
        <Stack
          sx={{
            my: 4,
            alignItems: 'center',
          }}
        >
          <Paper sx={{ my: 1, mx: 'auto', p: 2 }}>
            <Grid container spacing={2}>
              <Grid item>
                <Avatar
                  data-testid="header-profile-picture"
                  src={picture}
                  alt="avatar"
                />
              </Grid>
              <Grid item>
                <Typography
                  sx={{
                    fontWeight: 'bold',
                    fontSize: {
                      xs: 18,
                      sm: SMALL_SCREEN_FONT_SIZE,
                    },
                  }}
                  variant="h6"
                  color="text.secondary"
                >
                  {name}
                </Typography>
              </Grid>
            </Grid>
          </Paper>
          <Paper
            variant="outlined"
            sx={{
              p: 3,
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <Paper elevation={3} sx={{ textAlign: 'center', p: 1, px: 2 }}>
              <Typography
                variant="h5"
                color="text.primary"
              >
                {`Score: ${score} points`}
              </Typography>
            </Paper>
            <Typography
              sx={{
                pt: 2,
                pb: 1,
                fontSize: {
                  xs: 16,
                  sm: SMALL_SCREEN_FONT_SIZE,
                },
                fontWeight: 'regular',
              }}
              color="text.primary"
            >
              {`You scored ${assertions} out of 5 questions`}
            </Typography>
            {assertions >= THREE_QUESTIONS
              && (
                <Typography
                  sx={{
                    mb: 2,
                    fontStyle: 'oblique',
                    fontWeight: 'regular',
                    textDecoration: 'underline',
                    fontSize: {
                      xs: 18,
                      sm: SMALL_SCREEN_FONT_SIZE,
                    },
                  }}
                >
                  Good Job!
                </Typography>
              )}
            <Button
              variant="outlined"
              data-testid="btn-play-again"
              color="primary"
              onClick={() => history.push('/trivia-game')}
              sx={{ mb: 1 }}
            >
              Play Again
            </Button>
            <Button
              variant="contained"
              data-testid="btn-ranking"
              color="primary"
              onClick={() => history.push('/trivia-game/ranking')}
            >
              Ranking
            </Button>
          </Paper>
        </Stack>
      </Container>
    );
  }
}

Feedback.propTypes = {
  history: PropTypes.shape({
    push: PropTypes.func,
  }).isRequired,
};
