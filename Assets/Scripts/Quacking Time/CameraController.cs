///*
//*  Copyright (c) Jonathan Carter
//*  E: jonathan@carter.games
//*  W: https://jonathan.carter.games/
//*/

using UnityEngine;

namespace CarterGames.QuackingTime
{
    public class CameraController : MonoBehaviour
    {
        //        [SerializeField] private bool isHittingWall;
        //        [SerializeField] private int numberOfCollisions;
        //        [SerializeField] private Vector3 initialVector;

        //        private RaycastHit hit;

        //        public int minZ;
        //        public int moveSpd;

        //        public Transform target;

        //        private void Start()
        //        {
        //            initialVector = transform.localPosition;
        //        }


        //        private void Update()
        //        {

        //            //Vector3 postest = (-transform.up).normalized;

        //            Debug.DrawLine(target.transform.position, (transform.position -  target.transform.position) + transform.position, Color.green);
        //            Debug.DrawLine(target.transform.position, target.transform.position, Color.red);
        //            Vector3 pos = (transform.position - target.transform.position) *.5f + transform.position;

        //            if (numberOfCollisions > 0)
        //            {
        //                if (Physics.Raycast(transform.position, pos, out hit))
        //                {
        //                    isHittingWall = true;
        //                }
        //            }
        //            else if (numberOfCollisions == 0)
        //            {
        //                isHittingWall = false;
        //            }


        //            if (isHittingWall)
        //            {
        //                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (moveSpd / 2) * Time.deltaTime, transform.localPosition.z + moveSpd * Time.deltaTime);
        //            }
        //            else
        //            {
        //                if (Physics.Raycast(transform.position, pos, out hit))
        //                {

        //                }
        //                else
        //                {
        //                    if (transform.localPosition.y < initialVector.y)
        //                    {
        //                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (moveSpd / 2) * Time.deltaTime, transform.localPosition.z);
        //                    }

        //                    if (transform.localPosition.z > initialVector.z)
        //                    {
        //                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - moveSpd * Time.deltaTime);
        //                    }
        //                }
        //            }
        //        }


        //        private void LateUpdate()
        //        {
        //            //transform.LookAt(target);
        //        }


        //        private void OnTriggerEnter(Collider other)
        //        {
        //            Debug.Log("Hit");

        //            if (other.gameObject.layer == LayerMask.NameToLayer("Quacking:Ground"))
        //            {
        //                Debug.Log("Hit>>>");
        //                numberOfCollisions++;
        //            }
        //        }

        //        private void OnTriggerExit(Collider other)
        //        {
        //            numberOfCollisions--;
        //        }
    }
}