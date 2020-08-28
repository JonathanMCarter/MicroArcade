using CarterGames.Arcade.Leaderboard;
using CarterGames.Arcade.Saving;
using CarterGames.Arcade.UserInput;
using CarterGames.Assets.AudioManager;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class ArcadeGameMenuCtrl : MonoBehaviour
    {
        [Header("Menu Data")]
        [SerializeField] private GameMenuData[] data;
        [SerializeField] private GameMenuData activeData;

        [Header("Menu Fields")]
        [SerializeField] private GameObject gameTitle;
        [SerializeField] private Text gameDesc;
        [SerializeField] private Image gameBackground;
        [SerializeField] private Image[] supportedControllers;
        [SerializeField] private GameObject[] topThreeScores;

        [Header("Menu Button Controls")]
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private Color32[] activeButtonColours;
        [SerializeField] private Color32[] inactiveButtonColours;

        private Color32 activeCol = new Color32(90, 200, 130, 255);
        private Color32 inactiveCol = new Color32(200, 95, 90, 255);

        [SerializeField] private bool isCoR;
        [SerializeField] private int lastPos;
        [SerializeField] private int pos;
        [SerializeField] private int maxPos;
        [SerializeField] private bool inputReady;


        [Header("Panel Active Status")]
        [SerializeField] internal bool playPanelActive;
        [SerializeField] internal bool infoPanelActive;
        [SerializeField] internal bool leaderboardPanelActive;


        [Header("Tutorial Options")]
        [SerializeField] private GameObject[] tutorialPages;


        [Header("Leaderboard Options")]
        [SerializeField] private GameObject[] leaderboardPanels;
        [SerializeField] private bool hasPopulated;
        private bool useOnline;


        [Header("Transitions")]
        [SerializeField] internal Animator transitions;


        [Header("(CG) - Audio Manager")]
        [SerializeField] private AudioManager am;


        private GameObject panelHolder;



        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Start()
        {
            panelHolder = GameObject.FindGameObjectWithTag("Respawn");


            // Sets the game data for the menu to the one selected
            activeData = data[PlayerPrefs.GetInt("GameSel")];

            // sets the display info up
            gameTitle.transform.GetChild(activeData.gameTitlePos).gameObject.SetActive(true);
            gameDesc.text = activeData.gameDesc;

            gameBackground.sprite = activeData.gameBackground;

            SupportedControlsSetup();
            LeaderboardSetup();
            MenuButtonsSetup();

            // Menu System Start
            maxPos = buttons.Length - 1;
            lastPos = pos;
            isCoR = false;
            inputReady = true;

            if (GetComponent<AudioManager>())
            {
                am = GetComponent<AudioManager>();
            }

            buttons[pos].GetComponent<Image>().color = inactiveButtonColours[pos];
        }



        private void Update()
        {
            MenuMovement();

            if (!playPanelActive)
            {
                if (MenuControls.Confirm())
                {
                    ConfirmOption();
                }

                if (MenuControls.Return())
                {
                    ReturnOption();
                }
            }


            if (infoPanelActive && !tutorialPages[activeData.infoPanelPos].activeInHierarchy)
            {
                tutorialPages[activeData.infoPanelPos].SetActive(true);
            }
        }

        /// <summary>
        /// Defines how player 1 can move around the menu with no panels open
        /// </summary>
        private void MenuMovement()
        {
            if (!playPanelActive && !infoPanelActive && !leaderboardPanelActive)
            {
                // Controls
                if (MenuControls.Left() && (!isCoR))
                {
                    StartCoroutine(MoveAround(-1));
                }

                if (MenuControls.Right() && (!isCoR))
                {
                    StartCoroutine(MoveAround(1));
                }

                if (MenuControls.Up() && (!isCoR))
                {
                    StartCoroutine(MoveAround(-2));
                }

                if (MenuControls.Down() && (!isCoR))
                {
                    StartCoroutine(MoveAround(2));
                }



                // Edits Visuals based on move
                if (pos != lastPos)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i].GetComponent<Image>().color == inactiveButtonColours[i])
                        {
                            buttons[i].GetComponent<Image>().color = activeButtonColours[i];
                        }
                    }

                    buttons[pos].GetComponent<Image>().color = inactiveButtonColours[pos];
                }
            }
        }

        /// <summary>
        /// Defines what happens when the Confirm button is pressed with no panels open
        /// </summary>
        private void ConfirmOption()
        {
            switch (pos)
            {
                case 0:

                    if (!playPanelActive && activeData.hasPlayPanel)
                    {
                        playPanelActive = true;
                        panelHolder.transform.GetChild(activeData.panels[0]).gameObject.SetActive(true);
                    }

                    break;

                case 1:

                    if (!infoPanelActive && activeData.hasInfoPanel)
                    {
                        panelHolder.transform.GetChild(activeData.panels[1]).gameObject.SetActive(true);
                        pos = 1;
                        isCoR = false;
                        infoPanelActive = true;
                    }

                    break;

                case 2:

                    if (!leaderboardPanelActive && activeData.hasLeaderboard)
                    {
                        leaderboardPanelActive = true;
                        panelHolder.transform.GetChild(activeData.panels[2]).gameObject.SetActive(true);


                        if (MenuControls.ToggleButton() && !isCoR)
                        {
                            ToggleOnlineLocalLeaderboards();
                        }


                        if (!hasPopulated)
                        {
                            switch (activeData.infoPanelPos)
                            {
                                case 0:
                                    StartCoroutine(Call_Ultimate_Pinball_Data_Online_Lives(false));
                                    hasPopulated = true;
                                    break;
                                case 1:
                                    StartCoroutine(Call_Starshine_Online(false));
                                    hasPopulated = true;
                                    break;
                                case 3:
                                    StartCoroutine(Call_CWIS(false));
                                    hasPopulated = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    break;

                case 3:

                    ChangeScene("Arcade-Play");

                    break;
                default:
                    Debug.Log("default");

                    break;
            }
        }

        /// <summary>
        /// Defines what happens when the Return button is pressed with no panels open
        /// </summary>
        private void ReturnOption()
        {
            if (MenuControls.Return())
            {
                switch (pos)
                {
                    case 0:

                        if (playPanelActive)
                        {
                            playPanelActive = false;
                            panelHolder.transform.GetChild(activeData.panels[0]).gameObject.SetActive(false);
                        }

                        break;

                    case 1:

                        if (infoPanelActive)
                        {
                            infoPanelActive = false;
                            panelHolder.transform.GetChild(activeData.panels[1]).gameObject.SetActive(false);
                        }

                        break;

                    case 2:

                        if (leaderboardPanelActive)
                        {
                            leaderboardPanelActive = false;
                            panelHolder.transform.GetChild(activeData.panels[2]).gameObject.SetActive(false);
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Sets up the supported controls UI. (Only run once!)
        /// </summary>
        private void SupportedControlsSetup()
        {
            if (activeData.supportedControls[0]) { supportedControllers[0].color = activeCol; }
            else { supportedControllers[0].color = inactiveCol; }
            if (activeData.supportedControls[1]) { supportedControllers[1].color = activeCol; }
            else { supportedControllers[1].color = inactiveCol; }
            if (activeData.supportedControls[2]) { supportedControllers[2].color = activeCol; }
            else { supportedControllers[2].color = inactiveCol; }
        }

        /// <summary>
        /// Sets up the leaderboard display panel with the top three scores if the game has a leaderboard. (Only run once!)
        /// </summary>
        private void LeaderboardSetup()
        {
            if (activeData.hasLeaderboard)
            {
                if (activeData.gameTitlePos == 0)
                {
                    StartCoroutine(Call_Ultimate_Pinball_Data_Online_Lives(true));
                }
                else if (activeData.gameTitlePos == 1)
                {
                    StartCoroutine(Call_Starshine_Online(true));
                }
                else if (activeData.gameTitlePos == 3)
                {
                    StartCoroutine(Call_CWIS(true));
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    topThreeScores[i].GetComponentsInChildren<Text>()[0].text = "";
                    topThreeScores[i].GetComponentsInChildren<Text>()[1].text = "";
                    topThreeScores[i].GetComponentsInChildren<Text>()[2].text = "";
                }
            }
        }

        /// <summary>
        /// Sets up the menu buttons to show crosses if the game in question does not support the option. (Only run once!)
        /// </summary>
        private void MenuButtonsSetup()
        {
            if (!activeData.hasPlayPanel)
            {
                buttons[0].GetComponentsInChildren<Image>()[2].enabled = true;
            }

            if (!activeData.hasInfoPanel)
            {
                buttons[1].GetComponentsInChildren<Image>()[2].enabled = true;
            }

            if (!activeData.hasLeaderboard)
            {
                buttons[2].GetComponentsInChildren<Image>()[2].enabled = true;
            }
        }

        /// <summary>
        /// Coroutine || Calls the Ultimate Pinball leaderboard and sets up the leaderboard panel with the result, the amount of entries returned are based on the bool.
        /// </summary>
        /// <param name="getTopThree">should it only get the top three entries?</param>
        private IEnumerator Call_Ultimate_Pinball_Data_Online_Lives(bool getTopThree)
        {
            List<UltimatePinballLeaderboardData> listData = new List<UltimatePinballLeaderboardData>(10);

            List<string> ReceivedPlayerName = new List<string>();
            List<string> ReceivedPlayerScore = new List<string>();
            List<string> ReceivedPlayerPlatform = new List<string>();
            List<string> ReceivedPlayerGamemode = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "/getpinballlivestop.php?");

            yield return Request.SendWebRequest();

            if (getTopThree)
            {
                if (Request.error == null)
                {
                    string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                    for (int i = 0; i < 12; i++)
                    {
                        if (i % 4 == 0)
                        {
                            ReceivedPlayerName.Add(Values[i]);
                        }
                        else if (i % 4 == 1)
                        {
                            ReceivedPlayerScore.Add(Values[i]);
                        }
                        else if (i % 4 == 2)
                        {
                            ReceivedPlayerPlatform.Add(Values[i]);
                        }
                        else if (i % 4 == 3)
                        {
                            ReceivedPlayerGamemode.Add(Values[i]);
                        }
                        else
                        {
                            Debug.LogError("Value to added to any list!");
                        }
                    }


                    for (int i = 0; i < 3; i++)
                    {
                        UltimatePinballLeaderboardData Data = new UltimatePinballLeaderboardData();

                        Data.PlayerName = ReceivedPlayerName[i];
                        Data.PlayerScore = int.Parse(ReceivedPlayerScore[i]);
                        Data.PlayerPlatform = ReceivedPlayerPlatform[i];

                        listData.Add(Data);
                    }


                    for (int i = 0; i < 3; i++)
                    {
                        topThreeScores[i].GetComponentsInChildren<Text>()[1].text = listData[i].PlayerName;
                        topThreeScores[i].GetComponentsInChildren<Text>()[2].text = listData[i].PlayerScore.ToString();
                    }
                }
            }
            else
            {
                if (Request.error == null)
                {
                    string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                    for (int i = 0; i < Values.Length - 1; i++)
                    {
                        if (i % 4 == 0)
                        {
                            ReceivedPlayerName.Add(Values[i]);
                        }
                        else if (i % 4 == 1)
                        {
                            ReceivedPlayerScore.Add(Values[i]);
                        }
                        else if (i % 4 == 2)
                        {
                            ReceivedPlayerPlatform.Add(Values[i]);
                        }
                        else if (i % 4 == 3)
                        {
                            ReceivedPlayerGamemode.Add(Values[i]);
                        }
                        else
                        {
                            Debug.LogError("Value to added to any list!");
                        }
                    }


                    for (int i = 0; i < ReceivedPlayerName.Count; i++)
                    {
                        UltimatePinballLeaderboardData Data = new UltimatePinballLeaderboardData();

                        Data.PlayerName = ReceivedPlayerName[i];
                        Data.PlayerScore = int.Parse(ReceivedPlayerScore[i]);

                        listData.Add(Data);
                    }


                    LeaderboardPanel _lPanel = leaderboardPanels[activeData.infoPanelPos].GetComponentInChildren<LeaderboardPanel>();
                    _lPanel.playerNames = new List<string>();
                    _lPanel.playerScores = new List<string>();

                    for (int i = 0; i < ReceivedPlayerName.Count; i++)
                    {
                        _lPanel.playerNames.Add(listData[i].PlayerName);
                        _lPanel.playerScores.Add(listData[i].PlayerScore.ToString());
                    }

                    _lPanel.PopulateLeaderboard();
                }
            }
        }

        /// <summary>
        /// Coroutine || Calls the Operation Starshine leaderboard and sets up the leaderboard panel with the result, the amount of entries returned are based on the bool.
        /// </summary>
        /// <param name="getTopThree">should it only get the top three entries?</param>
        private IEnumerator Call_Starshine_Online(bool getTopThree)
        {
            List<StarshineLeaderboardData> listData = new List<StarshineLeaderboardData>(5);

            List<string> ReceivedPlayer1Name = new List<string>();
            List<string> ReceivedPlayer2Name = new List<string>();
            List<string> ReceivedPlayer1ShipName = new List<string>();
            List<string> ReceivedPlayer2ShipName = new List<string>();
            List<string> ReceivedPlayer1Score = new List<string>();
            List<string> ReceivedPlayer2Score = new List<string>();
            List<string> ReceivedTotalScore = new List<string>();
            List<string> ReceivedPlatform = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "getscorestarshineall.php?");

            yield return Request.SendWebRequest();

            if (getTopThree)
            {
                if (Request.error == null)
                {
                    string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                    // only get the top 5 entries
                    for (int i = 0; i < Values.Length; i++)
                    {
                        if (i % 8 == 0)
                        {
                            ReceivedPlayer1Name.Add(Values[i]);
                        }
                        else if (i % 8 == 1)
                        {
                            ReceivedPlayer2Name.Add(Values[i]);
                        }
                        else if (i % 8 == 2)
                        {
                            ReceivedPlayer1ShipName.Add(Values[i]);
                        }
                        else if (i % 8 == 3)
                        {
                            ReceivedPlayer2ShipName.Add(Values[i]);
                        }
                        else if (i % 8 == 4)
                        {
                            ReceivedPlayer1Score.Add(Values[i]);
                        }
                        else if (i % 8 == 5)
                        {
                            ReceivedPlayer2Score.Add(Values[i]);
                        }
                        else if (i % 8 == 6)
                        {
                            ReceivedTotalScore.Add(Values[i]);
                        }
                        else if (i % 8 == 7)
                        {
                            ReceivedPlatform.Add(Values[i]);
                        }
                        else
                        {
                            Debug.LogError("Value to added to any list!");
                        }
                    }


                    for (int i = 0; i < ReceivedPlatform.Count; i++)
                    {
                        StarshineLeaderboardData Data = new StarshineLeaderboardData();

                        Data.Player1Name = ReceivedPlayer1Name[i];
                        Data.Player2Name = ReceivedPlayer2Name[i];
                        Data.Player1ShipName = ReceivedPlayer1ShipName[i];
                        Data.Player2ShipName = ReceivedPlayer2ShipName[i];
                        Data.Player1Score = int.Parse(ReceivedPlayer1Score[i]);
                        Data.Player2Score = int.Parse(ReceivedPlayer2Score[i]);
                        Data.Platform = ReceivedPlatform[i];

                        listData.Add(Data);
                    }


                    for (int i = 0; i < 3; i++)
                    {
                        if (i < listData.Count)
                        {
                            topThreeScores[i].GetComponentsInChildren<Text>()[1].text = listData[i].Player1Name + " | " + listData[i].Player2Name;
                            topThreeScores[i].GetComponentsInChildren<Text>()[2].text = (listData[i].Player1Score + listData[i].Player2Score).ToString();
                        }
                        else
                        {
                            topThreeScores[i].GetComponentsInChildren<Text>()[1].text = "##### |  #####";
                            topThreeScores[i].GetComponentsInChildren<Text>()[2].text = "000,000,000,000";
                        }
                    }
                }
            }
            else
            {
                if (Request.error == null)
                {
                    string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                    // only get the top 5 entries
                    for (int i = 0; i < Values.Length - 1; i++)
                    {
                        if (i % 8 == 0)
                        {
                            ReceivedPlayer1Name.Add(Values[i]);
                        }
                        else if (i % 8 == 1)
                        {
                            ReceivedPlayer2Name.Add(Values[i]);
                        }
                        else if (i % 8 == 2)
                        {
                            ReceivedPlayer1ShipName.Add(Values[i]);
                        }
                        else if (i % 8 == 3)
                        {
                            ReceivedPlayer2ShipName.Add(Values[i]);
                        }
                        else if (i % 8 == 4)
                        {
                            ReceivedPlayer1Score.Add(Values[i]);
                        }
                        else if (i % 8 == 5)
                        {
                            ReceivedPlayer2Score.Add(Values[i]);
                        }
                        else if (i % 8 == 6)
                        {
                            ReceivedTotalScore.Add(Values[i]);
                        }
                        else if (i % 8 == 7)
                        {
                            ReceivedPlatform.Add(Values[i]);
                        }
                        else
                        {
                            Debug.LogError("Value to added to any list!");
                        }
                    }


                    for (int i = 0; i < ReceivedPlatform.Count; i++)
                    {
                        StarshineLeaderboardData Data = new StarshineLeaderboardData();

                        Data.Player1Name = ReceivedPlayer1Name[i];
                        Data.Player2Name = ReceivedPlayer2Name[i];
                        Data.Player1ShipName = ReceivedPlayer1ShipName[i];
                        Data.Player2ShipName = ReceivedPlayer2ShipName[i];
                        Data.Player1Score = int.Parse(ReceivedPlayer1Score[i]);
                        Data.Player2Score = int.Parse(ReceivedPlayer2Score[i]);
                        Data.Platform = ReceivedPlatform[i];

                        listData.Add(Data);
                    }


                    LeaderboardPanel _lPanel = leaderboardPanels[activeData.infoPanelPos].GetComponentInChildren<LeaderboardPanel>();
                    _lPanel.playerNames = new List<string>();
                    _lPanel.playerScores = new List<string>();

                    for (int i = 0; i < ReceivedPlayer1Name.Count; i++)
                    {
                        _lPanel.playerNames.Add(listData[i].Player1Name + " & " + listData[i].Player2Name);
                        _lPanel.playerScores.Add((listData[i].Player1Score + listData[i].Player2Score).ToString());
                    }

                    _lPanel.PopulateLeaderboard();
                }
            }
        }

        /// <summary>
        /// Coroutine || Calls the CWIS leaderboard and sets up the leaderboard panel with the result, the amount of entries returned are based on the bool.
        /// </summary>
        /// <param name="getTopThree">should it only get the top three entries?</param>
        private IEnumerator Call_CWIS(bool getTopThree)
        {
            List<CWIS.LeaderboardData> listData = new List<CWIS.LeaderboardData>();

            List<string> ReceivedPlayerName = new List<string>();
            List<string> ReceivedPlayerScore = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "/getcwis.php?");

            yield return Request.SendWebRequest();

            if (getTopThree)
            {
                if (Request.error == null)
                {
                    string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                    for (int i = 0; i < 12; i++)
                    {
                        if (i % 2 == 0)
                        {
                            ReceivedPlayerName.Add(Values[i]);
                        }
                        else if (i % 2 == 1)
                        {
                            ReceivedPlayerScore.Add(Values[i]);
                        }
                        else
                        {
                            Debug.LogError("Value to added to any list!");
                        }
                    }


                    for (int i = 0; i < 3; i++)
                    {
                        CWIS.LeaderboardData _data = new CWIS.LeaderboardData();

                        _data.name = ReceivedPlayerName[i];
                        _data.score = int.Parse(ReceivedPlayerScore[i]);

                        listData.Add(_data);
                    }


                    for (int i = 0; i < 3; i++)
                    {
                        topThreeScores[i].GetComponentsInChildren<Text>()[1].text = listData[i].name;
                        topThreeScores[i].GetComponentsInChildren<Text>()[2].text = listData[i].score.ToString();
                    }
                }
            }
            else
            {
                if (Request.error == null)
                {
                    string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                    for (int i = 0; i < Values.Length - 1; i++)
                    {
                        if (i % 2 == 0)
                        {
                            ReceivedPlayerName.Add(Values[i]);
                        }
                        else if (i % 2 == 1)
                        {
                            ReceivedPlayerScore.Add(Values[i]);
                        }
                        else
                        {
                            Debug.LogError("Value to added to any list!");
                        }
                    }


                    for (int i = 0; i < ReceivedPlayerName.Count; i++)
                    {
                        CWIS.LeaderboardData _data = new CWIS.LeaderboardData();

                        _data.name = ReceivedPlayerName[i];
                        _data.score = int.Parse(ReceivedPlayerScore[i]);

                        listData.Add(_data);
                    }


                    LeaderboardPanel _lPanel = leaderboardPanels[activeData.infoPanelPos].GetComponentInChildren<LeaderboardPanel>();
                    _lPanel.playerNames = new List<string>();
                    _lPanel.playerScores = new List<string>();

                    for (int i = 0; i < ReceivedPlayerName.Count; i++)
                    {
                        _lPanel.playerNames.Add(listData[i].name);
                        _lPanel.playerScores.Add(listData[i].score.ToString());
                    }

                    _lPanel.PopulateLeaderboard();
                }
            }
        }

        /// <summary>
        /// Coroutine || Controls the delay between actions when you move around the menus
        /// </summary>
        /// <param name="Value">The amount the pos needs the change for the UI</param>
        private IEnumerator MoveAround(int Value)
        {
            isCoR = true;

            lastPos = pos;
            pos += Value;

            if (pos > maxPos)
            {
                if (pos == 4)
                {
                    pos = 0;
                }
                else
                {
                    pos -= 4;
                }
            }
            else if (pos < 0)
            {
                pos = maxPos;
            }

            if (am)
            {
                am.Play("Menu_Click", Random.Range(.65f, .85f), Random.Range(.85f, 1.15f));
            }

            yield return new WaitForSecondsRealtime(.25f);
            isCoR = false;
        }

        /// <summary>
        /// Changes the scene with a delay and transition (Calls IEnumerator ChangeToScene())
        /// </summary>
        /// <param name="Scene">Scene name to change to</param>
        /// <param name="Delay">Optional || the delay to wait for before changing scene</param>
        public void ChangeScene(string Scene, float Delay = 1.25f)
        {
            StartCoroutine(ChangeToScene(Scene, Delay));
        }

        /// <summary>
        /// Coroutine || Changes the scene after a delay, is called from Method ChangeScene()
        /// </summary>
        /// <param name="NewScene">Scene name to change to</param>
        /// <param name="Delay">Optional || the delay to wait for before changing scene</param>
        IEnumerator ChangeToScene(string NewScene, float Delay = 1.25f)
        {
            inputReady = false;
            transitions.SetBool("ChangeScene", true);
            yield return new WaitForSecondsRealtime(Delay);
            AsyncOperation Async = SceneManager.LoadSceneAsync(NewScene);
            Async.allowSceneActivation = false;
            yield return new WaitForSecondsRealtime(.1f);
            Async.allowSceneActivation = true;
            yield return new WaitForSecondsRealtime(.1f);
            inputReady = true;
        }


        private void ToggleOnlineLocalLeaderboards()
        {
            if (useOnline)
            {
                useOnline = false;

                switch (activeData.infoPanelPos)
                {
                    case 0:
                        
                        break;
                    case 1:
                        
                        break;
                    case 3:
                        
                        break;
                    default:
                        break;
                }
            }
            else
            {
                useOnline = true;
                hasPopulated = false;
            }
        }
    }
}