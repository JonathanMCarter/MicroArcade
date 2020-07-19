using CarterGames.Crushing.Saving;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/


public class StatsUI : MonoBehaviour
{
    [SerializeField] private CrushingData saveData;
    [SerializeField] private Text[] bestRoundElements;
    [SerializeField] private Text[] lifetimeElements;

    private const string defaultTimerValue = "00:00:000";


    private void Start()
    {
        saveData = SaveManager.LoadGame();

        if (saveData.longestRoundTime > 0)
        {
            bestRoundElements[0].text = FormatTimer(saveData.longestRoundTime);
        }
        else
        {
            bestRoundElements[0].text = defaultTimerValue;
        }

        bestRoundElements[1].text = saveData.bestStarsCollected[1].ToString();
        bestRoundElements[2].text = saveData.bestNumberOfDodges.ToString();

        lifetimeElements[0].text = saveData.numberOfRoundsPlayedLifetime.ToString();
        lifetimeElements[2].text = saveData.starsCollectedLifetime[1].ToString();
        lifetimeElements[1].text = saveData.numberOfDodgesLifetime.ToString();
    }


    /// <summary>
    /// Converts a float value to a string value formatted in the following 00:00:000 format
    /// </summary>
    /// <param name="input">float value to format</param>
    /// <returns>returns the float formatted to 00:00:000</returns>
    private string FormatTimer(float input)
    {
        string _mins;
        string _seconds;

        _mins = Mathf.FloorToInt(input / 60).ToString("00");
        _seconds = Mathf.FloorToInt((input % 60)).ToString("00");

        int _pos = input.ToString().IndexOf('.');

        return _mins + ":" + _seconds + ":" + input.ToString().Substring(_pos + 1, 3);
    }
}
