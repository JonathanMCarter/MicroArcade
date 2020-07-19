/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using CarterGames.Arcade.Menu;
using UnityEngine;

namespace CarterGames.Starshine
{
    public class StarshineMusicPlayer : MusicPlayerScript
    {
        [SerializeField] private AudioSource[] stageTracks;
        private int trackNumber;

        private void Start()
        {
            trackNumber = 1;
        }

        public void PlayNextTrack()
        {
            trackNumber += 1;
        }


        private void Update()
        {
            if (trackNumber == 2 && stageTracks[1].volume != 1)
            {
                stageTracks[1].volume += Time.deltaTime;
            }

            if (trackNumber == 3 && stageTracks[2].volume != 1)
            {
                stageTracks[2].volume += Time.deltaTime;
            }
        }
    }
}