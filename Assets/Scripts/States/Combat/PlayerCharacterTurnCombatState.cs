using UnityEngine;

public class PlayerCharacterTurnCombatState : CombatState
{
    public override void OnEnter()
    {
        StateMachine.Input.PressedConfirm += this.OnPressedConfirm;
        this.StateMachine.CurrentActor.StartNewTurn();
        this.StateMachine.TriggerNewTurn();
    }

    public override void OnExit()
    {
        StateMachine.Input.PressedConfirm -= this.OnPressedConfirm;
    }

    public override void Tick()
    {
        
    }

    private void OnPressedConfirm()
    {
        this.TransitionToNextTurn();
    }

    public void TransitionToNextTurn()
    {
        this.StateMachine.Turn.EndTurn();
        this.StateMachine.CurrentActor.ActorEndTurn();
        this.StateMachine.ChangeState<PickNextActorCombatState>();
    }
}
