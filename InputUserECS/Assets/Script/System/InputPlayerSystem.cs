using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;

partial class InputPlayerSystem : SystemBase
{
    private InputAction _moveAction;
    private InputAction _jerkAction;

    private float2 _moveInput;
    private float _jerkInput;

    [RequiredMember]
    protected override void OnCreate()
    {

        _moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
        _jerkAction = new InputAction("jerk", binding: "<Keyboard>/space");

        _moveAction.AddCompositeBinding("Dpad")
            .With("Up", binding: "<Keyboard>/w")
            .With("Down", binding: "<Keyboard>/s")
            .With("Left", binding: "<Keyboard>/a")
            .With("Right", binding: "<Keyboard>/d");

        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.canceled+= context => { _moveInput = context.ReadValue<Vector2>(); };

        _jerkAction.performed += context => { _jerkInput = context.ReadValue<float>(); };
        _jerkAction.canceled += context => { _jerkInput = context.ReadValue<float>(); };

        _jerkAction.Enable();
        _moveAction.Enable();
    }

    protected override void OnUpdate()
    {
        foreach (var inputPlayer in SystemAPI.Query<RefRW<InputPlayerComponent>>())
        {
            inputPlayer.ValueRW.Move = _moveInput;
            inputPlayer.ValueRW.Jerk = _jerkInput;
        }
    }

    protected override void OnDestroy()
    {
        _jerkAction.Disable();
        _moveAction.Disable();
    }
}
