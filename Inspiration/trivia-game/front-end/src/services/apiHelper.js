import md5 from 'crypto-js/md5';

const GRAVATAR_URL = 'https://www.gravatar.com/avatar/';

export const fetchPlayerToken = async () => {
  const request = await fetch('https://opentdb.com/api_token.php?command=request')
    .then((response) => response.json())
    .then((token) => localStorage.setItem('token', token.token));
  return request;
};

export const fetchSetPlayerImg = async (user) => {
  const hash = md5(user.email).toString();
  const avatar = await fetch(`${GRAVATAR_URL}${hash}`).then((url) => url.url);

  const newUser = { ...user, avatar };
  return newUser;
};

export const fetchQuestions = async (token, settings) => {
  const settingsKeys = Object.keys(settings);
  let apiCall = 'https://opentdb.com/api.php?amount=5&encode=url3986';
  settingsKeys.forEach((setting, index) => {
    if (settings[setting] !== 'All') {
      apiCall += `&${settingsKeys[index]}=${settings[setting]}`;
    }
  });
  const request = await fetch(`${apiCall}&${token}`)
    .then((response) => response.json())
    .then((questions) => questions.results);
  return request;
};

export const fetchCategories = () => fetch('https://opentdb.com/api_category.php')
  .then((response) => response.json())
  .then((categories) => localStorage.setItem('categories',
    JSON.stringify(categories.trivia_categories)));
