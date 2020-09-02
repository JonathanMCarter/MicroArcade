using CarterGames.Arcade.UserInput;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class QuackingTimePanel : MonoBehaviour
    {
        [SerializeField] private GameObject rounds;
        [SerializeField] private GameObject hats;

        [SerializeField] private GameObject[] roundOptions;
        //[SerializeField] private GameObject[] 

        private Panel[] panels;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Start()
        {
            panels = new Panel[2];
            panels[0] = new Panel();
            panels[1] = new Panel();
            panels[0].BaseSetup();
            panels[1].BaseSetup();
            panels[0].items = roundOptions;
            panels[0].maxPos = panels[0].items.Length;
            ChangeColour();
        }


        private void Update()
        {
            if (MenuControls.Left() && !panels[0].isCoR)
            {
                StartCoroutine(panels[0].MoveAround(-1));
                ChangeColour();
            }

            if (MenuControls.Right() && !panels[0].isCoR)
            {
                StartCoroutine(panels[0].MoveAround(1));
                ChangeColour();
            }
        }


        private void ChangeColour()
        {
            for (int i = 0; i < panels[0].items.Length; i++)
            {
                if (panels[0].pos.Equals(i) && !panels[0].items[i].GetComponent<Image>().color.Equals(Color.black))
                {
                    panels[0].items[i].GetComponent<Image>().color = Color.black;
                }
                else if (!panels[0].pos.Equals(i) && panels[0].items[i].GetComponent<Image>().color.Equals(Color.black))
                {
                    panels[0].items[i].GetComponent<Image>().color = Color.grey;
                }
            }
        }
    }
}