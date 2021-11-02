using UnityEngine;

public class PlayerCharacterTurnCombatState : CombatState
{
    // acting entity goes here
    // and probably some UI

    public override void OnEnter()
    {
        Debug.Log("Entering PlayerCharacterTurnCombatState.");
        StateMachine.Input.PressedConfirm += this.OnPressedConfirm;
    }

    public override void OnExit()
    {
        StateMachine.Input.PressedConfirm -= this.OnPressedConfirm;
        Debug.Log("Exiting PlayerCharacterTurnCombatState.");
    }

    private void OnPressedConfirm()
    {
        Debug.Log("Confirm pressed.");
        this.TransitionToNextTurn();
    }

    private void TransitionToNextTurn()
    {
        this.StateMachine.ChangeState<PickNextActorCombatState>();
    }
}
