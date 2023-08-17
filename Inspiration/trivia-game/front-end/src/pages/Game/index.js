import {
  Container, Typography, Avatar, Stack,
} from '@mui/material';
import PropTypes from 'prop-types';
import React from 'react';
import { withStyles } from '@material-ui/styles';
import { connect } from 'react-redux';
import TimeCounter from './TimeCounter';
import { updateLocalStorage, updateRanking } from '../../services/helperServices';
import styles from './styles';
import { QuestionBody } from './QuestionBody';

const ONE_SECOND = 1000;

export class Game extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      clicked: false,
      timer: 30,
      questionNumber: 0,
      playerScore: 0,
      playerAssertions: 0,
    };

    this.handleClick = this.handleClick.bind(this);
    this.setTimer = this.setTimer.bind(this);
    this.nextButton = this.nextButton.bind(this);
  }

  componentDidMount() {
    this.setTimer();
  }

  async componentWillUnmount() {
    updateRanking();
  }

  setTimer() {
    this.setState({ timer: 30 });
    const countdown = setInterval(() => {
      this.setState(
        (prevState) => ({
          timer: prevState.timer - 1,
        }),
        () => {
          const { timer, clicked } = this.state;
          if (timer <= 0 || clicked) {
            clearInterval(countdown);
            this.setState({ clicked: true });
          }
        },
      );
    }, ONE_SECOND);
  }

  handleClick({ target }) {
    const { timer, questionNumber } = this.state;
    const { questions } = this.props;
    const { difficulty } = questions[questionNumber];
    const baseScore = 10;

    const obj = {
      easy: 1,
      medium: 2,
      hard: 3,
    };
    this.setState({ clicked: true });

    if (target.name === 'correct-answer') {
      this.setState((prevstate) => ({
        playerScore:
          prevstate.playerScore + baseScore + timer * obj[difficulty],
        playerAssertions: prevstate.playerAssertions + 1,
      }));
    }
  }

  nextButton(questionNumber) {
    const { history, questions, player } = this.props;
    if (questionNumber < questions.length - 1) {
      this.setState(
        (prevState) => ({
          questionNumber: prevState.questionNumber + 1,
          clicked: false,
        }),
        () => {
          this.setTimer();
        },
      );
    } else {
      updateLocalStorage(this.state, player);
      history.push('/trivia-game/feedback');
    }
  }

  render() {
    const { player, questions, classes } = this.props;
    const { timer, questionNumber, playerScore } = this.state;

    return (
      <Container component="main" maxWidth="md" sx={{ pb: 5 }}>
        <Stack
          sx={{
            my: 4,
            alignItems: 'center',
          }}
        >
          <Avatar
            sx={{ height: 60, width: 60 }}
            src={player.avatar}
            alt="Player Image Avatar"
          />
          <Typography variant="h6">
            {player.name}
          </Typography>
          <Typography
            variant="h6"
            sx={{ fontWeight: 'regular' }}
          >
            {`${playerScore} Points`}
          </Typography>
          <TimeCounter timer={timer} />
          <QuestionBody
            state={this.state}
            questions={questions}
            classes={classes}
            handler={this.handleClick}
            nextButton={this.nextButton}
          />
        </Stack>
        <hr
          style={{
            color: '#000000',
            backgroundColor: '#000000',
            width: '80%',
            borderColor: '#000000',
          }}
        />
        <Typography
          variant="h6"
          component="div"
          sx={{
            pt: 2, pb: 4, fontWeight: 'regular', textAlign: 'center',
          }}
          gutterBottom
        >
          {`${questionNumber + 1}/5`}
        </Typography>
      </Container>
    );
  }
}

Game.propTypes = {
  classes: PropTypes.shape({
    disabledGreen: PropTypes.string,
    disabledRed: PropTypes.string,
  }).isRequired,
  history: PropTypes.shape({
    push: PropTypes.func,
  }).isRequired,
  player: PropTypes.shape({
    avatar: PropTypes.string,
    email: PropTypes.string,
    name: PropTypes.string,
  }).isRequired,
  questions: PropTypes.arrayOf(PropTypes.shape({
    category: PropTypes.string,
    question: PropTypes.string,
    difficulty: PropTypes.string,
    correct_answer: PropTypes.string,
    incorrect_answers: PropTypes.arrayOf(PropTypes.string),
  })).isRequired,
};

const mapStateToProps = ({ user: { player, questions } }) => ({
  player,
  questions,
});

export default connect(mapStateToProps)(withStyles(styles)(Game));
