using System;
using Minesweeper.Models;

namespace Minesweeper.Services
{
    public class SecurityService
    {
        PlayerDAO playerDAO = new PlayerDAO();
        public bool IsValid(PlayerModel user)
        {
            return playerDAO.FindUserByNameAndPassword(user);
        }
        public SecurityService()
        {
        }
    }
}

