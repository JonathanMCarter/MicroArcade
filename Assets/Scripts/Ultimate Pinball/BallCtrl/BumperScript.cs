using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcade;

namespace Pinball.BallCtrl
{
    public class BumperScript : MonoBehaviour
    {
        [SerializeField]
        private float hitScale;

        private float initialScale;
        private GameManager gameManager;
        private AudioManager audioManager;


        public GameObject visibleSprite;
        public List<Sprite> bumperSprites;
        public bool isSphereBumper;
        public bool canScore;
        public int scoreIncrement;



        private void HitByBall()
        {
            initialScale = visibleSprite.transform.localScale.y;
            visibleSprite.transform.localScale = new Vector3(hitScale, hitScale, 1);
        }


        private void ResetScale()
        {
            visibleSprite.transform.localScale = new Vector3(initialScale, initialScale, 1);
        }


        private void ScoreOnHit(Joysticks player)
        {
            switch (player)
            {
                case Joysticks.White:
                    gameManager.Player1Stats.Score += scoreIncrement;
                    break;
                case Joysticks.Black:
                    gameManager.Player2Stats.Score += scoreIncrement;
                    break;
            }
        }



        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            audioManager = gameManager.gameObject.GetComponent<AudioManager>();
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioManager.Play("BumperHit", .1f, Random.Range(.9f, 1.1f));

            HitByBall();

            if (isSphereBumper)
            {
                GetComponentsInChildren<SpriteRenderer>()[1].sprite = bumperSprites[1];
            }

            if (canScore)
            {
                ScoreOnHit(collision.gameObject.GetComponent<BallMoveScript>().LastHit);
                canScore = false;
            }
        }


        private void OnCollisionExit2D(Collision2D collision)
        {
            ResetScale();

            if (isSphereBumper)
            {
                GetComponentsInChildren<SpriteRenderer>()[1].sprite = bumperSprites[0];
            }

            canScore = true;
        }
    }
}