using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starshine_SaveScript : MonoBehaviour
{
    public struct StarshineGameData
    {
        public int Player1ShipChoice;
        public int Player2ShipChoice;
    }

    public StarshineGameData Data;


    public void SaveShipSelectionOptions()
    {
        PlayerPrefs.SetInt("Starshine-Data-Player1ShipChoice", Data.Player1ShipChoice);
        PlayerPrefs.SetInt("Starshine-Data-Player2ShipChoice", Data.Player2ShipChoice);
    }


    public void LoadShipSelectionOptions()
    {
        Data.Player1ShipChoice = PlayerPrefs.GetInt("Starshine-Data-Player1ShipChoice");
        Data.Player2ShipChoice = PlayerPrefs.GetInt("Starshine-Data-Player2ShipChoice");
    }
}
