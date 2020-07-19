using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.UltimatePinball
{
    public class AnimationDelay : MonoBehaviour
    {
        public float DelayAmount;
        public string TriggerName;
        Animator Anim;

        void Awake()
        {
            if (!Anim) { Anim = GetComponent<Animator>(); }
            StartCoroutine(AnimDelay());
        }

        IEnumerator AnimDelay()
        {
            yield return new WaitForSeconds(DelayAmount);
            Anim.SetBool(TriggerName, true);
        }
    }
}