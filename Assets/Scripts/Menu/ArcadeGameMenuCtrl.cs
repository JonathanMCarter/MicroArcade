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
using UnityEditorInternal;

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
        [SerializeField] private Text gameTitle;
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
        [SerializeField] private bool playPanelActive;
        [SerializeField] private bool infoPanelActive;
        [SerializeField] private bool leaderboardPanelActive;


        [Header("Tutorial Options")]
        [SerializeField] private GameObject[] tutorialPages;


        [Header("(CG) - Audio Manager")]
        [SerializeField] private AudioManager am;


        private GameObject panelHolder;



        private void Start()
        {
            panelHolder = GameObject.FindGameObjectWithTag("Respawn");


            // Sets the game data for the menu to the one selected
            activeData = data[PlayerPrefs.GetInt("GameSel")];

            // sets the display info up
            gameTitle.text = activeData.GameTitle;
            gameDesc.text = activeData.GameDesc;

            gameBackground.sprite = activeData.gameBackground;


            if (activeData.supportedControls[0]) { supportedControllers[0].color = activeCol; }
            else { supportedControllers[0].color = inactiveCol; }
            if (activeData.supportedControls[1]) { supportedControllers[1].color = activeCol; }
            else { supportedControllers[1].color = inactiveCol; }
            if (activeData.supportedControls[2]) { supportedControllers[2].color = activeCol; }
            else { supportedControllers[2].color = inactiveCol; }


            if (activeData.hasLeaderboard)
            {
                if (activeData.GameTitle.Contains("Pinball"))
                {
                    StartCoroutine(Call_Ultimate_Pinball_Data_Online_Lives());
                }
                else if (activeData.GameTitle.Contains("Starshine"))
                {
                    StartCoroutine(Call_Starshine_Online());
                }
            }


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
            ConfirmOption();
            ReturnOption();

            if (infoPanelActive)
            {
                TutorialMovement();
            }
        }


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


        private void ConfirmOption()
        {
            if (MenuControls.Confirm())
            {
                switch (pos)
                {
                    case 0:

                        if (!playPanelActive)
                        {
                            playPanelActive = true;
                            panelHolder.transform.GetChild(activeData.panels[0]).gameObject.SetActive(true);
                        }

                        break;

                    case 1:

                        if (!infoPanelActive)
                        {
                            infoPanelActive = true;
                            panelHolder.transform.GetChild(activeData.panels[1]).gameObject.SetActive(true);
                            pos = 0;
                            maxPos = 2;
                            isCoR = false;
                        }

                        break;

                    case 2:

                        if (!leaderboardPanelActive)
                        {
                            leaderboardPanelActive = true;
                            panelHolder.transform.GetChild(activeData.panels[2]).gameObject.SetActive(true);
                        }

                        break;

                    case 3:

                        ChangeScene("Arcade-Play");

                        break;
                    default:
                        break;
                }
            }
        }


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


        private IEnumerator Call_Ultimate_Pinball_Data_Online_Lives()
        {
            List<UltimatePinballLeaderboardData> listData = new List<UltimatePinballLeaderboardData>(10);

            List<string> ReceivedPlayerName = new List<string>();
            List<string> ReceivedPlayerScore = new List<string>();
            List<string> ReceivedPlayerPlatform = new List<string>();
            List<string> ReceivedPlayerGamemode = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "/getpinballlivestop.php?");

            yield return Request.SendWebRequest();

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


        private IEnumerator Call_Starshine_Online()
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


        private IEnumerator MoveAround(int Value)
        {
            isCoR = true;

            lastPos = pos;
            pos += Value;

            if (pos > maxPos)
            {
                pos -= 4;
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


        public void ChangeScene(string Scene, float Delay = 1.25f)
        {
            StartCoroutine(ChangeToScene(Scene, Delay));
        }


        IEnumerator ChangeToScene(string NewScene, float Delay = 1.25f)
        {
            inputReady = false;
            yield return new WaitForSecondsRealtime(Delay);
            AsyncOperation Async = SceneManager.LoadSceneAsync(NewScene);
            Async.allowSceneActivation = false;
            yield return new WaitForSecondsRealtime(.1f);
            Async.allowSceneActivation = true;
            yield return new WaitForSecondsRealtime(.1f);
            inputReady = true;
        }


        private void TutorialMovement()
        {
            if (MenuControls.Left() && !isCoR)
            {
                StartCoroutine(MoveAround(-1));
            }
            else if (MenuControls.Right() && !isCoR)
            {
                StartCoroutine(MoveAround(1));
            }


            for (int i = 0; i < tutorialPages[activeData.infoPanelPos].transform.childCount; i++)
            {
                if (i == pos && !tutorialPages[activeData.infoPanelPos].transform.GetComponentsInChildren<GameObject>()[i].activeInHierarchy)
                {
                    tutorialPages[activeData.infoPanelPos].transform.GetComponentsInChildren<GameObject>()[i].SetActive(true);
                }
                else if (i != pos && tutorialPages[activeData.infoPanelPos].transform.GetComponentsInChildren<GameObject>()[i].activeInHierarchy)
                {
                    tutorialPages[pos].SetActive(false);
                }
            }
        }
    }
}