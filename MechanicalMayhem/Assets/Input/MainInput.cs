//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input/MainInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MainInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInput"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""3ddd7484-07f8-4294-b4fc-e9c7e3719e84"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""89199c40-00bb-47a1-9404-8964e4132bbf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Value"",
                    ""id"": ""a4d3fb8a-49ef-46e6-ab87-91f6a256e480"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Value"",
                    ""id"": ""3590f1b0-71e6-479b-8462-36193feef33c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ToggleInventory"",
                    ""type"": ""Value"",
                    ""id"": ""ae302c20-833e-4bb3-af7b-0a31ffe56fc1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""ae2dce05-7ccf-4903-bf2b-7d49166102a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Value"",
                    ""id"": ""3b7f3530-45f1-4b53-9314-90e5e5451356"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SelectWeapon1"",
                    ""type"": ""Value"",
                    ""id"": ""ccfab791-6a09-436d-b748-38d30f8aadd9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SelectWeapon2"",
                    ""type"": ""Value"",
                    ""id"": ""c1f6f73d-481b-40cc-a86c-2d4d53507ab8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SelectWeapon3"",
                    ""type"": ""Value"",
                    ""id"": ""397dc104-eb0b-4b2b-8cd6-4ead655c09c8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""161ddaca-6044-4e70-a134-e1feed90dbc4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PauseScreen"",
                    ""type"": ""Value"",
                    ""id"": ""682e077f-6f46-4f71-bb59-861fb3ad3af9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""caacfec9-1fcc-4d2e-928b-689f1921d661"",
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
                    ""id"": ""191350e6-aed2-456e-92fb-52281bb32d9f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""064bbb3b-ff6e-477b-a79e-289447b0e7d9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2e66d5f1-902d-4294-b714-273fe3acdbed"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""879bbcbd-8a27-4208-911e-ed41b5b74b2f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""abc5548e-ae37-481b-bc02-949a0625d022"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a5b5fb3-8367-4156-9780-8e8507f0eb9d"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectWeapon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e0f8b80-5625-4fb4-bfd9-a7d82e3217d8"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectWeapon2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e3b5649-ee57-434e-9a85-5fd73aed0468"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectWeapon3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""315ff72f-49f0-4496-97d1-5e62a52527a8"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35e792bc-15e2-4d81-a268-55d13cba83af"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b934552-1cec-4739-9f22-b0d0daddc97e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c754a681-4d71-4402-83e4-d209daa486dc"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f2f63ab-ac69-4a19-be1a-88076770065d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89f374fd-ced9-4fd7-8bb7-0624fe50f7dc"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""PauseScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KBM"",
            ""bindingGroup"": ""KBM"",
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
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Movement = m_Main.FindAction("Movement", throwIfNotFound: true);
        m_Main_Interact = m_Main.FindAction("Interact", throwIfNotFound: true);
        m_Main_Sprint = m_Main.FindAction("Sprint", throwIfNotFound: true);
        m_Main_ToggleInventory = m_Main.FindAction("ToggleInventory", throwIfNotFound: true);
        m_Main_Fire = m_Main.FindAction("Fire", throwIfNotFound: true);
        m_Main_Reload = m_Main.FindAction("Reload", throwIfNotFound: true);
        m_Main_SelectWeapon1 = m_Main.FindAction("SelectWeapon1", throwIfNotFound: true);
        m_Main_SelectWeapon2 = m_Main.FindAction("SelectWeapon2", throwIfNotFound: true);
        m_Main_SelectWeapon3 = m_Main.FindAction("SelectWeapon3", throwIfNotFound: true);
        m_Main_MousePosition = m_Main.FindAction("MousePosition", throwIfNotFound: true);
        m_Main_PauseScreen = m_Main.FindAction("PauseScreen", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_Movement;
    private readonly InputAction m_Main_Interact;
    private readonly InputAction m_Main_Sprint;
    private readonly InputAction m_Main_ToggleInventory;
    private readonly InputAction m_Main_Fire;
    private readonly InputAction m_Main_Reload;
    private readonly InputAction m_Main_SelectWeapon1;
    private readonly InputAction m_Main_SelectWeapon2;
    private readonly InputAction m_Main_SelectWeapon3;
    private readonly InputAction m_Main_MousePosition;
    private readonly InputAction m_Main_PauseScreen;
    public struct MainActions
    {
        private @MainInput m_Wrapper;
        public MainActions(@MainInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Main_Movement;
        public InputAction @Interact => m_Wrapper.m_Main_Interact;
        public InputAction @Sprint => m_Wrapper.m_Main_Sprint;
        public InputAction @ToggleInventory => m_Wrapper.m_Main_ToggleInventory;
        public InputAction @Fire => m_Wrapper.m_Main_Fire;
        public InputAction @Reload => m_Wrapper.m_Main_Reload;
        public InputAction @SelectWeapon1 => m_Wrapper.m_Main_SelectWeapon1;
        public InputAction @SelectWeapon2 => m_Wrapper.m_Main_SelectWeapon2;
        public InputAction @SelectWeapon3 => m_Wrapper.m_Main_SelectWeapon3;
        public InputAction @MousePosition => m_Wrapper.m_Main_MousePosition;
        public InputAction @PauseScreen => m_Wrapper.m_Main_PauseScreen;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_MainActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnInteract;
                @Sprint.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSprint;
                @ToggleInventory.started -= m_Wrapper.m_MainActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnToggleInventory;
                @Fire.started -= m_Wrapper.m_MainActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnFire;
                @Reload.started -= m_Wrapper.m_MainActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnReload;
                @SelectWeapon1.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSelectWeapon1;
                @SelectWeapon1.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSelectWeapon1;
                @SelectWeapon1.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSelectWeapon1;
                @SelectWeapon2.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSelectWeapon2;
                @SelectWeapon2.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSelectWeapon2;
                @SelectWeapon2.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSelectWeapon2;
                @SelectWeapon3.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSelectWeapon3;
                @SelectWeapon3.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSelectWeapon3;
                @SelectWeapon3.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSelectWeapon3;
                @MousePosition.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                @PauseScreen.started -= m_Wrapper.m_MainActionsCallbackInterface.OnPauseScreen;
                @PauseScreen.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnPauseScreen;
                @PauseScreen.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnPauseScreen;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @ToggleInventory.started += instance.OnToggleInventory;
                @ToggleInventory.performed += instance.OnToggleInventory;
                @ToggleInventory.canceled += instance.OnToggleInventory;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @SelectWeapon1.started += instance.OnSelectWeapon1;
                @SelectWeapon1.performed += instance.OnSelectWeapon1;
                @SelectWeapon1.canceled += instance.OnSelectWeapon1;
                @SelectWeapon2.started += instance.OnSelectWeapon2;
                @SelectWeapon2.performed += instance.OnSelectWeapon2;
                @SelectWeapon2.canceled += instance.OnSelectWeapon2;
                @SelectWeapon3.started += instance.OnSelectWeapon3;
                @SelectWeapon3.performed += instance.OnSelectWeapon3;
                @SelectWeapon3.canceled += instance.OnSelectWeapon3;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @PauseScreen.started += instance.OnPauseScreen;
                @PauseScreen.performed += instance.OnPauseScreen;
                @PauseScreen.canceled += instance.OnPauseScreen;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    private int m_KBMSchemeIndex = -1;
    public InputControlScheme KBMScheme
    {
        get
        {
            if (m_KBMSchemeIndex == -1) m_KBMSchemeIndex = asset.FindControlSchemeIndex("KBM");
            return asset.controlSchemes[m_KBMSchemeIndex];
        }
    }
    public interface IMainActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnToggleInventory(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnSelectWeapon1(InputAction.CallbackContext context);
        void OnSelectWeapon2(InputAction.CallbackContext context);
        void OnSelectWeapon3(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnPauseScreen(InputAction.CallbackContext context);
    }
}
