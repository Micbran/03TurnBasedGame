using UnityEngine;

public class PlayerCharacterTurnCombatState : CombatState
{
    // acting entity goes here
    // and probably some UI

    public override void OnEnter()
    {
        StateMachine.Input.PressedConfirm += this.OnPressedConfirm;
    }

    public override void OnExit()
    {
        this.StateMachine.Turn.EndTurn();
        StateMachine.Input.PressedConfirm -= this.OnPressedConfirm;
    }

    private void OnPressedConfirm()
    {
        this.TransitionToNextTurn();
    }

    private void TransitionToNextTurn()
    {
        this.StateMachine.ChangeState<PickNextActorCombatState>();
    }
}
