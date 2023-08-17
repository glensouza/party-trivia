import {
  Avatar, Button, Container, Grid, List, ListItem, ListItemAvatar,
  ListItemText, Paper, Stack, Typography,
} from '@mui/material';
import PropTypes from 'prop-types';
import React from 'react';
import { EmojiEventsTwoTone } from '@mui/icons-material';

export default class Ranking extends React.Component {
  constructor() {
    super();

    this.state = {
      ranking: null,
    };
  }

  componentDidMount() {
    fetch('http://localhost:3001/api/leaderboard')
      .then((response) => response.json())
      .then((ranking) => this.setState({ ranking }));
  }

  render() {
    const { ranking } = this.state;
    const { history } = this.props;
    return (
      <Container component="main">
        <Stack
          sx={{
            alignItems: 'center',
            my: 4,
          }}
        >
          <Grid
            container
            direction="row"
            alignItems="center"
            justifyContent="center"
            sx={{ py: 2 }}
          >
            <Typography variant="h2" sx={{ fontSize: 40 }}>
              Leaderboard
            </Typography>
            <EmojiEventsTwoTone sx={{ fontSize: 60 }} />
          </Grid>
          <Paper
            variant="outlined"
            sx={{
              width: 250,
              px: 2,
              pb: 2,
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <List sx={{ mb: 2 }}>
              {ranking && ranking.map((user, index) => (
                <ListItem key={`${user.name + index}`} divider>
                  <Typography variant="h6" sx={{ fontSize: 18 }}>
                    {index + 1}
                  </Typography>
                  <ListItemAvatar sx={{ ml: 1 }}>
                    <Avatar
                      sx={{ width: 45, height: 45 }}
                      src={user.picture}
                    />
                  </ListItemAvatar>
                  <ListItemText
                    primary={user.name}
                    secondary={(
                      <Typography
                        sx={{ display: 'inline', fontWeight: 'bold' }}
                        component="span"
                        variant="body2"
                        color="text.primary"
                      >
                        {user.score}
                        {' '}
                        Points
                      </Typography>
                    )}
                  />
                </ListItem>
              ))}
            </List>
            <Button
              variant="contained"
              type="button"
              data-testid="btn-go-home"
              onClick={() => history.push('/trivia-game')}
            >
              Play Again
            </Button>
          </Paper>
        </Stack>
      </Container>
    );
  }
}

Ranking.propTypes = {
  history: PropTypes.shape({
    push: PropTypes.func,
  }).isRequired,
};
