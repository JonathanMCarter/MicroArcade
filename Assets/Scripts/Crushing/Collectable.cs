using CarterGames.Assets.AudioManager;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject collectionUI;

        private AudioManager audioManager;
        private GameManager gameManager;

        public enum StarType { Silver, Gold, None };
        public StarType starType;


        private void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
            collectionUI = GameObject.FindGameObjectWithTag("StarCollectionUI");
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collectionUI.GetComponent<Animator>().SetTrigger("collectStar");
                audioManager.Play("CollectStar", .75f);

                switch (starType)
                {
                    case StarType.Silver:
                        gameManager.saveData.starsCollected[0]++;
                        break;
                    case StarType.Gold:
                        gameManager.saveData.starsCollected[1]++;
                        break;
                }
            }

            gameObject.SetActive(false);

            GameObject _go = Instantiate(explosionPrefab, transform.position, transform.rotation);

            //changes the colour of the explosion particles to match the player's colour
            var _col = _go.GetComponent<ParticleSystem>().colorOverLifetime;
            Gradient _newGradient = new Gradient();
            _newGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(GetComponent<SpriteRenderer>().color, 0.0f), new GradientColorKey(GetComponent<SpriteRenderer>().color, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(0f, 1f) });
            _col.color = _newGradient;

            _go.GetComponent<ParticleSystem>().Play();
        }
    }
}