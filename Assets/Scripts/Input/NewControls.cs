// GENERATED AUTOMATICALLY FROM 'Assets/Arcade Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace CarterGames.Arcade.UserInput
{
    public class @NewControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @NewControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Arcade Controls"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""c277b507-d4f4-4b5a-ac7f-826583253ea5"",
            ""actions"": [
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Value"",
                    ""id"": ""58641d7a-c209-4fda-8d7f-3a328b065880"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""69c814c3-e6be-4fd8-92c9-a3688e940228"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""4a391b46-becd-4568-9f48-21bcc7f9b820"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""37af3ad8-ee18-4497-a829-f55d5f6079f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button1"",
                    ""type"": ""Button"",
                    ""id"": ""57988398-e423-4553-92d4-866651d70f04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button2"",
                    ""type"": ""Button"",
                    ""id"": ""544a094c-17af-49bb-bd2a-18b2364b159b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button3"",
                    ""type"": ""Button"",
                    ""id"": ""fbdddcf4-3a80-41d4-9e18-01e0b0368c26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button4"",
                    ""type"": ""Button"",
                    ""id"": ""edb82c27-74b6-4e8f-b2c4-b2ea09338f53"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button5"",
                    ""type"": ""Button"",
                    ""id"": ""c504b9b3-4b45-4f84-a71a-c58b1388b438"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button6"",
                    ""type"": ""Button"",
                    ""id"": ""c03e7de4-1886-47cf-b640-ce240c96b98d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button7"",
                    ""type"": ""Button"",
                    ""id"": ""2e0d5725-ae6c-4bd4-8778-b81e9f9d7ff5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button8"",
                    ""type"": ""Button"",
                    ""id"": ""c9f06631-e850-481b-bd8c-f8ae4370680a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""07f21320-70fe-4c8b-bdcb-3605ccc45ae9"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39ed6ac1-fd01-4ebd-bd13-0aac7a67a35a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5898d150-4c60-49fd-bb22-073f82749952"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17d00861-01ab-42b0-8e14-3e9b4bae1a43"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6934eb4a-54ca-4e71-b64b-bf6215f779dd"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9aef29e-ba89-4fc8-9eec-0ccac970e874"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""073dfd55-9bdd-4c39-944e-35c2552a0b94"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d23f35df-e3a5-48e3-8192-8abbe2e4d773"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6132d983-9cb1-4b05-a326-2aa6cedc717d"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5d13780-a1d5-40c4-90bf-bf5037d8e8d9"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""115a25f9-acb7-4013-a3f1-88802be65bb2"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""Button2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45e07216-a368-4c61-a225-c85886046ad6"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""Button3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3b3498b-851b-45bb-9040-2f0f43c286a5"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""Button4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ec52e84-0af4-48b4-bcb4-63bbe0cf265f"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""Button5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""098bf699-0a18-4db7-a6d9-597269a134dc"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""Button6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7745c4c0-3add-4ca3-bdf6-cfc6401bac7c"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""Button7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65a29c31-2a08-4bf1-a273-56bc63d4766d"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ArcadeBoard"",
                    ""action"": ""Button8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""ArcadeBoard"",
            ""bindingGroup"": ""ArcadeBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XboxController"",
            ""bindingGroup"": ""XboxController"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Controls
            m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
            m_Controls_MoveLeft = m_Controls.FindAction("MoveLeft", throwIfNotFound: true);
            m_Controls_MoveRight = m_Controls.FindAction("MoveRight", throwIfNotFound: true);
            m_Controls_MoveUp = m_Controls.FindAction("MoveUp", throwIfNotFound: true);
            m_Controls_MoveDown = m_Controls.FindAction("MoveDown", throwIfNotFound: true);
            m_Controls_Button1 = m_Controls.FindAction("Button1", throwIfNotFound: true);
            m_Controls_Button2 = m_Controls.FindAction("Button2", throwIfNotFound: true);
            m_Controls_Button3 = m_Controls.FindAction("Button3", throwIfNotFound: true);
            m_Controls_Button4 = m_Controls.FindAction("Button4", throwIfNotFound: true);
            m_Controls_Button5 = m_Controls.FindAction("Button5", throwIfNotFound: true);
            m_Controls_Button6 = m_Controls.FindAction("Button6", throwIfNotFound: true);
            m_Controls_Button7 = m_Controls.FindAction("Button7", throwIfNotFound: true);
            m_Controls_Button8 = m_Controls.FindAction("Button8", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Controls
        private readonly InputActionMap m_Controls;
        private IControlsActions m_ControlsActionsCallbackInterface;
        private readonly InputAction m_Controls_MoveLeft;
        private readonly InputAction m_Controls_MoveRight;
        private readonly InputAction m_Controls_MoveUp;
        private readonly InputAction m_Controls_MoveDown;
        private readonly InputAction m_Controls_Button1;
        private readonly InputAction m_Controls_Button2;
        private readonly InputAction m_Controls_Button3;
        private readonly InputAction m_Controls_Button4;
        private readonly InputAction m_Controls_Button5;
        private readonly InputAction m_Controls_Button6;
        private readonly InputAction m_Controls_Button7;
        private readonly InputAction m_Controls_Button8;
        public struct ControlsActions
        {
            private @NewControls m_Wrapper;
            public ControlsActions(@NewControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveLeft => m_Wrapper.m_Controls_MoveLeft;
            public InputAction @MoveRight => m_Wrapper.m_Controls_MoveRight;
            public InputAction @MoveUp => m_Wrapper.m_Controls_MoveUp;
            public InputAction @MoveDown => m_Wrapper.m_Controls_MoveDown;
            public InputAction @Button1 => m_Wrapper.m_Controls_Button1;
            public InputAction @Button2 => m_Wrapper.m_Controls_Button2;
            public InputAction @Button3 => m_Wrapper.m_Controls_Button3;
            public InputAction @Button4 => m_Wrapper.m_Controls_Button4;
            public InputAction @Button5 => m_Wrapper.m_Controls_Button5;
            public InputAction @Button6 => m_Wrapper.m_Controls_Button6;
            public InputAction @Button7 => m_Wrapper.m_Controls_Button7;
            public InputAction @Button8 => m_Wrapper.m_Controls_Button8;
            public InputActionMap Get() { return m_Wrapper.m_Controls; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
            public void SetCallbacks(IControlsActions instance)
            {
                if (m_Wrapper.m_ControlsActionsCallbackInterface != null)
                {
                    @MoveLeft.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveLeft;
                    @MoveLeft.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveLeft;
                    @MoveLeft.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveLeft;
                    @MoveRight.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveRight;
                    @MoveRight.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveRight;
                    @MoveRight.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveRight;
                    @MoveUp.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveUp;
                    @MoveUp.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveUp;
                    @MoveUp.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveUp;
                    @MoveDown.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveDown;
                    @MoveDown.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveDown;
                    @MoveDown.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMoveDown;
                    @Button1.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton1;
                    @Button1.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton1;
                    @Button1.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton1;
                    @Button2.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton2;
                    @Button2.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton2;
                    @Button2.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton2;
                    @Button3.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton3;
                    @Button3.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton3;
                    @Button3.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton3;
                    @Button4.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton4;
                    @Button4.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton4;
                    @Button4.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton4;
                    @Button5.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton5;
                    @Button5.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton5;
                    @Button5.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton5;
                    @Button6.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton6;
                    @Button6.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton6;
                    @Button6.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton6;
                    @Button7.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton7;
                    @Button7.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton7;
                    @Button7.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton7;
                    @Button8.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton8;
                    @Button8.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton8;
                    @Button8.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnButton8;
                }
                m_Wrapper.m_ControlsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MoveLeft.started += instance.OnMoveLeft;
                    @MoveLeft.performed += instance.OnMoveLeft;
                    @MoveLeft.canceled += instance.OnMoveLeft;
                    @MoveRight.started += instance.OnMoveRight;
                    @MoveRight.performed += instance.OnMoveRight;
                    @MoveRight.canceled += instance.OnMoveRight;
                    @MoveUp.started += instance.OnMoveUp;
                    @MoveUp.performed += instance.OnMoveUp;
                    @MoveUp.canceled += instance.OnMoveUp;
                    @MoveDown.started += instance.OnMoveDown;
                    @MoveDown.performed += instance.OnMoveDown;
                    @MoveDown.canceled += instance.OnMoveDown;
                    @Button1.started += instance.OnButton1;
                    @Button1.performed += instance.OnButton1;
                    @Button1.canceled += instance.OnButton1;
                    @Button2.started += instance.OnButton2;
                    @Button2.performed += instance.OnButton2;
                    @Button2.canceled += instance.OnButton2;
                    @Button3.started += instance.OnButton3;
                    @Button3.performed += instance.OnButton3;
                    @Button3.canceled += instance.OnButton3;
                    @Button4.started += instance.OnButton4;
                    @Button4.performed += instance.OnButton4;
                    @Button4.canceled += instance.OnButton4;
                    @Button5.started += instance.OnButton5;
                    @Button5.performed += instance.OnButton5;
                    @Button5.canceled += instance.OnButton5;
                    @Button6.started += instance.OnButton6;
                    @Button6.performed += instance.OnButton6;
                    @Button6.canceled += instance.OnButton6;
                    @Button7.started += instance.OnButton7;
                    @Button7.performed += instance.OnButton7;
                    @Button7.canceled += instance.OnButton7;
                    @Button8.started += instance.OnButton8;
                    @Button8.performed += instance.OnButton8;
                    @Button8.canceled += instance.OnButton8;
                }
            }
        }
        public ControlsActions @Controls => new ControlsActions(this);
        private int m_ArcadeBoardSchemeIndex = -1;
        public InputControlScheme ArcadeBoardScheme
        {
            get
            {
                if (m_ArcadeBoardSchemeIndex == -1) m_ArcadeBoardSchemeIndex = asset.FindControlSchemeIndex("ArcadeBoard");
                return asset.controlSchemes[m_ArcadeBoardSchemeIndex];
            }
        }
        private int m_XboxControllerSchemeIndex = -1;
        public InputControlScheme XboxControllerScheme
        {
            get
            {
                if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("XboxController");
                return asset.controlSchemes[m_XboxControllerSchemeIndex];
            }
        }
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        public interface IControlsActions
        {
            void OnMoveLeft(InputAction.CallbackContext context);
            void OnMoveRight(InputAction.CallbackContext context);
            void OnMoveUp(InputAction.CallbackContext context);
            void OnMoveDown(InputAction.CallbackContext context);
            void OnButton1(InputAction.CallbackContext context);
            void OnButton2(InputAction.CallbackContext context);
            void OnButton3(InputAction.CallbackContext context);
            void OnButton4(InputAction.CallbackContext context);
            void OnButton5(InputAction.CallbackContext context);
            void OnButton6(InputAction.CallbackContext context);
            void OnButton7(InputAction.CallbackContext context);
            void OnButton8(InputAction.CallbackContext context);
        }
    }
}
