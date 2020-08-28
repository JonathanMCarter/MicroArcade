using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Starshine
{
    public class StageController : MonoBehaviour
    {
        [Header("Active Stage")]
        public Stage ActiveStage;
        public int CurrentStagePhase;
        public int MaxPhasesInStage;
        public List<Stage> GameStages;

        public List<GameObject> Enemies;
        public List<int> EnemiesAmount;
        public List<GameObject> SpawnedEnemies;

        public bool SpawnWave;
        public int StageComplete;
        public int NumberInStage;
        public int CurrentStageHealth;
        public int ActiveStageNumber;

        [Header("Stage HealthBar")]
        public Slider StageHealthBar;

        public LayerMask DoNotOverlap;

        GameManager GM;
        internal CutsceneScript CSS;
        BackgroundScript BS;

        bool IsCoRunning;
        bool IsActive;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Awake()
        {
            GM = GetComponent<GameManager>();
            CSS = GetComponent<CutsceneScript>();
            BS = FindObjectOfType<BackgroundScript>();
            Init();
        }


        void Start()
        {
            StageHealthBar.maxValue = ActiveStage.MaxHealth;
            CurrentStageHealth = ActiveStage.MaxHealth;
        }


        void Update()
        {
            if ((SpawnWave) && (!CSS.IsInCutscene))
            {
                // Spawn Waves
                if (CurrentStagePhase != MaxPhasesInStage)
                {
                    DetectObjectsInPhase(ActiveStage.Phases[CurrentStagePhase]);
                    SpawnWave = false;
                }
                else
                {
                    ChangeToNextStage();
                }
            }


            StageHealthBar.value = GetStageHealth();
        }

        /// <summary>
        /// Changes the Game Stage SO to the next one in it's list an makes all the stuff happen to set it up
        /// </summary>
        void ChangeToNextStage()
        {
            if (CurrentStageHealth == 0)
            {
                // Respawns all the enemies (needs to really just re-update their stages to the new stage variants, but will do that on a seperate occasion)
                ReInit();

                CSS.FadeIntoCutscene = true;

                ActiveStageNumber++;
                ActiveStage = GameStages[ActiveStageNumber];

                MaxPhasesInStage = ActiveStage.Phases.Count;

                // Update Health Bar (Max Value, Colour & Label Text)
                StageHealthBar.GetComponent<Slider>().maxValue = ActiveStage.MaxHealth;
                StageHealthBar.GetComponentsInChildren<Image>()[1].color = ActiveStage.StageHealthBarColour;
                StageHealthBar.transform.parent.GetComponentInChildren<Text>().text = ActiveStage.StageTitle;

                // Updates the Stage Health to be the new stage's max health
                CurrentStageHealth = ActiveStage.MaxHealth;

                // Set the stage phase to its default value (0)
                CurrentStagePhase = 0;

                // Does a check to make sure the stage is not changed when it moves from Stage 3 to the Final Boss
                if (GM.ActiveStage != Stages.Stage3)
                {
                    GM.ActiveStage = (Stages)ActiveStageNumber;
                    BS.UpdateType = true;
                }

                // Suppost to add a delay to the new stage's first wave, doesn't seem to work currently :( !!!
                if (!IsCoRunning)
                {
                    Debug.Log("This Was Called");
                    StartCoroutine(WaitToStartNewStage(3f));
                }

                // update health and shields for ship
                for (int i = 0; i < FindObjectsOfType<ShipManagement>().Length; i++)
                {
                    if (!FindObjectsOfType<ShipManagement>()[i].IsEm)
                    {
                        FindObjectsOfType<ShipManagement>()[i].UpdatePlayerHealthStats();
                    }
                }
            }
        }

        /// <summary>
        /// Spawns the next phase.... 
        /// </summary>
        public void NextPhase()
        {
            ++StageComplete;
            Debug.Log("Amount Done Increases");

            if (StageComplete == NumberInStage)
            {
                Debug.Log("New Stage");
                ++CurrentStagePhase;
                SpawnWave = true;
                StageComplete = 0;
            }
        }

        /// <summary>
        /// Initilises the enemies at the start of the game for the first stage
        /// </summary>
        void Init()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                for (int j = 0; j < EnemiesAmount[i]; j++)
                {
                    GameObject Go = Instantiate(Enemies[i]);
                    Go.SetActive(false);
                    SpawnedEnemies.Add(Go);
                }
            }

            ActiveStage = GameStages[0];
            StageHealthBar.GetComponentsInChildren<Image>()[1].color = ActiveStage.StageHealthBarColour;
            StageHealthBar.transform.parent.GetComponentInChildren<Text>().text = ActiveStage.StageTitle;

            MaxPhasesInStage = ActiveStage.Phases.Count;
        }

        /// <summary>
        /// Re-initilises the enemies for the new stage
        /// </summary>
        void ReInit()
        {
            SpawnedEnemies.Clear();

            for (int i = 0; i < Enemies.Count; i++)
            {
                for (int j = 0; j < EnemiesAmount[i]; j++)
                {
                    GameObject Go = Instantiate(Enemies[i]);
                    Go.SetActive(false);
                    SpawnedEnemies.Add(Go);
                }
            }
        }


        /// <summary>
        /// Goes through the stage phase and gets which enemies are needed in the stage to be spawned
        /// </summary>
        /// <param name="Name">Current stage - phase - enemies to spawn string</param>
        void DetectObjectsInPhase(string Name)
        {
            if (Name.Contains("+"))
            {

                string[] Array = Name.Split('+');

                Debug.Log(Array.Length);
                Debug.Log(Array[0]);
                Debug.Log(Array[1]);

                NumberInStage = Array.Length;

                for (int i = 0; i < NumberInStage; i++)
                {
                    Debug.Log("Spawing Multi");
                    SpawnMultipleObjectsIntoScene(i, Array);
                }
            }
            else
            {
                for (int i = 0; i < SpawnedEnemies.Count; i++)
                {
                    if (SpawnedEnemies[i].name.Contains(Name))
                    {
                        SpawnedEnemies[i].SetActive(true);
                        NumberInStage = 1;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Spawns more than one enemy type into the scene at once (used when the stage has more than one enemy in it)
        /// </summary>
        /// <param name="pos">Position in the string array to check</param>
        /// <param name="Array">THe string array for the enemies to spawn in this phase of the stage</param>
        void SpawnMultipleObjectsIntoScene(int pos, string[] Array)
        {
            for (int j = 0; j < SpawnedEnemies.Count; j++)
            {
                if ((!SpawnedEnemies[j].activeInHierarchy) && (SpawnedEnemies[j].name.Contains("Rocket")) && (!Array[pos].Contains("Drone")))
                {
                    Debug.Log("Not Spawning Drones");
                    Vector3 NewPos = new Vector3(Random.Range(-16f, 16f), 12.5f, 0f);
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(NewPos, .5f, DoNotOverlap);

                    while (colliders.Length != 0)
                    {
                        NewPos = new Vector3(Random.Range(-16f, 16f), 12.5f, 0f);
                        colliders = Physics2D.OverlapCircleAll(NewPos, .5f, DoNotOverlap);
                    }

                    SpawnedEnemies[j].transform.position = NewPos;
                    SpawnedEnemies[j].SetActive(true);
                    break;
                }
                else if ((!SpawnedEnemies[j].activeInHierarchy) && (Array[pos].Contains("Drone")))
                {
                    if (SpawnedEnemies[j].name.Contains(Array[pos]))
                    {
                        SpawnedEnemies[j].SetActive(true);
                        break;
                    }
                }
            } 
        }


        /// <summary>
        /// Gets the current stage health value as an Int
        /// </summary>
        /// <returns>(Int) Current Stage Health</returns>
        int GetStageHealth()
        {
            return CurrentStageHealth;
        }

        /// <summary>
        /// Reduces the stage health by 1
        /// </summary>
        internal void ReduceStageHealth()
        {
            --CurrentStageHealth;
        }

        /// <summary>
        /// Overload - Reduces the stage health by the inputted amount (needed for the final boss to use the same health bar)
        /// </summary>
        /// <param name="Amount">Amount to reduce the health by</param>
        internal void ReduceStageHealth(int Amount)
        {
            CurrentStageHealth -= Amount;
        }

        /// <summary>
        /// Changes the text UI below the stage health bar to what is inputted
        /// </summary>
        /// <param name="LabelValue">The new string for the health bar text UI</param>
        internal void ChangeStageLabel(string LabelValue)
        {
            StageHealthBar.transform.parent.GetComponentInChildren<Text>().text = LabelValue;
        }

        /// <summary>
        /// Changes the sage health bar slider colour to the new value
        /// </summary>
        /// <param name="NewColour">Colour to set as</param>
        internal void ChangeStageBarColour(Color32 NewColour)
        {
            StageHealthBar.GetComponentsInChildren<Image>()[1].color = NewColour;
        }


        internal void StageDamaged()
        {
            StartCoroutine(FlashCol());
        }


        IEnumerator FlashCol()
        {
            StageHealthBar.GetComponentsInChildren<Image>()[1].color = Color.white;
            yield return new WaitForSeconds(.1f);
            StageHealthBar.GetComponentsInChildren<Image>()[1].color = ActiveStage.StageHealthBarColour;
        }

        /// <summary>
        /// Delays the spawning of the next wave by the inputted time (in seconds)
        /// </summary>
        /// <param name="HowLong">How many seconds to wait</param>
        IEnumerator WaitToStartNewStage(float HowLong)
        {
            IsCoRunning = true;
            yield return new WaitForSeconds(HowLong);
            SpawnWave = true;
            IsCoRunning = false;
        }
    }
}