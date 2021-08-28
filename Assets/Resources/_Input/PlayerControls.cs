// GENERATED AUTOMATICALLY FROM 'Assets/Resources/_Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""KeyboardAndMouse"",
            ""id"": ""2a306a0c-10cc-47a7-8a71-9767007d21b3"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""3a58deb9-e253-49d8-9b70-93f5593e3c06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Button"",
                    ""id"": ""4a897560-aa53-4d1b-acb4-603fe0920a18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Approaching"",
                    ""type"": ""Value"",
                    ""id"": ""4d865ca7-6cdb-4855-9500-9d7846747784"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pausing"",
                    ""type"": ""Button"",
                    ""id"": ""5fb3f8fa-72a4-4138-9c78-c8a7c61aa420"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""decf3eff-f96e-46af-bfa3-7bfc16fed6e8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9d1eb8a4-5d51-46c2-96de-6ca4bd6ce3fd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""87205e5e-80ab-432d-b765-39a9a59fdbbf"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3cf4afa6-b33f-4e0a-975d-6c2b0fea04e0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""b2e10410-bd82-481a-9331-770c68df6f0f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""down"",
                    ""id"": ""79354a49-8ac7-4b9c-8adb-d47063c1a351"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""42f37076-bbbf-4c2f-94a6-14d801c88e82"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""56ad5a08-b51c-4f2f-8cde-a4f74a75acfa"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b5d550da-4f9d-4d17-a74d-9e9e2b54913a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ed86e80-ba57-4294-abcc-4dbf0387d7ea"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e807b78-34d8-4e7a-a2cd-264f0f555c20"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b4dca6e-1320-46a3-9bf8-9cbd512f9414"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Approaching"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0875c5e1-44a9-4b3b-a109-cba737b10452"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Pausing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardAndMouse"",
            ""bindingGroup"": ""KeyboardAndMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // KeyboardAndMouse
        m_KeyboardAndMouse = asset.FindActionMap("KeyboardAndMouse", throwIfNotFound: true);
        m_KeyboardAndMouse_Movement = m_KeyboardAndMouse.FindAction("Movement", throwIfNotFound: true);
        m_KeyboardAndMouse_Rotation = m_KeyboardAndMouse.FindAction("Rotation", throwIfNotFound: true);
        m_KeyboardAndMouse_Approaching = m_KeyboardAndMouse.FindAction("Approaching", throwIfNotFound: true);
        m_KeyboardAndMouse_Pausing = m_KeyboardAndMouse.FindAction("Pausing", throwIfNotFound: true);
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

    // KeyboardAndMouse
    private readonly InputActionMap m_KeyboardAndMouse;
    private IKeyboardAndMouseActions m_KeyboardAndMouseActionsCallbackInterface;
    private readonly InputAction m_KeyboardAndMouse_Movement;
    private readonly InputAction m_KeyboardAndMouse_Rotation;
    private readonly InputAction m_KeyboardAndMouse_Approaching;
    private readonly InputAction m_KeyboardAndMouse_Pausing;
    public struct KeyboardAndMouseActions
    {
        private @PlayerControls m_Wrapper;
        public KeyboardAndMouseActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_KeyboardAndMouse_Movement;
        public InputAction @Rotation => m_Wrapper.m_KeyboardAndMouse_Rotation;
        public InputAction @Approaching => m_Wrapper.m_KeyboardAndMouse_Approaching;
        public InputAction @Pausing => m_Wrapper.m_KeyboardAndMouse_Pausing;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardAndMouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardAndMouseActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardAndMouseActions instance)
        {
            if (m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnMovement;
                @Rotation.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnRotation;
                @Approaching.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnApproaching;
                @Approaching.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnApproaching;
                @Approaching.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnApproaching;
                @Pausing.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnPausing;
                @Pausing.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnPausing;
                @Pausing.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnPausing;
            }
            m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Approaching.started += instance.OnApproaching;
                @Approaching.performed += instance.OnApproaching;
                @Approaching.canceled += instance.OnApproaching;
                @Pausing.started += instance.OnPausing;
                @Pausing.performed += instance.OnPausing;
                @Pausing.canceled += instance.OnPausing;
            }
        }
    }
    public KeyboardAndMouseActions @KeyboardAndMouse => new KeyboardAndMouseActions(this);
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardAndMouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    public interface IKeyboardAndMouseActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnApproaching(InputAction.CallbackContext context);
        void OnPausing(InputAction.CallbackContext context);
    }
}
