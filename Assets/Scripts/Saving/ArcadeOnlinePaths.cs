/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/


namespace CarterGames.Arcade.Saving
{
    [System.Serializable]
    public class ArcadeOnlinePaths
    {
        public string onlineLeaderboardsBasePath = "https://www.carter.games/ma/";
        public string defaultLeaderboardsPath = "https://www.carter.games/ma/";

        public ArcadeOnlinePaths(string basePath)
        {
            onlineLeaderboardsBasePath = basePath;
        }

        public ArcadeOnlinePaths()
        {
            onlineLeaderboardsBasePath = defaultLeaderboardsPath;
        }
    }
}