using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Starshine
{
    [RequireComponent(typeof(PlayerController))]
    public class Revive : MonoBehaviour
    {
        public bool NeedReviving;
        public bool IsGhost;
        public float GhostTimer;
        public int GhostLimit;
        public PlayerController PC;
        public SpriteRenderer SR;
        GameManager GM;
        public Color32 Col;

        private void Start()
        {
            PC = GetComponent<PlayerController>();
            SR = GetComponent<SpriteRenderer>();
            GM = FindObjectOfType<GameManager>();
            Col = SR.color;
            GhostLimit = 3;
        }


        private void Update()
        {
            NeedReviving = CheckPlayerHealth();

            if (NeedReviving)
            {
                if (FindObjectsOfType<PlayerController>().Length > 1)
                {
                    if (!IsGhost)
                    {
                        TurnIntoGhost();
                    }


                    GhostTimer += Time.deltaTime;

                    if (GhostTimer > GhostLimit)
                    {
                        --GM.PlayersAlive;
                        GM.SetStats(this.GetComponent<ShipManagement>().PlayerStats, (int)this.GetComponent<ShipManagement>().PlayerNumber);
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    --GM.PlayersAlive;
                    GM.SetStats(this.GetComponent<ShipManagement>().PlayerStats, (int)this.GetComponent<ShipManagement>().PlayerNumber);
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (IsGhost)
                {
                    ReturnToNormal();
                    IsGhost = false;
                }
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (NeedReviving)
            {
                if (collision.gameObject.GetComponent<Revive>())
                {
                    ReturnToNormal();
                }
            }
        }


        private bool CheckPlayerHealth()
        {
            if (PC.Health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void TurnIntoGhost()
        {
            Debug.Log("Turning Into A Ghost - " + gameObject.name);
            gameObject.GetComponent<PlayerController>().IsEnabled = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("OP:SS:ReviveMe");
            SR.color = new Color32((byte)SR.color.r, (byte)SR.color.g, (byte)SR.color.b, (byte)50f);
            IsGhost = true;
        }


        private void ReturnToNormal()
        {
            Debug.Log("Returning From A Ghost - " + gameObject.name);
            gameObject.GetComponent<PlayerController>().IsEnabled = true;
            gameObject.layer = LayerMask.NameToLayer("OP:SS:Player");
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            GhostTimer = 0;
            PC.Health = PC.Ship.Health[(int)PC.GM.ActiveStage] / 2;
            PC.Shield = PC.Ship.Shield[(int)PC.GM.ActiveStage] / 4;
            SR.color = Col;
            IsGhost = false;
        }
    }
}