using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

[CreateAssetMenu(fileName = "Game Menu Data", menuName = "Arcade/Game Menu Data")]
public class GameMenuData : ScriptableObject
{
    public string GameTitle;
    public string GameDesc;
    public bool[] supportedControls = new bool[3];
    public bool hasLeaderboard;
}