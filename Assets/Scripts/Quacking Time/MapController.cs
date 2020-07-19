/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;

namespace CarterGames.QuackingTime
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private GameObject[] mapHexagons;
        [SerializeField] private Color defaultHexagonColor;
        [SerializeField] private Texture lockedMaterial;
        [SerializeField] private Texture defaultMaterial;


        /// <summary>
        /// returns the locked material
        /// </summary>
        /// <returns></returns>
        public Texture GetLockedTexture()
        {
            return lockedMaterial;
        }


        public Texture GetDefaultTexture()
        {
            return defaultMaterial;
        }


        /// <summary>
        /// returns the default hexagon color for use in other scripts
        /// </summary>
        /// <returns>The defalt hexagon color</returns>
        public Color GetDefaultHexagonColor()
        {
            return defaultHexagonColor;
        }


        /// <summary>
        /// Calculate the scores of all ducks...
        /// </summary>
        public int[] CalculateScores()
        {
            int _duck1Score = 0;
            int _duck2Score = 0;


            for (int i = 0; i < mapHexagons.Length; i++)
            {
                if (mapHexagons[i].GetComponent<GridOBJ>())
                {
                    switch (mapHexagons[i].GetComponent<GridOBJ>().ControlledBy)
                    {
                        case DuckPlayers.P1:
                            ++_duck1Score;
                            break;
                        case DuckPlayers.P2:
                            ++_duck2Score;
                            break;
                        default:
                            break;
                    }
                }
            }

            return new int[2] { _duck1Score, _duck2Score };
        }


        /// <summary>
        /// Resets all hexagons back to default parameters
        /// </summary>
        public void ResetHexagons()
        {
            for (int i = 0; i < mapHexagons.Length; i++)
            {
                if (mapHexagons[i].GetComponent<GridOBJ>())
                {
                    mapHexagons[i].GetComponent<GridOBJ>().ControlledBy = DuckPlayers.None;
                    mapHexagons[i].GetComponent<Renderer>().material.SetColor("_BaseColor", defaultHexagonColor);
                    mapHexagons[i].GetComponent<GridOBJ>().IsSafe = true;
                    mapHexagons[i].GetComponent<GridOBJ>().LockHex = false;
                }
            }
        }


        public void ResetLockedHexagons()
        {
            for (int i = 0; i < mapHexagons.Length; i++)
            {
                if (mapHexagons[i].GetComponent<GridOBJ>())
                {
                    if (mapHexagons[i].GetComponent<GridOBJ>().ControlledBy == DuckPlayers.None)
                    {
                        mapHexagons[i].GetComponent<Renderer>().material.SetColor("_BaseColor", defaultHexagonColor);
                        mapHexagons[i].GetComponent<Renderer>().material.SetTexture("_BaseMap", defaultMaterial);
                        mapHexagons[i].GetComponent<GridOBJ>().LockHex = false;
                    }
                }
            }
        }


        public void SetControlledHexagonsToLocked(DuckPlayers _duck)
        {
            for (int i = 0; i < mapHexagons.Length; i++)
            {
                if (mapHexagons[i].GetComponent<GridOBJ>())
                {
                    if (mapHexagons[i].GetComponent<GridOBJ>().ControlledBy == _duck)
                    {
                        mapHexagons[i].GetComponent<Renderer>().material.SetTexture("_BaseMap", lockedMaterial);
                        mapHexagons[i].GetComponent<GridOBJ>().LockHex = true;
                    }
                }
            }
        }



        public void SetControlledHexagonsToUnlocked(DuckPlayers _duck)
        {
            for (int i = 0; i < mapHexagons.Length; i++)
            {
                if (mapHexagons[i].GetComponent<GridOBJ>())
                {
                    if (mapHexagons[i].GetComponent<GridOBJ>().ControlledBy == _duck)
                    {
                        mapHexagons[i].GetComponent<Renderer>().material.SetTexture("_BaseMap", defaultMaterial);
                        mapHexagons[i].GetComponent<GridOBJ>().LockHex = false;
                    }
                }
            }
        }


        /// <summary>
        /// Removes all hexagons for the inputted duck... 
        /// </summary>
        /// <param name="RemoveDuckColour">The Duck to change to</param>
        public void RemoveAllOfOneColour(DuckPlayers RemoveDuckColour)
        {
            for (int i = 0; i < mapHexagons.Length; i++)
            {
                if (mapHexagons[i].GetComponent<GridOBJ>())
                {
                    if (mapHexagons[i].GetComponent<GridOBJ>().ControlledBy == RemoveDuckColour)
                    {
                        mapHexagons[i].GetComponent<GridOBJ>().ControlledBy = DuckPlayers.None;
                        mapHexagons[i].GetComponent<Renderer>().material.SetColor("_BaseColor", defaultHexagonColor);

                        if (mapHexagons[i].GetComponent<Renderer>().material.GetTexture("_BaseMap") != defaultMaterial)
                        {
                            mapHexagons[i].GetComponent<Renderer>().material.SetTexture("_BaseMap", defaultMaterial);
                            mapHexagons[i].GetComponent<GridOBJ>().LockHex = false;
                        }
                    }
                }
            }
        }




        /// <summary>
        /// Chooses a location for the objects to spawn into
        /// </summary>
        /// <returns></returns>
        public Vector3 GetRandomHexagon()
        {
            int _randomNumber = Random.Range(0, mapHexagons.Length);

            if (mapHexagons[_randomNumber].GetComponent<GridOBJ>())
            {
                if (mapHexagons[_randomNumber].GetComponent<GridOBJ>().IsSafe)
                {
                    return mapHexagons[_randomNumber].transform.GetChild(0).transform.position;
                }
                else
                {
                    return Vector3.zero;
                }
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}