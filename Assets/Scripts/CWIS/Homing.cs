using CarterGames.Assets.AudioManager;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class Homing : MonoBehaviour
    {
        [SerializeField] private Transform radarIcon;

        private AudioManager am;
        private Rigidbody rb;
        internal GameObject targetPos;
        public int missileSpd = 150;


        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            am = FindObjectOfType<AudioManager>();
        }


        private void FixedUpdate()
        {
            Vector3 dir = new Vector3(targetPos.transform.position.x - transform.position.x, 0, targetPos.transform.position.z - transform.position.z);
            rb.velocity = (dir).normalized * missileSpd;
            transform.LookAt(targetPos.transform.position);

            if (!targetPos.gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
        }


        private void LateUpdate()
        {
            radarIcon.transform.rotation = Quaternion.Euler(90, -90, 0);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Missile"))
            {
                other.gameObject.SetActive(false);
                gameObject.SetActive(false);
                am.Play("missileHitFar", .25f, Random.Range(.75f, .95f));
            }
        }
    }
}