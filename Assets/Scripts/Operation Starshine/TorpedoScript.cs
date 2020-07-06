using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starshine
{
    public class TorpedoScript : Damage
    {
        [Header("--- { Torpedo } ---")]
        public GameObject ExplosionRadiusPrefab;
        public ParticleSystem ExplosionParticles;


        protected override void OnEnable()
        {
            DamageDelt = false;

            if (!GetComponent<SpriteRenderer>().enabled)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }


        protected override void Start()
        {
            IsTorp = true;
            base.Start();
        }


        private new void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (!collision.gameObject.name.Contains("CWIS"))
            {
                ParticleSystem _go = Instantiate(ExplosionParticles, transform.position, transform.rotation);
                Destroy(_go.gameObject, .75f);
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}