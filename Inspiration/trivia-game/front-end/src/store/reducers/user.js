import { SET_PLAYER_INFO, SET_QUESTIONS, SET_SETTINGS } from '../actions';

const INITIAL_STATE = {
  player: {
    name: '',
    email: '',
  },
  questions: '',
  settings: {
    category: 'All',
    difficulty: 'All',
    type: 'All',
  },
};

const userReducer = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case SET_PLAYER_INFO:
      return { ...state, player: action.payload };
    case SET_QUESTIONS:
      return { ...state, questions: action.payload };
    case SET_SETTINGS:
      return { ...state, settings: action.payload };
    default:
      return state;
  }
};

export default userReducer;
