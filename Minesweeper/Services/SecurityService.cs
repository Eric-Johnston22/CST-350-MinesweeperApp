using System;
using Minesweeper.Models;

namespace Minesweeper.Services
{
    public class SecurityService
    {
        PlayerDAO playerDAO = new PlayerDAO();
        public bool IsValid(PlayerModel player)
        {
            return playerDAO.FindUserByNameAndPassword(player);
        }
        public bool alreadyExists(PlayerModel player)
        {
            return playerDAO.FindUserByName(player);
        }
        public bool addPlayer(PlayerModel player)
        {
            return playerDAO.AddPlayerToDatabase(player);
        }
        public SecurityService()
        {
        }
    }
}

