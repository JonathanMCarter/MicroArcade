using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS.Menu
{
    public class UIBSEnableDisable : MonoBehaviour
    {
        [SerializeField] private GameObject[] toEnableDisable;
        private UIButtonSwitch uibs;


        private void Awake()
        {
            uibs = GetComponent<UIButtonSwitch>();
        }


        public void EnableDisable()
        {
            for (int i = 0; i < toEnableDisable.Length; i++)
            {
                if (!i.Equals(uibs.pos))
                {
                    toEnableDisable[i].SetActive(false);
                }
                else
                {
                    toEnableDisable[i].SetActive(true);
                }
            }
        }


        public void RevertEffect()
        {
            for (int i = 0; i < toEnableDisable.Length; i++)
            {
                toEnableDisable[i].SetActive(true);
            }
        }
    }
}