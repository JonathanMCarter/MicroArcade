/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/


namespace Arcade.Saving
{
    [System.Serializable]
    public class QuackingTimeData
    {
        public int player1HatSelection;
        public int player2HatSelection;

        public QuackingTimeData()
        {
            player1HatSelection = 0;
            player2HatSelection = 0;
        }

        public QuackingTimeData(int player1Hat, int player2Hat)
        {
            player1HatSelection = player1Hat;
            player2HatSelection = player2Hat;
        }
    }
}