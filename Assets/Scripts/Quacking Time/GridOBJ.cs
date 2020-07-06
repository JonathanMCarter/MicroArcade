using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quacking
{
    public class GridOBJ : MonoBehaviour
    {
        public DuckPlayers ControlledBy;
        [SerializeField] private GameManager GM;
        [SerializeField] internal bool LockHex;
        [SerializeField] internal bool IsSafe = true;

        private MapController mapController;


        private void Start()
        {
            ControlledBy = DuckPlayers.None;
            GM = FindObjectOfType<GameManager>();
            mapController = FindObjectOfType<MapController>();
        }


        private void SetColour(Color32 Input, DuckPlayers Player)
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", Input);
            ControlledBy = Player;

            switch (Player)
            {
                case DuckPlayers.P1:
                    GM.Duck1Score = mapController.CalculateScores()[0];
                    break;
                case DuckPlayers.P2:
                    GM.Duck2Score = mapController.CalculateScores()[1];
                    break;
                case DuckPlayers.None:
                    break;
                default:
                    break;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Waterie")
            {
                GetComponent<Renderer>().material.SetColor("_BaseColor", mapController.GetDefaultHexagonColor());
                IsSafe = false;
                ControlledBy = DuckPlayers.None;

                if (other.GetComponent<DuckScript>())
                {
                    switch (other.GetComponent<DuckScript>().Ducks)
                    {
                        case DuckPlayers.P1:
                            GM.Duck1Score = mapController.CalculateScores()[0];
                            GM.Duck2Score = mapController.CalculateScores()[1];
                            break;
                        case DuckPlayers.P2:
                            GM.Duck1Score = mapController.CalculateScores()[0];
                            GM.Duck2Score = mapController.CalculateScores()[1];
                            break;
                        case DuckPlayers.None:
                            break;
                        default:
                            break;
                    }
                }
            }

            if (other.gameObject.tag != "Waterie")
            {
                if (IsSafe)
                {
                    if (!LockHex)
                    {
                        if (other.gameObject.GetComponent<DuckScript>())
                        {
                            SetColour(other.gameObject.GetComponent<DuckScript>().DuckColour, other.gameObject.GetComponent<DuckScript>().Ducks);
                        }
                        else if (other.gameObject.GetComponent<PaintCanScript>())
                        {
                            if (other.gameObject.GetComponent<PaintCanScript>().duckToUse != DuckPlayers.None)
                            {
                                SetColour(other.gameObject.GetComponent<PaintCanScript>().duckColour, other.gameObject.GetComponent<PaintCanScript>().duckToUse);
                            }
                        }
                    }
                    else
                    {
                        SetToUnLocked();
                    }

                    if (other.GetComponent<DuckScript>())
                    {
                        if (other.GetComponent<DuckScript>().LockHex)
                        {
                            SetToLocked();
                        }
                        else
                        {
                            SetToUnLocked();
                        }
                    }
                }
            }
        }


        internal void SetToLocked()
        {
            GetComponent<Renderer>().material.SetTexture("_BaseMap", mapController.GetLockedTexture());
            LockHex = true;
        }


        internal void SetToUnLocked()
        {
            GetComponent<Renderer>().material.SetTexture("_BaseMap", null);
            LockHex = false;
        }
    }
}