using Minesweeper.Controllers;
using Minesweeper.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Numerics;

namespace Minesweeper.Services
{
    public class MongoDAO : IConnect
    {
        static MongoClient dbClient = new MongoClient("mongodb://localhost:27017");
        public bool AddGameToDatabase(GameModel game)
        {
            var collection = dbClient.GetDatabase("Minesweeper").GetCollection<BsonDocument>("game_data");
            var counter = dbClient.GetDatabase("Minesweeper").GetCollection<BsonDocument>("counter");

            BsonDocument myGame = game.ToBsonDocument();
            var counterFilter = Builders<BsonDocument>.Filter.Eq("_id","item_id");
            int num = counter.Find(counterFilter).FirstOrDefault().GetElement("number").Value.ToInt32();
            myGame.SetElement(new BsonElement("_id", num));
            myGame.SetElement(new BsonElement("GameNumber", num));
            myGame.SetElement(new BsonElement("Id", game.Id));
            collection.InsertOne(myGame);

            var filter = Builders<BsonDocument>.Filter.And(Builders<BsonDocument>.Filter.Eq("_id","item_id"));
            var updates = Builders<BsonDocument>.Update.Inc("number", 1);
            BsonDocument document = counter.FindOneAndUpdate(filter, updates);
            if(document!=null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool DeleteGameByNumber(GameModel game)
        {
            var collection = dbClient.GetDatabase("Minesweeper").GetCollection<BsonDocument>("game_data");
            var filter = Builders<BsonDocument>.Filter.Eq("GameNumber", game.GameNumber);
            long success = collection.DeleteMany(filter).DeletedCount;
            if(success>0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public List<GameModel> FindAllGames()
        {
            List<GameModel> found = new List<GameModel>();
            var collection = dbClient.GetDatabase("Minesweeper").GetCollection<BsonDocument>("game_data");
            var filter = Builders<BsonDocument>.Filter.Empty;
            var gotten = collection.Find(filter).ToList();
            foreach (var item in gotten)
            {
                GameModel temp = new GameModel();
                temp.Id = item.GetElement("Id").Value.ToInt32();
                temp.GameNumber = item.GetElement("GameNumber").Value.ToInt32();
#pragma warning disable CS8601 // Possible null reference assignment.
                temp.Date = item.GetElement("Date").Value.ToString();
                temp.Time = item.GetElement("Time").Value.ToString();
                temp.GameData = item.GetElement("GameData").Value.ToString();
#pragma warning restore CS8601 // Possible null reference assignment.
                found.Add(temp);
            }
            return found;
        }

        public GameModel FindGameByNumber(GameModel game)
        {
            CellModel[,] madeGrid = new CellModel[10, 10];

            var collection = dbClient.GetDatabase("Minesweeper").GetCollection<BsonDocument>("game_data");
            var filter = Builders<BsonDocument>.Filter.Eq("GameNumber", game.GameNumber);
            var retrieved = collection.Find(filter).FirstOrDefault();
            GameModel gameModel = new GameModel();
            gameModel.Id = retrieved.GetElement("Id").Value.ToInt32();
            gameModel.GameNumber = retrieved.GetElement("GameNumber").Value.ToInt32();

#pragma warning disable CS8601 // Possible null reference assignment.
            gameModel.Time = retrieved.GetElement("Time").Value.ToString();
            gameModel.Date = retrieved.GetElement("Date").Value.ToString();
            gameModel.GameData = retrieved.GetElement("GameData").ToString();
#pragma warning restore CS8601 // Possible null reference assignment.
            return gameModel;
        }

        public List<GameModel> FindGamesById(PlayerModel player)
        {
            List<GameModel> found = new List<GameModel>();
            var collection = dbClient.GetDatabase("Minesweeper").GetCollection<BsonDocument>("game_data");
            var filter = Builders<BsonDocument>.Filter.Eq("Id",player.Id);
            var gotten = collection.Find(filter).ToList();
            foreach (var item in gotten)
            {
                GameModel temp = new GameModel();
                temp.Id = item.GetElement("Id").Value.ToInt32();
                temp.GameNumber = item.GetElement("GameNumber").Value.ToInt32();
#pragma warning disable CS8601 // Possible null reference assignment.
                temp.Date = item.GetElement("Date").Value.ToString();
                temp.Time = item.GetElement("Time").Value.ToString();
                temp.GameData = item.GetElement("GameData").Value.ToString();
#pragma warning restore CS8601 // Possible null reference assignment.
                found.Add(temp);
            }
            return found;
        }

        public BoardService GetGameByNumber(GameModel game)
        {
            var collection = dbClient.GetDatabase("Minesweeper").GetCollection<BsonDocument>("game_data");
            var filter = Builders<BsonDocument>.Filter.Eq("GameNumber",game.GameNumber);
            var retrieved = collection.Find(filter).FirstOrDefault();

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string gameData = retrieved.GetElement("GameData").Value.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string[] cells = gameData.Split('&');
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            BoardService temp = new BoardService(bool.Parse(cells[cells.Length - 5]), bool.Parse(cells[cells.Length - 4]), Int32.Parse(cells[cells.Length - 3]), Int32.Parse(cells[cells.Length - 2]), Int32.Parse(cells[cells.Length - 1]));
            CellModel[,] madeGrid = new CellModel[temp.Size, temp.Size];
            for (int i = 0; i < cells.Length - 5; i++)
            {
                string[] data = cells[i].Split(",");
                CellModel singleCell = new CellModel();
                for (int j = 0; j < data.Length; j++)
                {
                    switch (j)
                    {
                        case 0:
                            if (data[j] == "1")
                            {
                                singleCell.Visited = true;
                            }
                            else
                            {
                                singleCell.Visited = false;
                            }
                            break;

                        case 1:
                            if (data[j] == "1")
                            {
                                singleCell.Live = true;
                            }
                            else
                            {
                                singleCell.Live = false;
                            }
                            break;

                        case 2:
                            singleCell.LiveNeighbors = Int32.Parse(data[j]);
                            break;

                        case 3:
                            if (data[j] == "1")
                            {
                                singleCell.Flagged = true;
                            }
                            else
                            {
                                singleCell.Flagged = false;
                            }
                            break;

                        case 4:
                            singleCell.Row = Int32.Parse(data[j]);
                            break;

                        case 5:
                            singleCell.Column = Int32.Parse(data[j]);
                            break;
                    }
                }
                madeGrid[singleCell.Row, singleCell.Column] = singleCell;
            }
            temp.Grid = madeGrid;
            return temp;
        }
    }
}
