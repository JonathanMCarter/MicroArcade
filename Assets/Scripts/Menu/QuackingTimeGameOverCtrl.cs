using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Arcade.Menu
{
    [RequireComponent(typeof(MenuSystem))]
    public class QuackingTimeGameOverCtrl : MenuSystem
    {
        public List<Image> EndOptions;
        public List<Color> Colours;

        protected override void Start()
        {
            base.Start();
            maxPos = EndOptions.Count-1;
            UpdateDisplay();
        }


        protected override void Update()
        {
            base.Update();
            MoveLR();
            if (ValueChanged()) { UpdateDisplay(); }
            if (Confirm()) { EndOptions[pos].GetComponent<Button>().onClick.Invoke(); }
        }


        void UpdateDisplay()
        {
            for (int i = 0; i < EndOptions.Count; i++)
            {
                if (pos == i && EndOptions[i].color != Colours[0])
                {
                    EndOptions[i].GetComponentInChildren<Text>().color = Colours[0];
                }
                else if (pos != i && EndOptions[i].color != Colours[1])
                {
                    EndOptions[i].GetComponentInChildren<Text>().color = Colours[1];
                }
            }
        }
    }
}