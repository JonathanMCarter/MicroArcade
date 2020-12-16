// GENERATED AUTOMATICALLY FROM 'Assets/Actions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace CarterGames.Arcade
{
    public class @Actions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Actions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""Menu"",
            ""id"": ""e4e34caa-f31b-47b5-8deb-2825120d030f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""71ff7fde-cc4d-4523-ae55-3ce537b36ca1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Value"",
                    ""id"": ""a8792439-b060-45f5-955d-b4953c079641"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Value"",
                    ""id"": ""203ac372-bb09-44e4-a65b-87a5f7cee952"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Controller/Joystick"",
                    ""id"": ""f6ee3627-a976-4d90-9cad-59c23936b8eb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0e49d5aa-cb15-4393-a031-cddbf62980f0"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3e740db8-6e95-422c-8e14-273dfb9d7b4c"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ad27a225-2397-4f8f-aaef-2a4a0934c8ee"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1fb8f213-5af1-460e-a3d4-b46ba65c9c95"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard-WASD"",
                    ""id"": ""4f649ac3-1936-4707-9f10-e2cfb45cd505"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e229c514-bde8-474d-80ab-c5b80a7c95dd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3ec45dca-4eab-4858-919d-4d8b5592146b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dbb70a3f-7ac0-4a68-a7af-416e78a351ca"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e173f703-97c9-423f-b797-beb3cc183ffb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard-ArrowKeys"",
                    ""id"": ""10420a36-264f-4297-86f0-c889e03e0f52"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""97ebcd47-2aac-4b08-ba2c-4232157f61dc"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""84f9dd02-f1c3-4953-8150-468c49598794"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d056b958-8fc3-49a6-b124-dc40c7f2331a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3b9c64b0-c514-4a83-9c8b-960f1a985161"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arcade"",
                    ""id"": ""f69d857b-2408-4b78-b93d-a4e0ef2c7c98"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""109b37be-8619-446a-bca7-a8de8cfba442"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Arcade"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""44962a25-9582-482d-bba0-d0de051ade18"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Arcade"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f37e51d1-efe5-41d9-a454-959bb7b50336"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Arcade"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""17904f24-7881-47ec-8b33-e5197ccfbb04"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Arcade"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""847ad1e3-46de-4f25-88e8-0d049b8f823d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4431d0e-330b-4953-8638-a7f7388a1634"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18e69cb8-1747-4834-afd6-41c4291f9962"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf7a700c-16d9-40f6-915f-f08907aa637c"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Arcade"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a14deb09-c05f-4603-8285-f975776fee91"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41eb6de4-1a50-4f3e-92b3-443941e174c6"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5eae152-cdf4-4fac-91da-4b50f93412ce"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7ae0ef1-5d44-4bc1-8104-f08d318266db"",
                    ""path"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >/button7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Arcade"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": true,
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
        },
        {
            ""name"": ""Arcade"",
            ""bindingGroup"": ""Arcade"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Menu
            m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
            m_Menu_Movement = m_Menu.FindAction("Movement", throwIfNotFound: true);
            m_Menu_Accept = m_Menu.FindAction("Accept", throwIfNotFound: true);
            m_Menu_Cancel = m_Menu.FindAction("Cancel", throwIfNotFound: true);
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

        // Menu
        private readonly InputActionMap m_Menu;
        private IMenuActions m_MenuActionsCallbackInterface;
        private readonly InputAction m_Menu_Movement;
        private readonly InputAction m_Menu_Accept;
        private readonly InputAction m_Menu_Cancel;
        public struct MenuActions
        {
            private @Actions m_Wrapper;
            public MenuActions(@Actions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Menu_Movement;
            public InputAction @Accept => m_Wrapper.m_Menu_Accept;
            public InputAction @Cancel => m_Wrapper.m_Menu_Cancel;
            public InputActionMap Get() { return m_Wrapper.m_Menu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
            public void SetCallbacks(IMenuActions instance)
            {
                if (m_Wrapper.m_MenuActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMovement;
                    @Accept.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                    @Accept.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                    @Accept.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                    @Cancel.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @Cancel.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @Cancel.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                }
                m_Wrapper.m_MenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Accept.started += instance.OnAccept;
                    @Accept.performed += instance.OnAccept;
                    @Accept.canceled += instance.OnAccept;
                    @Cancel.started += instance.OnCancel;
                    @Cancel.performed += instance.OnCancel;
                    @Cancel.canceled += instance.OnCancel;
                }
            }
        }
        public MenuActions @Menu => new MenuActions(this);
        private int m_ControllerSchemeIndex = -1;
        public InputControlScheme ControllerScheme
        {
            get
            {
                if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
                return asset.controlSchemes[m_ControllerSchemeIndex];
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
        private int m_ArcadeSchemeIndex = -1;
        public InputControlScheme ArcadeScheme
        {
            get
            {
                if (m_ArcadeSchemeIndex == -1) m_ArcadeSchemeIndex = asset.FindControlSchemeIndex("Arcade");
                return asset.controlSchemes[m_ArcadeSchemeIndex];
            }
        }
        public interface IMenuActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnAccept(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
        }
    }
}
