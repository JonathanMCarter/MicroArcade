using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class FlickerButton : MonoBehaviour
    {
        private Button btn;
        public bool shouldFlicker;
        private bool isCoR;


        private void Start()
        {
            btn = GetComponent<Button>();
        }

        private void Update()
        {
            if (shouldFlicker && !isCoR)
            {
                if (!btn.interactable)
                {
                    btn.interactable = true;
                }

                StartCoroutine(flickerCo());
            }
        }

        private IEnumerator flickerCo()
        {
            isCoR = true;
            yield return new WaitForSeconds(.25f);
            btn.targetGraphic.color = Color.yellow;
            yield return new WaitForSeconds(.25f);
            btn.targetGraphic.color = new Color32(50, 50, 50, 255);
            isCoR = false;
        }

        public void StopFlicker()
        {
            btn.targetGraphic.color = new Color32(50, 50, 50, 255);
            btn.interactable = false;
            shouldFlicker = false;
            StopAllCoroutines();
            isCoR = false;
        }
    }
}