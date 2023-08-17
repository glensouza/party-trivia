interface Player {
  name: string;
  score: number;
  picture: string;
}

export class Leaderboard {
  ranking: Player[] = [];

  setLeaderboard = (player: Player) => {
    this.ranking.push(player);
    this.ranking.sort((a, b) => b.score - a.score);
    if (this.ranking.length > 10) {
      this.ranking.pop();
    }
  }

  getLeaderboard = () => {
    return this.ranking;
  }
}