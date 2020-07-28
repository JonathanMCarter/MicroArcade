using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class EditText : MonoBehaviour
    {
        private Text text;
        private CWIS_Controller controller;
        private ShipController ship;
        private GameManager gm;

        public enum textOption { Cw1, Cw2, ShipHealth, Score, Missiles }

        public textOption option;


        private void Start()
        {
            text = GetComponent<Text>();
            controller = FindObjectOfType<CWIS_Controller>();
            ship = FindObjectOfType<ShipController>();
            gm = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            switch (option)
            {
                case textOption.Cw1:
                    text.text = controller.GetAmmoCount_CW1() + "/" + controller.maxAmmo1;
                    break;
                case textOption.Cw2:
                    text.text = controller.GetAmmoCount_CW2() + "/" + controller.maxAmmo2;
                    break;
                case textOption.ShipHealth:
                    text.text = ship.shipHealth.ToString();
                    break;
                case textOption.Score:
                    text.text = gm.score.ToString();
                    break;
                case textOption.Missiles:
                    text.text = ship.shipMissiles.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}