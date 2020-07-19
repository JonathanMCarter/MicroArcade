using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing.Saving
{
    [System.Serializable]
    public class CrushingData
    {
        //User Data - Stats for the last round & their PB
        public int[] starsCollected;        // Collectables - element 0 (Gold Stars), element 1 (Silver Stars)
        public int numberOfDodges;          // The number of crushers dodges in the last round
        public int[] bestStarsCollected;        // Collectables - element 0 (Gold Stars), element 1 (Silver Stars)
        public int bestNumberOfDodges;          // The number of crushers dodges in the last round
        public float lastRoundTime;         // The amount of time the last round lasted
        public float longestRoundTime;      // The best time the player has gotten


        // Settings Data
        public float[] playerColour;        // The Colour chosen by the player (stored in 4 it values, cause I wanted to do it this way xD).
        public SerializeVector3 playerPipPosition;
        public float[] crusherColour;       // The Colour chosen by the player (stored in 4 it values, cause I wanted to do it this way xD). 
        public SerializeVector3 crusherPipPosition;
        public SerializeSprite playerShapeSprite; // The Player shape that has been selected in the settings (uses a custom class to work, so won't work in other projects that do not have utilities)
        public short playerShapeChoice;
        public float musicVolume;           // The music volume for the game
        public float sfxVolume;             // The sfx volume for the game


        // User Statistics
        public int numberOfDodgesLifetime;          // Stores the number of crusher dodges
        public int numberOfRoundsPlayedLifetime;    // Stores the number of rounds played, only ones the player has completed...
        public int[] starsCollectedLifetime;        // Stores the amount og starts that have been collected over the dats's lifetime


        public CrushingData()
        {
            // player round stats
            starsCollected = new int[2];
            numberOfDodges = 0;
            lastRoundTime = 0;

            // player pb stats
            bestStarsCollected = new int[2] { 0, 0 };
            bestNumberOfDodges = 0;
            longestRoundTime = 0;

            // Player settings 
            playerColour = new float[4] { 1f, 1f, 1f, 1f };
            playerPipPosition = new SerializeVector3(0f, 0f, 0f);
            crusherColour = new float[4] { 1f, .721f, .156f, 1f };
            crusherPipPosition = new SerializeVector3(85.5f, 126.5f, 0f);
            playerShapeSprite = new SerializeSprite();
            playerShapeChoice = 1;
            musicVolume = -6;
            sfxVolume = 0;

            // lifetime stats defaults
            starsCollectedLifetime = new int[2] { 0, 0 };
            numberOfDodgesLifetime = 0;
            numberOfRoundsPlayedLifetime = 0;
        }
    }
}