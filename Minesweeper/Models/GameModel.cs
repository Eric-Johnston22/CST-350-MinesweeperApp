namespace Minesweeper.Models
{
    public class GameModel
    {
        public int GameNumber { get; set; }
        public int Id { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string GameData { get; set; }

        public GameModel(int id, string gameData)
        {
            Id = id;
            GameData = gameData;
            Time = DateTime.Now.ToString("hh:mm:ss");
            Date = DateTime.Now.ToString("yyyy-MM-dd");
        }
        public GameModel(int gameNumber,int id, string time,string date,string gameData)
        {
            GameNumber = gameNumber; 
            Id = id;
            GameData = gameData;
            Time = time;
            Date = date;
        }

        public GameModel()
        {
        }
    }
}
