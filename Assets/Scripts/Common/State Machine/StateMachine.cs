using UnityEngine;
using System.Collections;
public class StateMachine : MonoBehaviour
{
    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }
    protected State _currentState;
    protected bool _inTransition;
    public virtual T GetState<T>() where T : State //what?
    {
        T target = GetComponent<T>();
        if (target == null) //if the getcomponent fail, I add a new component
            target = gameObject.AddComponent<T>();
        return target;
    }

    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }
    protected virtual void Transition(State value) //if is the same value, is in transition, if the last state is ongoing, then exit and enter the new state
    {
        if (_currentState == value || _inTransition)
            return;
        _inTransition = true;

        if (_currentState != null)
            _currentState.Exit();

        _currentState = value;

        if (_currentState != null)
            _currentState.Enter();

        _inTransition = false;
    }
}
