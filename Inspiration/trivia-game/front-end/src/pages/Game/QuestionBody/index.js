import PropTypes from 'prop-types';
import React from 'react';
import { Button, Paper, Stack, Typography } from '@mui/material';

export class QuestionBody extends React.Component {
  randomAnswers(questions) {
    const { handler, classes, state: { questionNumber, clicked } } = this.props;
    const allQuestions = [
      questions[questionNumber].correct_answer,
      ...questions[questionNumber].incorrect_answers,
    ].sort();
    return (
      <Stack spacing={2}>
        {allQuestions.map((question, index) => {
          if (question
            === [questionNumber].correct_answer) {
            return (
              <Button
                name="correct-answer"
                variant="outlined"
                onClick={handler}
                data-testid="correct-answer"
                disabled={clicked}
                id="correct"
                className={classes.disabledGreen}
                key={index}
              >
                {decodeURIComponent(question)}
              </Button>
            );
          }
          return (
            <Button
              variant="outlined"
              type="button"
              disabled={clicked}
              id={index}
              className={classes.disabledRed}
              key={index}
              data-testid={`wrong-answer-${index}`}
              onClick={handler}
            >
              {decodeURIComponent(question)}
            </Button>
          );
        })}
      </Stack>
    );
  }

  render() {
    const { nextButton, questions, state: { questionNumber, clicked } } = this.props;
    return (
      <Paper
        variant="outlined"
        sx={{
          maxWidth: { xs: '80%' },
          my: { xs: 1, md: 2 },
          p: 2,
          pb: 5,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <Paper elevation={3} sx={{ textAlign: 'center', p: 1 }}>
          <Typography sx={{ fontSize: { xs: 18, md: 20 }, fontWeight: 'bold' }}>
            {decodeURIComponent(questions[questionNumber].category)}
          </Typography>
        </Paper>
        <Typography
          variant="h6"
          sx={{ py: 3, fontWeight: 'regular', textAlign: 'center' }}
          gutterBottom
        >
          {decodeURIComponent(questions[questionNumber].question)}
        </Typography>
        {this.randomAnswers(questions)}
        {clicked && (
          <Button
            sx={{ mt: 2 }}
            variant="contained"
            data-testid="btn-next"
            onClick={() => nextButton(questionNumber)}
          >
            Next
          </Button>
        )}
      </Paper>
    );
  }
}

QuestionBody.propTypes = {
  classes: PropTypes.shape({
    disabledGreen: PropTypes.string,
    disabledRed: PropTypes.string,
  }).isRequired,
  questions: PropTypes.arrayOf(PropTypes.shape({
    category: PropTypes.string,
    question: PropTypes.string,
    difficulty: PropTypes.string,
    correct_answer: PropTypes.string,
    incorrect_answers: PropTypes.arrayOf(PropTypes.string),
  })).isRequired,
  state: PropTypes.shape({
    clicked: PropTypes.bool.isRequired,
    questionNumber: PropTypes.number,
  }).isRequired,
  handler: PropTypes.func.isRequired,
  nextButton: PropTypes.func.isRequired,
};

export default (QuestionBody);
