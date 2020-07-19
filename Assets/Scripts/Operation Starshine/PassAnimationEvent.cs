using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Starshine
{
    public class PassAnimationEvent : MonoBehaviour
    {
        public void PassCanShootToDrones()
        {
            for (int i = 0; i < GetComponentsInChildren<Enemies>().Length; i++)
            {
                GetComponentsInChildren<Enemies>()[i].ShouldShoot = true;
                GetComponentsInChildren<Enemies>()[i].CanShootMain = true;
                GetComponentsInChildren<Enemies>()[i].CanShootAlt = true;
            }
        }

        public void PassCantShoot()
        {
            for (int i = 0; i < GetComponentsInChildren<Enemies>().Length; i++)
            {
                GetComponentsInChildren<Enemies>()[i].ShouldShoot = false;
                GetComponentsInChildren<Enemies>()[i].CanShootMain = false;
                GetComponentsInChildren<Enemies>()[i].CanShootAlt = false;
            }
        }


        public void DisableRocketandReset()
        {
            gameObject.SetActive(false);
        }
    }
}