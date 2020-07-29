using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Starshine
{
    public class BossScript : MonoBehaviour
    {

        public enum BossStages
        {
            Intro,
            Attack1,
            Attack2,
            Attack3,
            Attack4,
        };

        public BossStages Stages;
        public ShipManagement.ShieldTypes ActiveShieldType;
        public ShieldColours Colours;

        public EnemyWeapons EmWeapons;

        public List<WeaponStats> BossWeapons;
        public ShipStats Boss;
        public int Health, Shield;
        public float MoveSpd;

        public List<GameObject> CWISTargets;
        public int CWISSalvoAmount;
        public bool CWIS_Active;
        public GameObject CWIS_Turret;

        public List<Transform> Attack1SpawnPoints;
        public List<Transform> Attack2SpawnPoints;
        public List<Transform> Attack3SpawnPoints;

        public int NumberOfBoltsToShoot;
        public int NumberOfMissilesToShoot;

        public List<GameObject> Lasers;

        public bool ChooseNewAttack;
        public bool AttackChosen;
        public bool BossIntroComplete;
        public bool HasSelectedTarget;
        public bool CanShootAttack1 = true;
        public bool CanShootAttack2 = true;
        public bool CanShootAttack3 = true;
        public bool CanShootAttack4 = true;
        public Rigidbody2D RB;

        public StageController SC;
        GameManager GM;
        public AudioManager am;



        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            SC = FindObjectOfType<StageController>();

            GM = FindObjectOfType<GameManager>();

            RB.AddForce(Vector2.down * MoveSpd);

            Health = Boss.Health[0];
            Shield = Boss.Shield[0];

            EmWeapons = FindObjectOfType<EnemyWeapons>();

            GetComponent<Animator>().enabled = false;
        }



        private void Update()
        {

            if (GM && !am)
            {
                am = GM.GetComponent<DialogueScript>().am;
            }


            // if alive
            if (AmLiving())
            {
                // Shield Active
                if (Shield > 0)
                {
                    if (!transform.GetChild(transform.childCount-1).gameObject.activeSelf)
                    {
                        transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (transform.GetChild(transform.childCount - 1).gameObject.activeSelf)
                    {
                        transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
                    }
                }


                // Move the boss into position
                if ((transform.position.y < 6f) && (!BossIntroComplete))
                {
                    RB.velocity = Vector2.zero;
                    ChooseNewAttack = true;
                    BossIntroComplete = true;
                }

                // Choose a new move to attack with
                if (ChooseNewAttack)
                {
                    AttackChosen = false;
                    StartCoroutine(CycleAttacks(Random.Range(3, 8)));
                }


                // CWIS
                if ((!CWIS_Active) && (CWISTargets.Count >= 1))
                {
                    StartCoroutine(CWIS_Delay());
                }


                // What each attack should call
                switch (Stages)
                {
                    case BossStages.Attack1:

                        if (CanShootAttack1)
                        {
                            StartCoroutine(Attack1());
                        }

                        break;
                    case BossStages.Attack2:

                        if (CanShootAttack2)
                        {
                            StartCoroutine(Attack2());
                        }

                        break;
                    case BossStages.Attack3:

                        if (CanShootAttack3)
                        {
                            StartCoroutine(Attack3());
                        }

                        break;
                    case BossStages.Attack4:

                        if (CanShootAttack4)
                        {
                            StartCoroutine(Attack4());
                        }

                        break;
                }
            }
            else
            {
                StartCoroutine(GameOverCo());
            }
        }




        bool AmLiving()
        {
            if (Health < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        IEnumerator CycleAttacks(float DelayBeforeNextAttack)
        {
            ChooseNewAttack = false;

            if (!AttackChosen)
            {
                Stages = ChooseAttackToUse();
                AttackChosen = true;
            }

            yield return new WaitForSeconds(DelayBeforeNextAttack);
            ChooseNewAttack = true;
        }


        BossStages ChooseAttackToUse()
        {
            int Rand = Random.Range(0, 100);

            if (Rand < 45)
            {
                return BossStages.Attack1;
            }
            else if (Rand > 45 && Rand < 55)
            {
                return BossStages.Attack2;
            }
            else if (Rand > 55 && Rand < 80)
            {
                return BossStages.Attack3;
            }
            else
            {
                return BossStages.Attack4;
            }
        }



        IEnumerator Attack1()
        {
            CanShootAttack1 = false;

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < NumberOfBoltsToShoot; i++)
            {
                GameObject Go = EmWeapons.GetBossBolt();
                Go.transform.position = ChoosePosition(Attack1SpawnPoints).position;
                Go.SetActive(true);

                Go.GetComponent<Damage>().DMG = (int)BossWeapons[0].Damage[0];
                Go.GetComponent<Rigidbody2D>().velocity += ChooseDirection() * 10;
                am.Play("Shoot", Random.Range(.04f, .1f), Random.Range(.9f, 1f));
            }

            yield return new WaitForSeconds(BossWeapons[0].Delay);
            CanShootAttack1 = true;
        }

        Vector2 ChooseDirection()
        {
            return new Vector2(Random.Range(-.8f, .8f), Random.Range(-.5f, -1f));
        }


        IEnumerator Attack2()
        {
            CanShootAttack2 = false;

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < NumberOfMissilesToShoot; i++)
            {
                GameObject Go = EmWeapons.GetMissile();
                Go.GetComponent<MissileScript>().ShouldFindTarget = true;
                Go.transform.position = ChoosePosition(Attack2SpawnPoints).position;
                Go.SetActive(true);
                Go.GetComponent<Damage>().DMG = (int)BossWeapons[1].Damage[0];
                am.Play("MisslieShoot", .25f);
            }

           
            yield return new WaitForSeconds(BossWeapons[1].Delay);

            Stages = ChooseAttackToUse();

            CanShootAttack2 = true;
        }


        Transform ChoosePosition(List<Transform> List)
        {
            return List[Random.Range(0, List.Count)];
        }


        IEnumerator Attack3()
        {
            CanShootAttack3 = false;

            am.Play("LaserPowerUp", .4f);
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < Lasers.Count; i++)
            {
                Lasers[i].SetActive(true);
                Lasers[i].GetComponent<ParticleSystem>().Play();
            }


            yield return new WaitForSeconds(.5f);

            for (int i = 0; i < Lasers.Count; i++)
            {
                Lasers[i].GetComponent<BoxCollider2D>().enabled = true;
            }

            am.Play("LaserShoot", .4f);
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < Lasers.Count; i++)
            {
                Lasers[i].GetComponent<BoxCollider2D>().enabled = false;
                Lasers[i].SetActive(false);
            }

            Stages = ChooseAttackToUse();

            CanShootAttack3 = true;
        }


        IEnumerator Attack4()
        {
            CanShootAttack4 = false;

            yield return new WaitForSeconds(2f);

            // Sheild Change
            ActiveShieldType = (ShipManagement.ShieldTypes)Random.Range(0, 4);
            UpdateShieldColours();

            Stages = ChooseAttackToUse();

            CanShootAttack4 = true;
        }



        private IEnumerator CWIS_Delay()
        {
            for (int i = 0; i < CWISSalvoAmount; i++)
            {
                StartCoroutine(CWIS_Shoot());
                yield return new WaitForSeconds(.1f);
            }

            CWISTargets.RemoveAt(0);
            CWIS_Active = false;
        }


        private IEnumerator CWIS_Shoot()
        {
            CWIS_Active = true;

            CWIS_Turret.transform.rotation = Quaternion.LookRotation(Vector3.forward, (CWIS_Turret.transform.position - CWISTargets[0].transform.position));

            if (!EmWeapons)
            {
                EmWeapons = FindObjectOfType<EnemyWeapons>();
            }

            GameObject newBolt = EmWeapons.GetBossBolt();

            newBolt.SetActive(true);
            newBolt.transform.position = transform.GetChild(0).transform.position;
            newBolt.transform.rotation = Quaternion.LookRotation(Vector3.forward, (CWISTargets[0].transform.position - newBolt.transform.position));

            newBolt.GetComponent<Rigidbody2D>().velocity = (CWISTargets[0].transform.GetChild(0).transform.position - newBolt.transform.position).normalized * 30;

            yield return new WaitForSeconds(.1f);
        }


        private void UpdateShieldColours()
        {
            switch (ActiveShieldType)
            {
                case ShipManagement.ShieldTypes.Bolts:
                    transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().enabled = true;
                    transform.GetChild(transform.childCount - 1).GetComponent<Renderer>().material.SetColor("shieldColor", Colours.BoltShieldColour);
                    break;
                case ShipManagement.ShieldTypes.Missiles:
                    transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().enabled = true;
                    transform.GetChild(transform.childCount - 1).GetComponent<Renderer>().material.SetColor("shieldColor", Colours.MissileShieldColour);
                    break;
                default:
                    transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().enabled = false;
                    break;
            }
        }


        private IEnumerator GameOverCo()
        {
            // Anim for boss death
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().SetTrigger("isDead");
            yield return new WaitForSeconds(3f);
            // if boss dead - players win
            GM.SetVictory();
        }
    }
}