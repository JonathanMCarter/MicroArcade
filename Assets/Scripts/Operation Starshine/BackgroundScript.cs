using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Starshine
{
    public class BackgroundScript : MonoBehaviour
    {
        public CanvasGroup WaterBG;
        public CanvasGroup CloudBG;
        public CanvasGroup SpaceBG;
        public bool UpdateType;
        GameManager GM;

        public bool ChangeToWater;
        public bool ChangeToCloud;
        public bool ChangeToSpace;

        public bool WaterVisible;
        public bool CloudVisible;
        public bool SpaceVisible;


        private void Start()
        {
            GM = FindObjectOfType<GameManager>();
        }


        private void Update()
        {
            if (UpdateType)
            {
                EnableCorrectGrid();
            }

            // Water
            if ((ChangeToWater) && (!WaterVisible))
            {
                WaterBG.alpha += Time.deltaTime;
            }


            if (WaterBG.alpha >= 1)
            {
                WaterVisible = true;
            }

            // Cloud
            if ((ChangeToCloud) && (!CloudVisible))
            {
                CloudBG.alpha += Time.deltaTime / 4;
                WaterBG.alpha -= CloudBG.alpha;
            }

            if (CloudBG.alpha >= 1)
            {
                CloudVisible = true;
            }

            // Space
            if ((ChangeToSpace) && (!SpaceVisible))
            {
                SpaceBG.alpha += Time.deltaTime;
                CloudBG.alpha -= SpaceBG.alpha;
            }

            if (SpaceBG.alpha >= 1)
            {
                SpaceVisible = true;
            }
        }

        

        void EnableCorrectGrid()
        {
            switch (GM.ActiveStage)
            {
                case Stages.Stage1:

                    ChangeToWater = true;

                    break;
                case Stages.Stage2:

                    ChangeToCloud = true;

                    break;
                case Stages.Stage3:

                    ChangeToSpace = true;

                    break;
                default:
                    break;
            }

            UpdateType = false;
        }


        public void ResetAllBackgrounds()
        {
            ChangeToWater = false;
            ChangeToCloud = false;
            ChangeToSpace = false;
            WaterVisible = false;
            CloudVisible = false;
            SpaceVisible = false;
        }
    }
}