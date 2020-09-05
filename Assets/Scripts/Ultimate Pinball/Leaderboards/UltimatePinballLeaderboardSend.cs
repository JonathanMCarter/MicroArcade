using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.Saving;
using CarterGames.Arcade.Leaderboard;
using CarterGames.Arcade.UserInput;
using UnityEngine.UI;

namespace CarterGames.UltimatePinball.Leaderboard
{
    public class UltimatePinballLeaderboardSend : InputSettings
    {
        public UltimatePinballLeaderboardData Player1_LB_Data, Player2_LB_Data;

        public OnScreenKeyboard Player1Keyboard;
        public OnScreenKeyboard Player2Keyboard;

        public bool Player1Ready, Player2Ready;

        bool HasDataSentP1, HasDataSentP2;
        public Image Player1ReadyImage, Player2ReadyImage;

        public Text Player1Name, Player2Name;

        UltimatePinballSessionData LoadedData;
        UltimatePinballData PinballGameData;

        public GameoverMenuCtrl MenuCtrl;


        void Start()
        {
            LoadedData = SaveManager.LoadLastUltimatePinballSession();
            PinballGameData = SaveManager.LoadUltimatePinball();

            Player1_LB_Data.PlayerScore = LoadedData.Player1Score;
            Player2_LB_Data.PlayerScore = LoadedData.Player2Score;
        }


        protected override void Update()
        {
            base.Update();

            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8)) { ConfirmP1(); }
                    if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B8)) { ConfirmP2(); }

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7)) { ReturnP1(); }
                    if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B7)) { ReturnP2(); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm)) { ConfirmP1(); }
                    if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.Confirm)) { ConfirmP2(); }

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Return)) { ReturnP1(); }
                    if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.Return)) { ReturnP2(); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { ConfirmP1(); }
                    if (KeyboardControls.ButtonPress(Players.P2, Buttons.B8)) { ConfirmP2(); }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { ReturnP1(); }
                    if (KeyboardControls.ButtonPress(Players.P2, Buttons.B7)) { ReturnP2(); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { ConfirmP1(); }
                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm)) { ConfirmP2(); }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { ReturnP1(); }
                    if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.Return)) { ReturnP2(); }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { ConfirmP2(); }
                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm)) { ConfirmP1(); }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { ReturnP2(); }
                    if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.Return)) { ReturnP1(); }

                    break;
                default:
                    break;
            }


            if ((Player1Ready) && (Player2Ready))
            {
                SendDataToBoard(Player1_LB_Data, 0);
                SendDataToBoard(Player2_LB_Data, 1);

                // change after sent
                GetComponentInParent<CanvasGroup>().alpha -= Time.deltaTime * 2;

                StartCoroutine(WaitAndEnableMenuCtrl());
            }
        }


        void ConfirmP1()
        {
            Player1_LB_Data.PlayerName = Player1Keyboard.GetFinalValue();
            Player1Keyboard.HideSelected();
            if (!Player1Ready) { Player1Ready = true; }
            Player1ReadyImage.enabled = true;
        }


        void ConfirmP2()
        {
            Player2_LB_Data.PlayerName = Player2Keyboard.GetFinalValue();
            Player2Keyboard.HideSelected();
            if (!Player2Ready) { Player2Ready = true; }
            Player2ReadyImage.enabled = true;
        }


        void ReturnP1()
        {
            if (Player1Ready) { Player1Ready = false; }
            Player1Keyboard.ShowSelected();
            Player1ReadyImage.enabled = false;
        }

        void ReturnP2()
        {
            if (Player2Ready) { Player2Ready = false; }
            Player2Keyboard.ShowSelected();
            Player2ReadyImage.enabled = false;
        }


        void SendDataToBoard(UltimatePinballLeaderboardData Data, int Player)
        {
            if ((!HasDataSentP1) && (Player == 0))
            {
                if (Player1_LB_Data.PlayerName == "")
                {
                    Player1_LB_Data.PlayerName = "Unknown";
                }
                else
                {
                    Player1_LB_Data.PlayerName = Player1_LB_Data.PlayerName.Substring(0, 1).ToString() + Player1_LB_Data.PlayerName.Substring(1).ToString().ToLower();
                    Player1Name.text = "Player 1 - " + Player1_LB_Data.PlayerName;
                }

                StartCoroutine(OnlineLeaderboardManager.Send_UltimatePinball_Data_Online(Data));
                HasDataSentP1 = true;
                Debug.Log("Data Send P1");
            }
            else if ((!HasDataSentP2) && (Player == 1))
            {
                if (Player2_LB_Data.PlayerName == "")
                {
                    Player2_LB_Data.PlayerName = "Unknown";
                }
                else
                {
                    Player2_LB_Data.PlayerName = Player2_LB_Data.PlayerName.Substring(0, 1).ToString() + Player2_LB_Data.PlayerName.Substring(1).ToString().ToLower();
                    Player2Name.text = "Player 2 - " + Player2_LB_Data.PlayerName;
                }

                StartCoroutine(OnlineLeaderboardManager.Send_UltimatePinball_Data_Online(Data));
                HasDataSentP2 = true;
                Debug.Log("Data Send P2");
            }
        }

        private IEnumerator WaitAndEnableMenuCtrl()
        {
            yield return new WaitForSeconds(.3f);
            MenuCtrl.ScriptEnabled = true;
        }
    }
}