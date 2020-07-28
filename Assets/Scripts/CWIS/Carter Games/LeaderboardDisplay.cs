using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Assets.LeaderboardManager
{
    public class LeaderboardDisplay : MonoBehaviour
    {
        [SerializeField] private LeaderboardData[] data;
        [SerializeField] private GameObject rowAnchor;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private GameObject loading;

        public bool updateLeaderboard;
        public bool useOnline;


        private void Start()
        {
            if (useOnline)
            {
                StartCoroutine(GetAllEntries());
            }
            else
            {

            }
        }


        private void Update()
        {
            if (updateLeaderboard)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    GameObject _newRow = Instantiate(rowPrefab, rowAnchor.transform);
                    _newRow.GetComponentsInChildren<Text>()[0].text = (i + 1).ToString();
                    _newRow.GetComponentsInChildren<Text>()[1].text = data[i].name;
                    _newRow.GetComponentsInChildren<Text>()[2].text = data[i].score.ToString();
                    _newRow.SetActive(true);
                }

                loading.SetActive(false);
                updateLeaderboard = false;
            }
        }

        private IEnumerator GetAllEntries()
        {
            loading.SetActive(true);

            List<LeaderboardData> ListData = new List<LeaderboardData>();

            List<string> ReceivedPlayerName = new List<string>();
            List<string> ReceivedPlayerScore = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get("https://carter.games/leaderboardfiles/getscoress9.php?");

            yield return Request.SendWebRequest();

            if (Request.error == null)
            {
                string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                // only get the top 5 entries
                for (int i = 0; i < Values.Length - 1; i++)
                {
                    if ((i % 2) == 0)
                    {
                        ReceivedPlayerName.Add(Values[i]);
                    }
                    else if ((i % 2) == 1)
                    {
                        ReceivedPlayerScore.Add(Values[i]);
                    }
                    else
                    {
                        Debug.LogError("Value not added to any list!" + Values[i]);
                    }
                }


                for (int i = 0; i < ReceivedPlayerName.Count; i++)
                {
                    LeaderboardData Data = new LeaderboardData();

                    Data.name = ReceivedPlayerName[i];

                    if (ReceivedPlayerScore[i] != null)
                    {
                        Data.score = float.Parse(ReceivedPlayerScore[i]);
                    }

                    ListData.Add(Data);
                }

                data = ListData.ToArray();

                updateLeaderboard = true;
            }
        }
    }
}