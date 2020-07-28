using CarterGames.Assets.AudioManager;
using System.Collections;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class MissileScript : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosion;
        [SerializeField] private Transform radarSprite;

        private GameManager gm;
        private AudioManager am;
        private MissileSpawer ms;

        private void OnEnable()
        {
            radarSprite.localScale = Vector3.zero;
        }

        private void Start()
        {
            if (!gm)
            {
                gm = FindObjectOfType<GameManager>();
            }

            if (!am)
            {
                am = FindObjectOfType<AudioManager>();
            }

            if (!ms)
            {
                ms = FindObjectOfType<MissileSpawer>();
            }
        }


        private void Update()
        {
            if (radarSprite.localScale.x < 30f)
            {
                radarSprite.localScale += new Vector3(1, 1, 0) * Time.deltaTime * 30;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                // explosion
                GameObject _go = Instantiate(explosion.gameObject, transform.position, transform.rotation);
                _go.GetComponent<ParticleSystem>().Play();

                switch (other.gameObject.GetComponent<Bullet>().shotBy)
                {
                    case 1:
                        gm.IncrementCWIS1HitCount();
                        break;
                    case 2:
                        gm.IncrementCWIS2HitCount();
                        break;
                    default:
                        break;
                }


                if (IsVisibleFrom(this.GetComponentInChildren<Renderer>(), Camera.main))
                {
                    am.Play("missileHitFar", Random.Range(.2f, .3f), Random.Range(1f, 1.25f));
                }
                else
                {
                    am.Play("missileHitFar", Random.Range(.05f, .2f), Random.Range(.75f, .95f));
                }

                gm.AddToScore(50 + (int)(Vector3.Distance(transform.position, Vector3.zero)));
                ms.activeMissiles.Remove(this.gameObject);
                gameObject.SetActive(false);
            }



            if (other.gameObject.CompareTag("Shaft"))
            {
                // explosion
                GameObject _go = Instantiate(explosion.gameObject, transform.position, transform.rotation);
                _go.GetComponent<ParticleSystem>().Play();

                am.Play("missileHitFar", .25f, Random.Range(.75f, .95f));

                gm.AddToScore(20 + (int)(Vector3.Distance(transform.position, Vector3.zero)));
                ms.activeMissiles.Remove(this.gameObject);
                gameObject.SetActive(false);
            }



            if (other.gameObject.CompareTag("Player"))
            {
                am.Play("hit", .5f);
                other.GetComponent<ShipController>().DamageShip(1);
                GameObject _go = Instantiate(explosion.gameObject, transform.position, transform.rotation);
                _go.GetComponent<ParticleSystem>().Play();
                gameObject.SetActive(false);
            }
        }



        /// <summary>
        /// Checks to see if the inputted camera can see the object
        /// </summary>
        /// <param name="renderer">Renderer to check</param>
        /// <param name="camera">Camera view to check</param>
        /// <returns>true / false</returns>
        private bool IsVisibleFrom(Renderer renderer, Camera camera)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }
    }
}