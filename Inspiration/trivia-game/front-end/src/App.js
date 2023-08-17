import React from 'react';
import { Switch, Route } from 'react-router-dom';
import Feedback from './pages/Feedback';
import game from './pages/Game';
import Login from './pages/Login';
import Ranking from './pages/Ranking';

export default function App() {
  return (
    <Switch>
      <Route exact path="/trivia-game" component={Login} />
      <Route path="/trivia-game/game" component={game} />
      <Route path="/trivia-game/feedback" component={Feedback} />
      <Route path="/trivia-game/ranking" component={Ranking} />
    </Switch>
  );
}
