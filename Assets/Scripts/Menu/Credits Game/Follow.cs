using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] internal Transform toFollow;
        [SerializeField] private Vector3 offset;


        private void Update()
        {
            transform.position = toFollow.position + offset;
        }
    }
}