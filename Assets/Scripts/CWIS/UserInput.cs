using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class UserInput : MonoBehaviour
    {

        public InputField field;
        public Button confirm;

        private void Start()
        {
            field = GetComponent<InputField>();
        }


        private void Update()
        {
            if (field.text.Length >= 3)
            {
                confirm.interactable = true;
            }
            else
            {
                confirm.interactable = false;
            }
        }


        public void SaveName()
        {
            PlayerPrefs.SetString("PlayerName", field.text);
            PlayerPrefs.Save();
        }
    }
}