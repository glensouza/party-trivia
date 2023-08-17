import express from 'express'
import 'dotenv/config'
import { Leaderboard } from './Leaderboard';

const PORT = process.env.PORT || 3001;

const leaderboard = new Leaderboard();
const app = express();
app.use(express.json());
app.use((_req, res, next) => {
  res.set('Access-Control-Allow-Origin', '*');
  res.set('Access-Control-Allow-Methods', 'GET, POST');
  res.set('Access-Control-Allow-Headers', '*');
  next();
});

app.get('/api/leaderboard', (_req, res) => {
  res.send(leaderboard.getLeaderboard());
})

app.post('/api/leaderboard', (req, res) => {
  leaderboard.setLeaderboard(req.body);
  res.sendStatus(200)
})

app.listen(PORT, () => {
  console.log(`Server started on port ${PORT}`);
})