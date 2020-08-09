using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

[CreateAssetMenu(fileName = "Game Menu Data", menuName = "Arcade/Game Menu Data")]
public class GameMenuData : ScriptableObject
{
    public int gameTitlePos;
    public string gameDesc;
    public Sprite gameBackground;
    public bool[] supportedControls = new bool[3];
    public bool hasLeaderboard;
    public bool hasPlayPanel;
    public bool hasInfoPanel;
    public int[] panels;
    public int infoPanelPos;
}