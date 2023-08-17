import PropTypes from 'prop-types';
import React from 'react';
import { connect } from 'react-redux';
import {
  Stack, Button,
  TextField, Link, Container,
} from '@mui/material';
import { withStyles } from '@material-ui/styles';
import { setPlayerInfo, setPlayerQuestions } from '../../store/actions';
import {
  fetchCategories, fetchPlayerToken,
  fetchQuestions, fetchSetPlayerImg,
} from '../../services/apiHelper';
import Footer from './Footer/Footer';
import BackdropComp from '../../components/Backdrop';
import logo from '../../assets/trivia.png';
import styles from './styles';
import Settings from '../Settings';
import '../../App.css';

export class LoginPage extends React.Component {
  constructor(props) {
    super(props);
    const { player: { name, email } } = this.props;
    this.state = {
      user: {
        name,
        email,
      },
      openBackdrop: true,
      openSettings: false,
    };
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);

    fetchPlayerToken()
      .then(() => {
        fetchCategories();
        this.setState({ openBackdrop: false });
      });
  }

  handleChange({ target }) {
    const { name, value } = target;
    this.setState((prevValue) => ({
      user: {
        ...prevValue.user,
        [name]: value,
      },
    }));
  }

  handleSubmit(event) {
    const {
      sendQuestions, sendPlayer, settings, history,
    } = this.props;
    const { user } = this.state;
    const token = localStorage.getItem('token');
    event.preventDefault();
    this.setState({ openBackdrop: true });

    fetchSetPlayerImg(user)
      .then((player) => {
        sendPlayer(player);
        fetchQuestions(token, settings)
          .then((questions) => {
            sendQuestions(questions);
            history.push('/trivia-game/game');
          });
      });
  }

  render() {
    const { classes } = this.props;
    const { user, openBackdrop, openSettings } = this.state;
    return (
      <Container component="main" maxWidth="xs">
        <>
          <BackdropComp open={openBackdrop} />
          <Settings
            openSettings={openSettings}
            setOpenSettings={(value) => this.setState({ openSettings: value })}
          />
          {openBackdrop || (
            <Stack
              component="form"
              onSubmit={this.handleSubmit}
              spacing={3}
              sx={{ my: 4 }}
            >
              <img src={logo} className={classes.logo} alt="logo" />
              <TextField
                name="name"
                type="text"
                label="Name:"
                value={user.name}
                autoComplete="name"
                onChange={this.handleChange}
                required
              />
              <TextField
                name="email"
                type="email"
                label="Gravatar email:"
                value={user.email}
                autoComplete="email"
                onChange={this.handleChange}
                helperText={(
                  <Link href="https://en.gravatar.com/" target="_blank" underline="always">
                    Gravatar
                  </Link>
                )}
              />
              <Button
                type="submit"
                fullWidth
                disabled={!user.name}
                variant="contained"
                color="primary"
              >
                Play
              </Button>
              <Button
                type="button"
                fullWidth
                variant="contained"
                color="secondary"
                onClick={() => this.setState({ openSettings: true })}
              >
                Settings
              </Button>
              <Footer />
            </Stack>
          )}
        </>
      </Container>
    );
  }
}

const mapStateToProps = ({ user: { player, settings } }) => ({
  player,
  settings,
});

const mapDispatchToProps = (dispatch) => ({
  sendQuestions: (payload) => dispatch(setPlayerQuestions(payload)),
  sendPlayer: (payload) => dispatch(setPlayerInfo(payload)),
});

export default connect(mapStateToProps, mapDispatchToProps)(
  withStyles(styles)(LoginPage),
);

LoginPage.propTypes = {
  classes: PropTypes.shape({
    logo: PropTypes.string,
  }).isRequired,
  history: PropTypes.shape({
    push: PropTypes.func,
  }).isRequired,
  player: PropTypes.shape({
    email: PropTypes.string,
    name: PropTypes.string,
  }).isRequired,
  sendPlayer: PropTypes.func.isRequired,
  sendQuestions: PropTypes.func.isRequired,
  settings: PropTypes.shape({
    category: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
    difficulty: PropTypes.string,
    type: PropTypes.string,
  }).isRequired,
};
