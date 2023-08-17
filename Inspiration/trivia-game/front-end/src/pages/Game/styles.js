const styles = () => ({
  disabledGreen: {
    '&:disabled': {
      borderColor: 'black !important',
      border: '3px solid black !important',
      background: 'linear-gradient(45deg, #1df401 30%, #1df401 90%)',
      color: '#212121 !important',
    },
  },
  disabledRed: {
    '&:disabled': {
      background: 'linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)',
    },
  },
});

export default styles;
