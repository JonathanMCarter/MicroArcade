/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;
using UnityEngine.UI;
using CarterGames.Arcade.Menu;
using CarterGames.Arcade.Leaderboard;
using CarterGames.Arcade.Saving;
using CarterGames.Arcade.UserInput;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Starshine
{
    public class VictoryController : MenuSystem
    {
        [SerializeField] private OnScreenKeyboard[] keyboards;
        [SerializeField] private CanvasGroup[] canvasGroups;
        [SerializeField] private GameObject leaderboardRow;
        [SerializeField] private OperationStarshineData loadedData;
        [SerializeField] private StarshineLeaderboardData leaderboardData;
        [SerializeField] private StarshineLeaderboardsSend sendScript;
        [SerializeField] private Color32 confirmCol;
        [SerializeField] private GameObject[] menuObjects;

        private bool changeToLeaderboard;
        private bool isLeaderboardScreen;

        private bool P1Ready, P2Ready;
        private Color32 P1PreviousCol, P2PreviousCol;

        public enum Screens { Input, Leaderboard };
        public Screens currentScreen;

        public GameObject[] Stats;
        public Animator animator;

        private new void Start()
        {
            sendScript = GetComponent<StarshineLeaderboardsSend>();
            MenuSystemStart();
            P1PreviousCol = keyboards[0].GetComponentsInChildren<Image>()[0].color;
            P2PreviousCol = keyboards[1].GetComponentsInChildren<Image>()[0].color;
            maxPos = menuObjects.Length - 1;
            am = FindObjectOfType<AudioManager>();
        }


        private new void Update()
        {
            if (currentScreen == Screens.Input)
            {
                if (P1Ready && P2Ready)
                {
                    if (Confirm())
                    {
                        loadedData = SaveManager.LoadOperationStarshine();
                        changeToLeaderboard = true;
                    }
                }

                // Keyboard backlights and confirming 
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        // Player 1
                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8) && (!P1Ready))
                        {
                            P1Ready = true;
                            keyboards[0].enabled = false;
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }
                        
                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7) && (P1Ready))
                        {
                            P1Ready = false;
                            keyboards[0].enabled = true;
                            keyboards[0].ShowSelected();
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = P1PreviousCol;
                        }

                        // Player 2
                        if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B8) && (!P2Ready))
                        {
                            P2Ready = true;
                            keyboards[1].enabled = false;
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }

                        if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B7) && (P2Ready))
                        {
                            P2Ready = false;
                            keyboards[1].enabled = true;
                            keyboards[1].ShowSelected();
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = P2PreviousCol;
                        }

                        break;
                    case SupportedControllers.GamePadBoth:

                        // Player 1
                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm) && (!P1Ready))
                        {
                            P1Ready = true;
                            keyboards[0].enabled = false;
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Return) && (P1Ready))
                        {
                            P1Ready = false;
                            keyboards[0].enabled = true;
                            keyboards[0].ShowSelected();
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = P1PreviousCol;
                        }

                        // Player 2
                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.Confirm) && (!P2Ready))
                        {
                            P2Ready = true;
                            keyboards[1].enabled = false;
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }

                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.Return) && (P2Ready))
                        {
                            P2Ready = false;
                            keyboards[1].enabled = true;
                            keyboards[1].ShowSelected();
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = P2PreviousCol;
                        }

                        break;
                    case SupportedControllers.KeyboardBoth:

                        // Player 1
                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8) && (!P1Ready))
                        {
                            P1Ready = true;
                            keyboards[0].enabled = false;
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7) && (P1Ready))
                        {
                            P1Ready = false;
                            keyboards[0].enabled = true;
                            keyboards[0].ShowSelected();
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = P1PreviousCol;
                        }

                        // Player 2
                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B8) && (!P2Ready))
                        {
                            P2Ready = true;
                            keyboards[1].enabled = false;
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }

                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B7) && (P2Ready))
                        {
                            P2Ready = false;
                            keyboards[1].enabled = true;
                            keyboards[1].ShowSelected();
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = P2PreviousCol;
                        }

                        break;
                    case SupportedControllers.KeyboardP1ControllerP2:

                        // Player 1
                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8) && (!P1Ready))
                        {
                            P1Ready = true;
                            keyboards[0].enabled = false;
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7) && (P1Ready))
                        {
                            P1Ready = false;
                            keyboards[0].enabled = true;
                            keyboards[0].ShowSelected();
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = P1PreviousCol;
                        }

                        // Player 2
                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.Confirm) && (!P2Ready))
                        {
                            P2Ready = true;
                            keyboards[1].enabled = false;
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }

                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.Return) && (P2Ready))
                        {
                            P2Ready = false;
                            keyboards[1].enabled = true;
                            keyboards[1].ShowSelected();
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = P2PreviousCol;
                        }

                        break;
                    case SupportedControllers.KeyboardP2ControllerP1:

                        // Player 1
                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm) && (!P1Ready))
                        {
                            P1Ready = true;
                            keyboards[0].enabled = false;
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Return) && (P1Ready))
                        {
                            P1Ready = false;
                            keyboards[0].enabled = true;
                            keyboards[0].ShowSelected();
                            keyboards[0].GetComponentsInChildren<Image>()[0].color = P1PreviousCol;
                        }

                        // Player 2
                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B8) && (!P2Ready))
                        {
                            P2Ready = true;
                            keyboards[1].enabled = false;
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = confirmCol;
                        }

                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B7) && (P2Ready))
                        {
                            P2Ready = false;
                            keyboards[1].enabled = true;
                            keyboards[1].ShowSelected();
                            keyboards[1].GetComponentsInChildren<Image>()[0].color = P2PreviousCol;
                        }

                        break;
                    default:
                        break;
                }

                // if move screen - do canvas grouip fades
                if ((changeToLeaderboard) && (canvasGroups[0].alpha != 0) && (canvasGroups[1].alpha != 1))
                {
                    canvasGroups[0].alpha -= 2 * Time.deltaTime;
                    canvasGroups[1].alpha += 2 * Time.deltaTime;

                    if (canvasGroups[0].alpha == 0 && canvasGroups[1].alpha == 1)
                    {
                        if (keyboards[0].GetFinalValue() != null)
                        {
                            leaderboardData.Player1Name = keyboards[0].GetFinalValue();
                        }
                        else
                        {
                            leaderboardData.Player1Name = "Unknown";
                        }

                        if (keyboards[1].GetFinalValue() != null)
                        {
                            leaderboardData.Player2Name = keyboards[1].GetFinalValue();
                        }
                        else
                        {
                            leaderboardData.Player2Name = "Unknown";
                        }

                        leaderboardData.Player1Score = loadedData.Player1Score;
                        leaderboardData.Player2Score = loadedData.Player2Score;
                        leaderboardData.Player1ShipName = ConvertToShipName(loadedData.LastPlayer1ShipSelection);
                        leaderboardData.Player2ShipName = ConvertToShipName(loadedData.LastPlayer2ShipSelection);


                        isLeaderboardScreen = true;
                        currentScreen = Screens.Leaderboard;
                        sendScript.SendDataToBoard(leaderboardData);
                        UpdateStatsScreen();
                    }
                }
            }
            else
            {
                MoveUD();
                UpdateMenuOptions();
                UpdateStatsScreen();

                if (isLeaderboardScreen)
                {
                    UpdateLeaderboardRow();
                    isLeaderboardScreen = false;
                }

                if (Confirm())
                {
                    switch (pos)
                    {
                        case 0:
                            animator.SetBool("ChangeScene", true);
                            ChangeScene("Operation-Starshine-Level", 1.1f);
                            break;
                        case 1:
                            animator.SetBool("ChangeScene", true);
                            ChangeScene("Operation-Starshine-Menu", 1.1f);
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        private void UpdateLeaderboardRow()
        {
            leaderboardRow.GetComponentsInChildren<Text>()[0].text = leaderboardData.Player1Name + " & " + leaderboardData.Player2Name;
            leaderboardRow.GetComponentsInChildren<Text>()[1].text = leaderboardData.Player1ShipName + " & " + leaderboardData.Player2ShipName;
            leaderboardRow.GetComponentsInChildren<Text>()[2].text = (leaderboardData.Player1Score + leaderboardData.Player2Score).ToString();
        }


        private string ConvertToShipName(int Value)
        {
            switch (Value)
            {
                case 0:
                    return "Aether Alpha";
                case 1:
                    return "Aether Attack";
                case 2:
                    return "Scarlet Interceptor";
                case 3:
                    return "Scarlet One";
                case 4:
                    return "Unity Gunship";
                default:
                    return "Unknown";
            }
        }


        private void UpdateMenuOptions()
        {
            for (int i = 0; i < menuObjects.Length; i++)
            {
                if (pos == i && menuObjects[i].GetComponent<Text>().color != Color.yellow)
                {
                    menuObjects[i].GetComponent<Text>().color = Color.yellow;
                }
                else if (pos != i && menuObjects[i].GetComponent<Text>().color == Color.yellow)
                {
                    menuObjects[i].GetComponent<Text>().color = Color.white;
                }
            }
        }



        private void UpdateStatsScreen()
        {
            for (int i = 0; i < Stats.Length; i++)
            {
                if (i == 0)
                {
                    Stats[0].GetComponentsInChildren<Text>()[0].text = "Health Gained: " + loadedData.player1Stats.healthGained.ToString();
                    Stats[0].GetComponentsInChildren<Text>()[1].text = "Health Lost: " + loadedData.player1Stats.healthLost.ToString();
                    Stats[0].GetComponentsInChildren<Text>()[2].text = "Shield Gained: " + loadedData.player1Stats.shieldGained.ToString();
                    Stats[0].GetComponentsInChildren<Text>()[3].text = "Shield Lost: " + loadedData.player1Stats.shieldLost.ToString();
                    Stats[0].GetComponentsInChildren<Text>()[4].text = "Time Shields Switched: " + loadedData.player1Stats.shieldSwitches.ToString();
                    Stats[0].GetComponentsInChildren<Text>()[5].text = "Main Weapon Shots Fired: " + loadedData.player1Stats.mainShotsFired.ToString();
                    Stats[0].GetComponentsInChildren<Text>()[6].text = "Alt Weapon Shots Fired: " + loadedData.player1Stats.altShotsFired.ToString();
                    Stats[0].GetComponentsInChildren<Text>()[7].text = "Special Ability Use Count: " + loadedData.player1Stats.specialShotsFired.ToString();
                    Stats[0].GetComponentsInChildren<Text>()[8].text = "Damage Delt: " + loadedData.player1Stats.damageDelt.ToString();
                    Stats[0].GetComponentsInChildren<Text>()[9].text = "Enemies Killed: " + loadedData.player1Stats.enemiesKilled.ToString();
                }
                else
                {
                    Stats[1].GetComponentsInChildren<Text>()[0].text = "Health Gained: " + loadedData.player2Stats.healthGained.ToString();
                    Stats[1].GetComponentsInChildren<Text>()[1].text = "Health Lost: " + loadedData.player2Stats.healthLost.ToString();
                    Stats[1].GetComponentsInChildren<Text>()[2].text = "Shield Gained: " + loadedData.player2Stats.shieldGained.ToString();
                    Stats[1].GetComponentsInChildren<Text>()[3].text = "Shield Lost: " + loadedData.player2Stats.shieldLost.ToString();
                    Stats[1].GetComponentsInChildren<Text>()[4].text = "Time Shields Switched: " + loadedData.player2Stats.shieldSwitches.ToString();
                    Stats[1].GetComponentsInChildren<Text>()[5].text = "Main Weapon Shots Fired: " + loadedData.player2Stats.mainShotsFired.ToString();
                    Stats[1].GetComponentsInChildren<Text>()[6].text = "Alt Weapon Shots Fired: " + loadedData.player2Stats.altShotsFired.ToString();
                    Stats[1].GetComponentsInChildren<Text>()[7].text = "Special Ability Use Count: " + loadedData.player2Stats.specialShotsFired.ToString();
                    Stats[1].GetComponentsInChildren<Text>()[8].text = "Damage Delt: " + loadedData.player2Stats.damageDelt.ToString();
                    Stats[1].GetComponentsInChildren<Text>()[9].text = "Enemies Killed: " + loadedData.player2Stats.enemiesKilled.ToString();
                }
            }
        }
    }
}