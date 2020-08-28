using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Pinball
{
    public class PinballTitleColours : MonoBehaviour
    {

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        void OnEnable()
        {
            StartCoroutine(Cycle());
        }


        Color ChooseRandomColour()
        {
            return new Color32((byte)Random.Range(100f, 255f), (byte)Random.Range(100f, 255f), (byte)Random.Range(100f, 255f), 255);
        }


        IEnumerator Cycle()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Text>())
                {
                    transform.GetChild(i).GetComponent<Text>().color = ChooseRandomColour();
                }
            }

            yield return new WaitForSeconds(.5f);

            StartCoroutine(Cycle());
        }
    }
}