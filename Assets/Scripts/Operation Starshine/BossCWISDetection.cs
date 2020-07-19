/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;

namespace CarterGames.Starshine
{
    public class BossCWISDetection : MonoBehaviour
    {
        [SerializeField] private BossScript bossScript;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<MissileScript>())
            {
                bossScript.CWISTargets.Add(collision.gameObject);
            }
        }
    }
}