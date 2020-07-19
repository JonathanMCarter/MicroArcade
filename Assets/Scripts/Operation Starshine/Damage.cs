using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CarterGames.Starshine
{
    public class Damage : MonoBehaviour
    {
        int PlayerShotFrom;
        public ShipManagement.ShieldTypes Type;

        public bool IsTorp;
        public bool IsLaser;

        public int DMG;
        internal bool DamageDelt;
        internal bool HitCWISDetection;
        public bool ShouldDestroy = true;

        public GameObject Explosion;

        float TParam;
        GameManager GM;

        protected virtual void OnEnable()
        {
            if (!IsLaser)
            {
                if ((GetComponent<BoxCollider2D>()) && (!GetComponent<BoxCollider2D>().enabled))
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                }
            }

            DamageDelt = false;
        }


        protected virtual void Start()
        {
            if (!GM)
            {
                GM = FindObjectOfType<GameManager>();
            }
        }


        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsTorp)
            {
                if (collision.gameObject.GetComponent<AutoDisableObject>())
                {
                    if (PlayerShotFrom == 1)
                    {
                        GM.Player1Score += DMG;
                        GameObject.FindGameObjectWithTag("WhitePlayer").GetComponent<ShipManagement>().PlayerStats.damageDelt += DMG;
                    }
                    else if (PlayerShotFrom == 2)
                    {
                        GM.Player2Score += DMG;
                        GameObject.FindGameObjectWithTag("BlackPlayer").GetComponent<ShipManagement>().PlayerStats.damageDelt += DMG;
                    }
                }
            }


            if (collision.gameObject.name.Contains("CWIS"))
            {
                HitCWISDetection = true;
            }
            else
            {
                HitCWISDetection = false;
            }

            if (!IsLaser)
            {
                if (!collision.gameObject.GetComponent<BossCWISDetection>())
                {
                    if (ShouldDestroy)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
        

        public void SetPlayerShotFrom(int Value)
        {
            PlayerShotFrom = Value;
        }



        public int GetPlayerShotFrom()
        {
            return PlayerShotFrom;
        }


        internal IEnumerator MoveCurve(GameObject Object, Vector2 Pos0, Vector2 Pos1, Vector2 Pos2)
        {
            if (gameObject.activeInHierarchy)
            {
                while (TParam < 1)
                {
                    if (Pos0 != null && Pos1 != null && Pos2 != null)
                    {
                        TParam += Time.deltaTime / 1.5f;

                        // https://www.gamasutra.com/blogs/VivekTank/20180806/323709/How_to_work_with_Bezier_Curve_in_Games_with_Unity.php
                        // B(t) = (1-t)[pow2]P0 + 2(1-t)tP1 + t[pow2]P2
                        Vector2 NewPos = Mathf.Pow(1 - TParam, 2) * Pos0 + 2 * (1 - TParam) * TParam * Pos1 + Mathf.Pow(TParam, 2) * Pos2;

                        Object.GetComponent<Rigidbody2D>().MovePosition(NewPos);

                        Object.transform.rotation = Quaternion.LookRotation(Vector3.forward, (NewPos - (Vector2)Object.transform.position));

                        yield return new WaitForEndOfFrame();
                    }
                    else
                    {
                        if (Explosion)
                        {
                            GameObject _go = Instantiate(Explosion, transform.position, transform.rotation);
                            Destroy(_go, 1f);
                            gameObject.SetActive(false);
                        }
                    }
                }

                TParam = 0f;

                if (Explosion)
                {
                    GameObject _go = Instantiate(Explosion, transform.position, transform.rotation);
                    Destroy(_go, 1f);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
