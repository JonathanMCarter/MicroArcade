using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcade;

namespace Pinball.BallCtrl
{
    public class LightCircleScript : MonoBehaviour
    {

        public enum Phases
        {
            Default,
            Cyan,
            Orange,
            Magenta,
            Duck,
            None,
        };

        public Phases SystemPhase;


        public List<SpriteRenderer> Lights;
        public int NumberActive;

        public Joysticks Player;

        public Color[] Col;

        GameManager GM;
        AudioManager AM;
        Animator Anim;

        private void Start()
        {
            GM = FindObjectOfType<GameManager>();
            AM = GM.GetComponent<AudioManager>();
            Anim = GetComponent<Animator>();
            ChangeLightColours();
        }


        private void Update()
        {
            if (NumberActive == Lights.Count)
            {
                switch (SystemPhase)
                {
                    case Phases.Default:
                        AddScore(1000);
                        SystemPhase = Phases.Cyan;
                        break;
                    case Phases.Cyan:
                        AddScore(2500);
                        SystemPhase = Phases.Orange;
                        break;
                    case Phases.Orange:
                        AddScore(5000);
                        SystemPhase = Phases.Magenta;
                        break;
                    case Phases.Magenta:
                        AddScore(7500);
                        SystemPhase = Phases.Duck;
                        break;
                    case Phases.Duck:
                        AddScore(10000);
                        break;
                    default:
                        break;
                }

                ChangeLightColours();

                AM.Play("LogoComp", .15f);
                Anim.SetTrigger("Spin");

                for (int i = 0; i < Lights.Count; i++)
                {
                    Lights[i].gameObject.GetComponent<LightCircleLight>().IsActive = false;
                }

                NumberActive = 0;
            }
        }


        internal int CheckAmount()
        {
            int I = 0;

            for (int i = 0; i < Lights.Count; i++)
            {
                if ((Lights[i].gameObject.GetComponent<LightCircleLight>()) && (Lights[i].gameObject.GetComponent<LightCircleLight>().IsActive))
                {
                    ++I;
                    Debug.Log("ffff");
                }
            }

            return I;
        }


        void ChangeLightColours()
        {
            for (int i = 0; i < Lights.Count; i++)
            {
                switch (SystemPhase)
                {
                    case Phases.Default:

                        if (GetComponent<SpriteRenderer>())
                        {
                            GetComponent<SpriteRenderer>().color = Col[0];
                        }

                        break;
                    case Phases.Cyan:

                        if (GetComponent<SpriteRenderer>())
                        {
                            GetComponent<SpriteRenderer>().color = Col[1];
                        }

                        break;
                    case Phases.Orange:

                        if (GetComponent<SpriteRenderer>())
                        {
                            GetComponent<SpriteRenderer>().color = Col[2];
                        }

                        break;
                    case Phases.Magenta:

                        if (GetComponent<SpriteRenderer>())
                        {
                            GetComponent<SpriteRenderer>().color = Col[3];
                        }

                        break;
                    case Phases.Duck:

                        if (GetComponent<SpriteRenderer>())
                        {
                            GetComponent<SpriteRenderer>().color = Col[4];
                        }

                        break;
                    case Phases.None:
                        break;
                    default:
                        break;
                }
            }
        }


        void AddScore(int ScoreToAdd = 0)
        {
            switch (Player)
            {
                case Joysticks.White:
                    GM.Player1Stats.Score += ScoreToAdd;
                    break;
                case Joysticks.Black:
                    GM.Player2Stats.Score += ScoreToAdd;
                    break;
                default:
                    break;
            }
        }
    }
}