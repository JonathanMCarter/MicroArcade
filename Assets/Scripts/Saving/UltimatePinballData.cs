namespace CarterGames.Arcade.Saving
{
    [System.Serializable]
    public class UltimatePinballData
    {
        public int LastGameTypeSelected;
        public int LastGameTypeIncrement;
        public int LastGameTypeAmountSelected;
    }


    [System.Serializable]
    public class UltimatePinballSessionData
    {
        public string Player1Name;
        public int Player1Score;
        public int Player1Health = 0;
        public string Player2Name;
        public int Player2Score;
        public int Player2Health = 0;


        public UltimatePinballSessionData(UltimatePinball.GameManager.BG_PlayerStats Player1Stats, UltimatePinball.GameManager.BG_PlayerStats Player2Stats)
        {
            Player1Name = Player1Stats.Name;
            Player1Score = Player1Stats.Score;
            Player1Health = Player1Stats.Health;
            Player2Name = Player2Stats.Name;
            Player2Score = Player2Stats.Score;
            Player2Health = Player2Stats.Health;
        }

        public UltimatePinballSessionData()
        {
            // default
        }
    }


}