using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public State CurrentState => this.currentState;
    protected bool InTransition { get; private set; }

    State currentState;
    protected State previousState;

    public virtual void ChangeState<T>() where T : State
    {
        T targetState = GetComponent<T>();
        if (targetState == null)
        {
            Debug.LogError("Cannot change state as it is not attached to this StateMachine object.");
            return;
        }

        this.StartStateChange(targetState);
    }

    public void RevertState()
    {
        if (this.previousState != null)
        {
            this.StartStateChange(this.previousState);
        }
    }

    protected void StartStateChange(State targetState)
    {
        if (this.currentState != targetState && !this.InTransition)
        {
            this.Transition(targetState);
        }
    }

    protected void Transition(State targetState)
    {
        this.InTransition = true;

        this.currentState?.OnExit();
        this.currentState = targetState;
        this.currentState?.OnEnter();

        this.InTransition = false;
    }

    private void Update()
    {
        if (CurrentState != null && !InTransition)
        {
            this.CurrentState.Tick();
        }
    }
}
