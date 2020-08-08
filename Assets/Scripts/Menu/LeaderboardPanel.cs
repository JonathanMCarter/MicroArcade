using CarterGames.Arcade.Menu;
using CarterGames.Arcade.UserInput;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] private ScrollRect rect;
    [SerializeField] private GameObject parent;

    private Panel panel;
    private ArcadeGameMenuCtrl aGMC;


    private void Start()
    {
        aGMC = FindObjectOfType<ArcadeGameMenuCtrl>();
        rect = GetComponent<ScrollRect>();
        panel = new Panel();
        panel.BaseSetup();
    }


    private void Update()
    {
        if (MenuControls.Up())
        {
            rect.verticalNormalizedPosition += 2f * Time.deltaTime;
        }

        if (MenuControls.Down())
        {
            rect.verticalNormalizedPosition -= 2f * Time.deltaTime;
        }


        if (MenuControls.Return())
        {
            aGMC.leaderboardPanelActive = false;
            parent.SetActive(false);
        }
    }
}