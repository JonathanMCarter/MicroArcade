using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace CarterGames.UltimatePinball.BallCtrl
{
    public class LightBarrierCtrl : MonoBehaviour
    {

        public int NumberOfActiveBarriers;

        public enum BarrierStages
        {
            Blue,
            Green,
            Yellow,
            Red,
            Purple,
        };

        public BarrierStages ScoringStage;

        [SerializeField]
        private List<LightBarrierOBJ> Barriers;

        float GivenScore;
        float Mulitplier;


        private void Start()
        {
            for (int i = 0; i < GetComponentsInChildren<LightBarrierOBJ>().Length; i++)
            {
                Barriers.Add(GetComponentsInChildren<LightBarrierOBJ>()[i]);
            }
        }


        private void Update()
        {
            switch (ScoringStage)
            {
                case BarrierStages.Blue:

                    Mulitplier = 1;

                    if (AreAllBarriersActive())
                    {
                        NumberOfActiveBarriers = 0;
                        UpdateColours();

                        for (int i = 0; i < Barriers.Count; i++)
                        {
                            Barriers[i].ResetColours();
                        }

                        ScoringStage = BarrierStages.Green;
                    }

                    break;
                case BarrierStages.Green:

                    Mulitplier = 2;

                    if (AreAllBarriersActive())
                    {
                        NumberOfActiveBarriers = 0;

                        for (int i = 0; i < Barriers.Count; i++)
                        {
                            Barriers[i].ResetColours();
                        }

                        ScoringStage = BarrierStages.Yellow;
                        UpdateColours();
                    }

                    break;
                case BarrierStages.Yellow:

                    Mulitplier = 4;

                    if (AreAllBarriersActive())
                    {
                        NumberOfActiveBarriers = 0;
                        ScoringStage = BarrierStages.Red;


                        for (int i = 0; i < Barriers.Count; i++)
                        {
                            Barriers[i].ResetColours();
                        }

                        UpdateColours();
                    }

                    break;
                case BarrierStages.Red:

                    Mulitplier = 8;

                    if (AreAllBarriersActive())
                    {
                        NumberOfActiveBarriers = 0;


                        for (int i = 0; i < Barriers.Count; i++)
                        {
                            Barriers[i].ResetColours();
                        }

                        ScoringStage = BarrierStages.Purple;
                        UpdateColours();
                    }

                    break;
                case BarrierStages.Purple:

                    Mulitplier = 16;

                    if (AreAllBarriersActive())
                    {
                        GivenScore += 10000;
                    }

                    break;
            }
        }


        /// <summary>
        /// Checks to see if all the barriers are active
        /// </summary>
        /// <returns>true or false value</returns>
        bool AreAllBarriersActive()
        {
            if (NumberOfActiveBarriers == 3) { return true; }
            else { return false; }
        }


        void UpdateColours()
        {
            for (int i = 0; i < Barriers.Count; i++)
            {
                Debug.Log("runing");
                Barriers[i].ResetColours();
            }
        }
    }
}