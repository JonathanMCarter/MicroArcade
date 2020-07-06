using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starshine
{
    public class ScrollBackground : MonoBehaviour
    {
        public Vector2 ScrollXY;

        void Update()
        {
            GetComponent<Renderer>().material.SetTextureOffset("_BaseMap", ScrollXY * Time.deltaTime);
        }
    }
}