using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class UIBSScrollRect : MonoBehaviour
    {
        [SerializeField] private ScrollRect rect;
        [SerializeField] private int[] positionsToChange;
        [SerializeField] private float[] rectPos;

        private UIButtonSwitch uIButtonSwitch;


        private void Start()
        {
            uIButtonSwitch = GetComponent<UIButtonSwitch>();
        }


        private void Update()
        {
            for (int i = 0; i < positionsToChange.Length; i++)
            {
                if (uIButtonSwitch.pos.Equals(positionsToChange[i]))
                {
                    rect.verticalNormalizedPosition = rectPos[i];
                }
            }    
        }
    }
}