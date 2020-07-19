using UnityEngine;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class CrusherScript : MonoBehaviour
    {
        [SerializeField] private GameObject crusherParticles;

        private CameraShakeScript camShakeScript;
        private Animator anim;

        public bool isCrushing;


        private void Start()
        {
            camShakeScript = Camera.main.GetComponent<CameraShakeScript>();
            anim = GetComponent<Animator>();
        }

        public void SpawnParticles()
        {
            GameObject _go = Instantiate(crusherParticles, transform.position, transform.rotation);
            Destroy(_go, 1f);
        }

        private void ShakeCam()
        {
            if (camShakeScript)
            {
                camShakeScript.ShakeCamera();
            }
        }

        private void StopCrusher()
        {
            anim.SetBool("Crush", false);
            isCrushing = false;
        }

        private void ResetTriggers()
        {
            // disables the trigger boxes
            foreach (BoxCollider2D B in GetComponentsInChildren<BoxCollider2D>())
            {
                if (B.isTrigger)
                {
                    B.enabled = false;
                }
            }
        }
    }
}