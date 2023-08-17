export const updateLocalStorage = ({ playerAssertions, playerScore }, player) => {
  const localStorageObj = {
    player: {
      name: player.name,
      assertions: playerAssertions,
      score: playerScore,
      gravatarEmail: player.email,
      picture: player.avatar,
    },
  };
  localStorage.setItem('state', JSON.stringify(localStorageObj));
};

export const updateRanking = () => {
  const playerInfo = JSON.parse(localStorage.getItem('state'));

  const player = {
    name: playerInfo.player.name,
    score: playerInfo.player.score,
    picture: playerInfo.player.picture,
  };

  fetch('http://localhost:3001/api/leaderboard', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(player),
  });

  // const getRanking = JSON.parse(localStorage.getItem('ranking'));
  // if (getRanking) {
  //   localStorage.setItem('ranking', JSON.stringify([...getRanking, player]));
  // } else localStorage.setItem('ranking', JSON.stringify([player]));
};
