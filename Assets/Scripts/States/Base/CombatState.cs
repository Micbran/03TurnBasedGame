using UnityEngine;

[RequireComponent(typeof(CombatStateMachine))]
public class CombatState : State
{
    protected CombatStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        this.StateMachine = GetComponent<CombatStateMachine>();
    }
}
