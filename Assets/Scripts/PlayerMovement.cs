// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerMovement.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerMovement : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerMovement()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerMovement"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""e2120baf-a857-4b76-ae6a-404c30494bc7"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""ff36fb48-503f-4843-b8ae-9d8f0b26120e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""c8575446-b9d2-4b34-95b9-3165ef3a0542"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseRelease"",
                    ""type"": ""Button"",
                    ""id"": ""af767a4d-279a-4b84-90df-e4f2304e12ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e3a1dcda-b6ce-4b85-8c5e-b9559f88f605"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f715312-3ad9-48ba-8ed0-c3ba2f92e60e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""333a6c63-6ee6-44f7-a08b-44954223a748"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_MousePosition = m_Movement.FindAction("MousePosition", throwIfNotFound: true);
        m_Movement_MouseClick = m_Movement.FindAction("MouseClick", throwIfNotFound: true);
        m_Movement_MouseRelease = m_Movement.FindAction("MouseRelease", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_MousePosition;
    private readonly InputAction m_Movement_MouseClick;
    private readonly InputAction m_Movement_MouseRelease;
    public struct MovementActions
    {
        private @PlayerMovement m_Wrapper;
        public MovementActions(@PlayerMovement wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_Movement_MousePosition;
        public InputAction @MouseClick => m_Wrapper.m_Movement_MouseClick;
        public InputAction @MouseRelease => m_Wrapper.m_Movement_MouseRelease;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMousePosition;
                @MouseClick.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseClick;
                @MouseRelease.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseRelease;
                @MouseRelease.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseRelease;
                @MouseRelease.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseRelease;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @MouseRelease.started += instance.OnMouseRelease;
                @MouseRelease.performed += instance.OnMouseRelease;
                @MouseRelease.canceled += instance.OnMouseRelease;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    public interface IMovementActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnMouseRelease(InputAction.CallbackContext context);
    }
}
