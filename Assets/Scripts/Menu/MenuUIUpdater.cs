using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Arcade.Menu
{
    public class MenuUIUpdater : MonoBehaviour
    {

        public enum Elements
        {
            BootScreenBGColourChanger,
            SettingsInputPlayer1,
            SettingsInputPlayer2,
        };



        public Elements Elly;
        [Header("Boot Screen BG Options")]
        public float ChangeSlowness;
        public CanvasGroup GameView;
        public List<Image> ControlIcons;


        private void Start()
        {
            if (Elly == (Elements.SettingsInputPlayer1 | Elements.SettingsInputPlayer2))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    ControlIcons.Add(transform.GetChild(i).GetComponent<Image>());
                }
            }
        }

        private void Update()
        {
            if (gameObject.activeInHierarchy)
            {
                switch (Elly)
                {
                    case Elements.BootScreenBGColourChanger:

                        float Hue, H, S, V;
                        Color.RGBToHSV(GetComponent<Image>().color, out H, out S, out V);
                        Hue = H;

                        if (Hue == 360)
                        {
                            Hue = 0;
                        }
                        else
                        {
                            Hue += Time.deltaTime / ChangeSlowness;
                        }

                        GetComponent<Image>().color = Color.HSVToRGB(Hue, S, V);

                        break;
                    case Elements.SettingsInputPlayer1:

                        switch (InputSettings.ControllerType)
                        {
                            case SupportedControllers.ArcadeBoard:
                                UpdateControllerIcon(0);
                                break;
                            case SupportedControllers.GamePadBoth:
                                UpdateControllerIcon(1);
                                break;
                            case SupportedControllers.KeyboardBoth:
                                UpdateControllerIcon(2);
                                break;
                            case SupportedControllers.KeyboardP1ControllerP2:
                                UpdateControllerIcon(2);
                                break;
                            case SupportedControllers.KeyboardP2ControllerP1:
                                UpdateControllerIcon(1);
                                break;
                            default:
                                break;
                        }

                        break;
                    case Elements.SettingsInputPlayer2:

                        switch (InputSettings.ControllerType)
                        {
                            case SupportedControllers.ArcadeBoard:
                                UpdateControllerIcon(0);
                                break;
                            case SupportedControllers.GamePadBoth:
                                UpdateControllerIcon(1);
                                break;
                            case SupportedControllers.KeyboardBoth:
                                UpdateControllerIcon(2);
                                break;
                            case SupportedControllers.KeyboardP1ControllerP2:
                                UpdateControllerIcon(1);
                                break;
                            case SupportedControllers.KeyboardP2ControllerP1:
                                UpdateControllerIcon(2);
                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        void UpdateControllerIcon(int WantedIcon)
        {
            for (int i = 0; i < ControlIcons.Count; i++)
            {
                if (i == WantedIcon)
                {
                    if (!ControlIcons[i].enabled)
                    {
                        ControlIcons[i].enabled = true;
                    }
                }
                else if (ControlIcons[i].enabled)
                {
                    ControlIcons[i].enabled = false;
                }
            }
        }
    }
}