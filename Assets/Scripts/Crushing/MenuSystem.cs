using System.Collections.Generic;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class MenuSystem : IMenuSystem
    {
        public List<GameObject> menuOptions { get; set; }
        public int position { get; set; }


        public void ChangePosition(int newPosition)
        {
            position = newPosition;

            for (int i = 0; i < menuOptions.Count; i++)
            {
                if ((position == i) && (!menuOptions[i].activeSelf))
                {
                    menuOptions[i].SetActive(true);
                }
                else if ((position != i) && (menuOptions[i].activeSelf))
                {
                    menuOptions[i].SetActive(false);
                }
            }
        }
    }
}