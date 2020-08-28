using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Starshine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MissileScript : MonoBehaviour
    {
        public enum Directions
        {
            None,
            Up,
            Down,
        };

        public enum Targets
        {
            None,
            Enemies,
            Players,
        };

        public Directions MissileDirection;
        public Targets ShouldTarget;
        public GameObject Target;
        public float MoveSpd, RotSpd;
        public Rigidbody2D RB;
        public List<GameObject> TargetList;
        public bool ShouldFindTarget;
        public bool ShouldGoFoward = true;
        public bool IsCoRunning;
        public GameObject ClosestEnemy;
        TrailRenderer TR;
        internal bool isRocketAngel;


        private void OnEnable()
        { 
            if (!isRocketAngel)
            {
                TargetList.Clear();

                if (TR)
                {
                    TR.enabled = true;
                }

                if (ShouldTarget == Targets.Enemies)
                {
                    ShouldGoFoward = true;
                }

                GetComponent<Rigidbody2D>().angularVelocity = 0f;

                if (ShouldTarget == Targets.Enemies)
                {
                    transform.rotation = Quaternion.identity;
                }
            }
        }


        private void OnDisable()
        {
            if (!isRocketAngel)
            {
                if (TR)
                {
                    TR.Clear();
                    TR.enabled = false;
                }

                IsCoRunning = false;
                StopAllCoroutines();
            }

            StopAllCoroutines();
        }


        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            TR = GetComponent<TrailRenderer>();
        }


        private void Update()
        {
            if (!isRocketAngel)
            {
                if (ShouldFindTarget)
                {
                    Target = FindTarget();
                    ShouldFindTarget = false;
                }
            }
        }



        private void FixedUpdate()
        {
            if (!isRocketAngel)
            {
                if ((Target) && (Target.activeInHierarchy))
                {
                    if (!ShouldGoFoward)
                    {
                        Vector2 Dir = (Vector2)Target.transform.position - RB.position;

                        Dir.Normalize();

                        float RotateAmount = Vector3.Cross(Dir, transform.up * MoveSpd).z;

                        RB.angularVelocity = -RotateAmount * RotSpd;

                        RB.velocity = GetDirection() * MoveSpd;
                    }
                    else
                    {
                        RB.velocity = GetDirection() * MoveSpd;

                        if ((ShouldGoFoward) && (!IsCoRunning))
                        {
                            StartCoroutine(WaitToTurnToTarget());
                        }
                    }
                }
                else
                {
                    ShouldFindTarget = true;
                    RB.angularVelocity = 0;
                }
            }
            else
            {
                if ((Target) && (!Target.activeInHierarchy))
                {
                    Vector2 Dir = (Vector2)Target.transform.position - RB.position;

                    Dir.Normalize();

                    float RotateAmount = Vector3.Cross(Dir, transform.up * MoveSpd).z;

                    RB.angularVelocity = -RotateAmount * RotSpd;

                    RB.velocity = GetDirection() * MoveSpd;
                }
            }
        }



        IEnumerator WaitToTurnToTarget()
        {
            Debug.Log("Missile Co Running");
            IsCoRunning = true;
            yield return new WaitForSeconds(.5f);
            ShouldGoFoward = false;
            IsCoRunning = false;
        }



        Vector2 GetDirection()
        {
            switch (MissileDirection)
            {
                case Directions.Up:
                    return transform.up;
                case Directions.Down:
                    return transform.up;
                default:
                    return transform.up;
            }
        }


        GameObject FindTarget()
        {
            float DistanceToClosestEnemy = Mathf.Infinity;


            switch (ShouldTarget)
            {
                case Targets.Enemies:


                    if (FindObjectOfType<Enemies>())
                    {
                        Debug.Log(FindObjectsOfType<Enemies>().Length);

                        // Find all targets in scene
                        for (int i = 0; i < FindObjectsOfType<Enemies>().Length; i++)
                        {
                            TargetList.Add(FindObjectsOfType<Enemies>()[i].gameObject);
                        }

                        // Determine which target is nearest to the shooter
                        for (int i = 0; i < TargetList.Count; i++)
                        {
                            float DistanceToTarget = (TargetList[i].transform.position - transform.position).sqrMagnitude;

                            if (DistanceToTarget < DistanceToClosestEnemy)
                            {
                                DistanceToClosestEnemy = DistanceToTarget;
                                ClosestEnemy = TargetList[i].gameObject;
                            }
                        }
                    }
                    else if (FindObjectOfType<BossScript>())
                    {
                        ClosestEnemy = FindObjectOfType<BossScript>().gameObject;
                    }


                    Debug.Log("Got Enemy");
                    Debug.Log(ClosestEnemy.name);
                    return ClosestEnemy;
                case Targets.Players:


                    if (FindObjectOfType<PlayerController>())
                    {
                        Debug.Log(FindObjectsOfType<PlayerController>().Length);

                        // Find all targets in scene
                        for (int i = 0; i < FindObjectsOfType<PlayerController>().Length; i++)
                        {
                            TargetList.Add(FindObjectsOfType<PlayerController>()[i].gameObject);
                        }

                        // Determine which target is nearest to the shooter
                        for (int i = 0; i < TargetList.Count; i++)
                        {
                            float DistanceToTarget = (TargetList[i].transform.position - transform.position).sqrMagnitude;

                            if (DistanceToTarget < DistanceToClosestEnemy)
                            {
                                DistanceToClosestEnemy = DistanceToTarget;
                                ClosestEnemy = TargetList[i].gameObject;
                            }
                        }
                    }

                    Debug.Log("Got Player");
                    Debug.Log(ClosestEnemy.name);
                    return ClosestEnemy;
                default:
                    Debug.Log("Got Nothing");
                    return null;
            }
        }
    }
}