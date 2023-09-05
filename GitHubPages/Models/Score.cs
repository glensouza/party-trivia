namespace GitHubPages.Models
{
    public class Score
    {
        public string CategoryName { get; set; } = string.Empty;
        public int Correct { get; set; }
        public int Total { get; set; }
    }
}
