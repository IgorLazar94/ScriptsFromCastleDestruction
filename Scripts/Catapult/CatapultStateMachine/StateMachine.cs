using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class StateMachine : MonoBehaviour, ISwitchState
{
    public State CurrentState { get; set; }
    private List<State> states;

    private void OnEnable()
    {
        EventBus.onCreateNewProjectile += OnCreateNewProjectile;
    }

    private void OnDisable()
    {
        EventBus.onCreateNewProjectile -= OnCreateNewProjectile;
    }

    private void Awake()
    {
        states = new List<State>() { new IdleState(this), new AimState(this), new ShotState(this) };
        CurrentState = states[0];
        CurrentState.Enter();
    }

    public void InitializeState(State startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    private void Update()
    {
        CurrentState.DoWork();
    }

    public void SwitchState<T>() where T : State
    {
        var state = states.FirstOrDefault(s => s is T);
        CurrentState.Exit();
        state.Enter();
        CurrentState = state;
    }

    private void OnCreateNewProjectile(GameObject newObject)
    {
        SwitchState<IdleState>();
    }
}
